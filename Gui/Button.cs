using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Abyss.Gui
{
    public class Button
    {
        public Action<View> OnClick;
        public Texture2D Image;
        public Texture2D ActiveImage;
        public Texture2D InactiveImage;
        public Texture2D ClickImage;
        public Texture2D DisabledImage;
        public string Text;
        public TextAlligment TextAlligment;
        public Vector2 Position;
        public Rectangle Rectangle;
        public bool IsActive = true;
        public Color TextColor;
        public int Top { get => Rectangle.Top; }
        public int Bottom { get => Rectangle.Bottom; }
        public int Left { get => Rectangle.Left; } 
        public int Right { get => Rectangle.Right; }

        public Button(
            Action<View> onClick,
            int x,
            int y,
            int width,
            int height,
            string text,
            TextAlligment alligment = TextAlligment.Center,
            Color? textColor = null,
            Texture2D activeImage = null,
            Texture2D inactiveImage = null,
            Texture2D clickImage = null,
            Texture2D disabledImage = null)
        {
            OnClick = onClick;
            ActiveImage = activeImage == null ? Arts.ButtonActive : activeImage;
            InactiveImage = inactiveImage == null ? Arts.ButtonInactive : inactiveImage;
            ClickImage = clickImage == null ? Arts.ButtonClick : clickImage;
            DisabledImage = disabledImage == null ? Arts.ButtonDisabled : disabledImage;
            TextAlligment = alligment;
            Image = InactiveImage;
            TextColor = textColor == null? Color.Black : (Color)textColor;
            Position = new Vector2(x, y);
            Rectangle = new Rectangle(x, y, width, height);
            Text = text;
        }

        public void OnActive() => Image = ActiveImage;
        public void OnInactive() => Image = InactiveImage; 

        public void Update(View view)
        {
            if (!IsActive)
            {
                Image = DisabledImage;
                return;
            }

            if (Rectangle.Contains(view.Input.MousePosition))
            {
                var oldImage = Image;
                OnActive();
                if (oldImage != Image)
                    SoundEffects.ButtonOnActive.Play();
                if (view.Input.WasLMBPressed())
                {
                    Image = ClickImage;
                    OnClick(view);
                    SoundEffects.ButtonOnClick.Play();
                }
            }
            else
            {
                Image = InactiveImage;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Rectangle, Color.White);
            if (TextAlligment == TextAlligment.Center)
                spriteBatch.DrawStrCentered(Rectangle, Text, new Vector2(0, 10), TextColor);
            else if (TextAlligment == TextAlligment.Left)
                spriteBatch.DrawString(Arts.Font, Text, Position + new Vector2(20, 40), TextColor);
            
        }
    }

    public enum TextAlligment
    {
        Left,
        Center,
    }
}
