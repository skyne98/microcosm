using Dcrew.MonoGame._2D_Camera;
using Microcosm.Desktop.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using System;

namespace Microcosm.Desktop
{
    public class MainGame : Game
    {
        const int TARGET_FPS = 144;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Camera _camera;
        float _previousScrollValue;
        Vector2 _previousMousePosition;

        Texture2D _grassTexture;
        SpriteFont _font;

        World _world;
        FPSCounterComponent _fps;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / TARGET_FPS);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _grassTexture = Content.Load<Texture2D>("terrain/grass/grass_05");
            _font = Content.Load<SpriteFont>("fonts/Font");

            _world = new WorldBuilder()
                .AddSystem(new RenderSystem(GraphicsDevice, _camera))
                .AddSystem(new GridSystem(GraphicsDevice, _camera, _grassTexture))
                .Build();
            _fps = new FPSCounterComponent(this, _spriteBatch, _font);
            Components.Add(_fps);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var gamepadState = GamePad.GetState(PlayerIndex.One);
            var mouseState = Mouse.GetState();

            if (gamepadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // Game systems
            _world.Update(gameTime);

            // Zoom
            if (mouseState.ScrollWheelValue < _previousScrollValue)
            {
                _camera.Scale += Vector2.One / 7;
            }
            else if (mouseState.ScrollWheelValue > _previousScrollValue)
            {
                _camera.Scale -= Vector2.One / 7;
            }
            if (_camera.Scale.X < 0.2)
                _camera.Scale = new Vector2(0.2f, _camera.Scale.Y);
            if (_camera.Scale.Y < 0.2)
                _camera.Scale = new Vector2(_camera.Scale.X, 0.2f);
            _previousScrollValue = mouseState.ScrollWheelValue;

            // Drag with mouse
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                var offset = _previousMousePosition - mouseState.Position.ToVector2();
                offset.X /= _camera.Scale.X;
                offset.Y /= _camera.Scale.Y;
                _camera.XY += offset;
            }
            _previousMousePosition = mouseState.Position.ToVector2();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
