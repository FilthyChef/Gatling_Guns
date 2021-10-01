using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.IO;

namespace DuckGame.GatlingGuns{
    [EditorGroup("Equipment|Gatling Guns")]
    class DarkLevels : Thing{
		Dictionary<Thing,LightSource> sources = new Dictionary<Thing,LightSource>();
        RenderTarget2D lightTarget;
        RenderTarget2D tempTarget;
        public static DarkLevels current;
        public EditorProperty<float> duckRadius = new EditorProperty<float>(30,null,0,80,1);
        public EditorProperty<bool> LocalDuckOnly = new EditorProperty<bool>(false);
        public EditorProperty<bool> BrightDucks = new EditorProperty<bool>(true);
        public static FieldInfo drawEndField = typeof(Bullet).GetField("drawEnd", BindingFlags.Instance | BindingFlags.NonPublic);
        public static Dictionary<Type, LightSourceInfo> CustomLightSources = new Dictionary<Type, LightSourceInfo>();
        public readonly Sprite lightMask;
        public readonly MTEffect _effect;
        public float maskDimension = 256;
        public float TextureScale = 0.5f;
        public Vec2 ScreenSize;

        //add default light sources and move the shader in the static constructor, which is called when editor is initialized
        static DarkLevels(){
            InstallShader();
            AddSource(typeof(Duck),
                (t) => (current.BrightDucks && (!current.LocalDuckOnly || (t as Duck).profile.localPlayer)) ? current.duckRadius : 0);

            AddSource(typeof(Bullet),
                (t) => (t as Bullet).ammo.bulletThickness * 15);

            AddSource(typeof(SmallFire), 40,new Vec2(0,-3));
            AddSource(typeof(Firecracker), 30);
            AddSource(typeof(Flare), 60);
            AddSource(typeof(LaserBullet), 40, bulletLightOffset);
            AddSource(typeof(MagBullet), 40);
            AddSource(typeof(QuadLaserBullet), 60);
            AddSource(typeof(Fluid),(thing)=>{
                return (thing as Fluid).data.heat * 20;
            });

            AddSource(typeof(FluidPuddle),(thing)=>
            {
                var fluid = thing as FluidPuddle;
                if(fluid.data.heat > 0.5f || fluid.onFire)
                    return (fluid.collisionSize.x)+16;
                return 0;
            },(Thing thing)=>
                new Vec2(0, -thing.collisionSize.y)
            );
        }

        public static void AddSource(Type t, float radius){
            CustomLightSources.Add(t,new LightSourceInfo(radius));
        }
        public static void AddSource(Type t, float radius, Vec2 offset){
            CustomLightSources.Add(t, new LightSourceInfo(radius,offset));
        }
        public static void AddSource(Type t,float radius,Func<Thing,Vec2> getOffset){
            CustomLightSources.Add(t, new LightSourceInfo(radius, getOffset));
        }
        public static void AddSource(Type t, Func<Thing, float> getradius){
            CustomLightSources.Add(t,new LightSourceInfo(getradius));
        }
        public static void AddSource(Type t,Func<Thing,float> getradius,Vec2 offset){
            CustomLightSources.Add(t, new LightSourceInfo(getradius,offset));
        }
        public static void AddSource(Type t, Func<Thing,float> getradius,Func<Thing,Vec2> getOffset){
            CustomLightSources.Add(t,new LightSourceInfo(getradius,getOffset));
        }
        /// <summary>
        /// copies the shader file to duckgames content file because relative path is needed
        /// </summary>
        public static void InstallShader(){
            var path = GetPath<GatlingGuns>("DarkShader.xnb").Replace("/", "\\");
            if (File.Exists(path)){
                File.Copy(path, Path.GetFullPath(@"Content\Shaders\DarkShader.xnb"), true);
            }
        }
        /// <summary>
        /// Gets the offset from the collisioncenter to the position of the bullet.
        /// </summary>
        /// <param name="thing">the bullet</param>
        /// <returns></returns>
        static Vec2 bulletLightOffset(Thing thing){
            var bul = thing as Bullet;
            var drawEnd = (Vec2)drawEndField.GetValue(thing);
            if (thing.removeFromLevel)
                return Vec2.Zero;
            return drawEnd - thing.collisionCenter;
        }
        public DarkLevels(float x,float y) : base(){
            this._editorName = "Dark Levels Flag";
			this.editorTooltip = "Place on the map to flag this level as Dark Level, right click to setup settings";
			lightMask = new Sprite(GetPath("Effects/lightMask.png"));
            graphic = new Sprite(GetPath("Effects/darkLevelsDarkBulb.png"));
            collisionSize = new Vec2(16);
            center = new Vec2(8);
            collisionOffset = -center;
            layer = Layer.Foreground;
            current = this;
            _effect = Content.Load<Effect>("Shaders/DarkShader");
            UpdateSize();
        }
        /* <summary>
        Changes the size of the renderTargets
        Also lets you specify how much you want to scale down the 
        rendertargets so that its smaller so you dont need a good Graphics card.
        ie 0.5 will make the targets half the size.
        </summary>
        <param name="NewScale"></param>
		*/
        public void UpdateSize(float NewScale = 1)
        {
            TextureScale = NewScale;
            ScreenSize = new Vec2(MonoMain.screenWidth, MonoMain.screenHeight) * TextureScale;
            lightTarget = new RenderTarget2D((int)ScreenSize.x, (int)ScreenSize.y);
            tempTarget = new RenderTarget2D((int)ScreenSize.x, (int)ScreenSize.y);
        }
        public override void Initialize(){
            base.Initialize();
            //remove all existing darknesses in the editor
            if (Level.current is Editor){
                var VanillaDarkness = Level.current.things[typeof(DarkLevels)];
                foreach (var DarkLevels in VanillaDarkness.Where(x => x != this))
                    (Level.current as Editor).RemoveObject(DarkLevels);
                return;
            }
            //make level dark on init and remove collisionbox
            updateLightTarget();
            collisionSize = Vec2.Zero;
        }
        /// <summary>
        /// Gets how much to scale down the LightTarget when drawn to fit with the camera
        /// </summary>
        /// <returns></returns>
        public Vec2 getCameraScaling(){
            return Level.current.camera.size / ScreenSize;
        }
        /// <summary>
        /// Adds a lightsource to the level using info 
        /// will update radius and offset if its already in the level
        /// </summary>
        /// <param name="t"></param>
        /// <param name="info"></param>
        public void AddSource(Thing t,LightSourceInfo info){
            if (sources.ContainsKey(t)){
                if (info.customOffset != null)
                    sources[t].offset = info.GetOffset(t);
                if (info.customRadius != null)
                    sources[t].Radius = info.GetRadius(t);
                return;
            }
            sources.Add(t, new LightSource(t.collisionCenter, info.GetRadius(t)) { offset = info.GetOffset(t) });
            return;
        }
        public override void Update(){
            //check the custom light sources
            foreach(var thing in Level.current.things.Where(x=>CustomLightSources.ContainsKey(x.GetType()))){
                var lInfo = CustomLightSources[thing.GetType()];
                AddSource(thing,lInfo);
            }
            //check if screen size has changed, if so update the light target size
            if(MonoMain.screenHeight != lightTarget.height / TextureScale || MonoMain.screenWidth != lightTarget.width / TextureScale)
                UpdateSize(TextureScale);
			
            for(var i = 0; i < sources.Count;i++){
                var source = sources.ElementAt(i);
                //make light source fade when its removed from the level
                if (source.Key.removeFromLevel)
                    source.Value.fading = true;
                //reduce radius of fading sources until its low enough to be removed
                if (source.Value.fading){
                    source.Value.Radius *= 0.85f;
                    if (source.Value.Radius <= 3){
                        sources.Remove(source.Key);
                        i--;
                    }
                }
                //update the positions of the light source
                source.Value.center = source.Key.collisionCenter;
            }
            //reupdate target according to newly added light sources
            updateLightTarget();
            base.Update();
        }
        /// <summary>
        /// gets the position of a light source on a new rendertarget
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public Vec2 getTargetPos(Vec2 scale, LightSource source){
            return (source.pos - Level.current.camera.position) / scale;
        }
        /// <summary>
        /// updates the lightTarget Texture to math the current Light Sources
        /// </summary>
        void updateLightTarget()
        {
            var camera = Level.current.camera;
            //As the shader only is applied to all textures being drawn i cant 
            //draw the mask on a black background and then slap it on the screen
            //I need to first draw each mask on a black background and blend them together with
            //Additive Blending and then draw it on another target with the shader so that it will
            //have a transparent background behind the white pixels
            Vec2 cScale = getCameraScaling();
            Graphics.SetRenderTarget(tempTarget);
            Graphics.Clear(Color.Black);
            Graphics.screen.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            //Draw each mask on a black background
            foreach (var source in sources)
            {
                var p = getTargetPos(cScale,source.Value);
                var maskScale = (source.Value.diameter / maskDimension) / cScale.x;
                Graphics.Draw(lightMask,p.x,p.y,maskScale,maskScale);
            }
            Graphics.screen.End();

            //Render the temptarget and apply shader that clears out light
            Graphics.SetRenderTarget(lightTarget);
            Graphics.Clear(Color.Transparent);
            Graphics.screen.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);
            _effect.effect.CurrentTechnique.Passes[0].Apply();
            //Draw the mask target with the shader applied
            Graphics.Draw(tempTarget,0,0);
            Graphics.screen.End();
  
            Graphics.SetRenderTarget(null);
        }

        public override void Draw()
        {
            //Draw the lightTarget
            var camera = Level.current.camera;
            var cScale = getCameraScaling();
            Graphics.Draw(lightTarget,camera.x,camera.y,cScale.x,cScale.y);

            if(Level.current is Editor)
                base.Draw();
        }

    }
    /// <summary>
    /// Contains info and custom functions to create Lightsources off.
    /// </summary>
    class LightSourceInfo{
        public Vec2 Offset;
        public float Radius;
        public Func<Thing,float> customRadius;
        public Func<Thing, Vec2> customOffset;
        public LightSourceInfo(float radius,Vec2 offset)
        {
            Radius = radius;
            Offset = offset;
        }

        public virtual Vec2 GetOffset(Thing t)
        {
            if (customOffset != null)
                return customOffset.Invoke(t);
            return Offset;
        }

        public virtual float GetRadius(Thing t)
        {
            if(customRadius != null)
            {
                return customRadius.Invoke(t);
            }
            return Radius;

        }

        public LightSourceInfo(float radius)
        {
            Offset = Vec2.Zero;
            Radius = radius;
        }

        public LightSourceInfo(Func<Thing, float> getradius)
        {
            Offset = Vec2.Zero;
            customRadius = getradius;
        }

        public LightSourceInfo(Func<Thing, float> getradius,Vec2 offset)
        {
            Offset = offset;
            customRadius = getradius;
        }
        public LightSourceInfo(Func<Thing, float> getradius, Func<Thing, Vec2> offset)
        {
            customOffset = offset;
            customRadius = getradius;
        }

        public LightSourceInfo(float radius, Func<Thing, Vec2> offset)
        {
            customOffset = offset;
            Radius = radius;
        }


    }

    class LightSource
    {
        public Vec2 position;
        public Vec2 offset = Vec2.Zero;
        private float radius;
        public bool fading = false;

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                position += new Vec2(radius-value);
                radius = value;
            }
        }

        public Vec2 pos
        {
            get
            {
                return position + offset;
            }
            set
            {
                position = value;
            }
        }

        public Vec2 center
        {
            get
            {
                return pos + new Vec2(radius);
            }
            set
            {
                pos = value - new Vec2(radius);
            }
        }

        public float diameter
        {
            get
            {
                return radius * 2;
            }
        }

        public LightSource(Vec2 center,float radius)
        {
            this.radius = radius;
            this.center = center;
        }
    }
}
