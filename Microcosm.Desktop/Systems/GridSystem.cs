using Dcrew.MonoGame._2D_Camera;
using Microcosm.Desktop.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microcosm.Desktop.Systems
{
    public class GridSystem : EntityUpdateSystem
    {
        const int GRID_SIZE = 8;

        private GraphicsDevice _graphicsDevice;
        private readonly Camera _camera;
        private readonly Texture2D _grassTexture;
        private SpriteBatch _spriteBatch;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Hex> _hexMapper;

        public GridSystem(GraphicsDevice graphicsDevice, Camera camera, Texture2D grassTexture)
            : base(Aspect.All(typeof(Hex), typeof(Sprite), typeof(Transform2)))
        {
            _graphicsDevice = graphicsDevice;
            _camera = camera;
            _grassTexture = grassTexture;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _hexMapper = mapperService.GetMapper<Hex>();

            var size = Math.Max(_grassTexture.Width, _grassTexture.Height) / 2;
            size -= 1;

            foreach (var x in Enumerable.Range(-GRID_SIZE, GRID_SIZE * 2 + 1))
            {
                foreach (var y in Enumerable.Range(-GRID_SIZE, GRID_SIZE * 2 + 1))
                {
                    foreach (var z in Enumerable.Range(-GRID_SIZE, GRID_SIZE * 2 + 1))
                    {
                        if (x + y + z == 0)
                        {
                            var hex = Hex.FromCubic(new Vector3(x, y, z), size);

                            var entity = CreateEntity();
                            entity.Attach(hex);
                            entity.Attach(new Sprite(_grassTexture));
                            entity.Attach(new Transform2(hex.ToPixel()));
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);
                var hex = _hexMapper.Get(entity);

                
            }
        }
    }
}
