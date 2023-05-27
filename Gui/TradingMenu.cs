using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Gui
{
    public class TradingMenu : Menu
    {
        private string currentWeaponStats;
        private int currentMoney;
        private Dictionary<WeaponName, int> Costs = new Dictionary<WeaponName, int>
        {
            { WeaponName.DP28, 200},
            { WeaponName.Colt, 100},
            { WeaponName.BAR, 1700},
            { WeaponName.Gewehr43, 1650},
            { WeaponName.Kar98K, 1500},
            { WeaponName.Mosin, 1500},
            { WeaponName.MP40, 1000},
            { WeaponName.PPSH, 1200},
            { WeaponName.Revolver, 900},
            { WeaponName.Stg44, 800},
            { WeaponName.Thompson, 700},
            { WeaponName.LMG99, 2000},
            { WeaponName.Cheat, 100},
        };

        private Dictionary<Button, int> ButtonsCosts;
        public TradingMenu(int width, int heigth) : base(width, heigth)
        {
        }

        public override void OnCreate()
        {
            ButtonsCosts = new Dictionary<Button, int>();
            State = MenuState.TradeMenu;
            Background = Arts.MainBackground;

            var rnd = new Random();
            var weapons = new WeaponName[3];
            weapons = weapons.Select(w => Costs.ToArray()[rnd.Next(Costs.Count)].Key).ToArray();
            var pos = new Vector2(200, 200);
            var padding = new Vector2(0, 150);
            foreach (var weaponName in weapons)
            {
                var weapon = WeaponsFactory.CreateWeapon(weaponName, null);
                var text = $"{weaponName} (УР:{weapon.Damage}, СКОР:{weapon.BulletSpeed}, КД:{weapon.Cooldown}) - {Costs[weaponName]}м";
                var act = CreateBuyFunction(weaponName);
                var btn = new Button(
                                act,
                                (int)pos.X,
                                (int)pos.Y,
                                1500, 
                                100, 
                                text, 
                                TextAlligment.Left,
                                textColor: Color.White,
                                activeImage: Arts.WeaponBuyInactive,
                                inactiveImage: Arts.WeaponBuyActive,
                                clickImage: Arts.WeaponBuyActive);
                Buttons.Add(btn);
                ButtonsCosts[btn] = Costs[weaponName];
                pos += padding;
            }

            var text1 = $"Аптечка (Восстанавливает 100 очков здоровья) - 100м";
            Action<View> act1 = (v) =>
            {
                v.Game.Player.Money -= 100;
                v.Game.Player.MedecineCount += 1;
            };
            var btn1 = new Button(
                            act1,
                            (int)pos.X,
                            (int)pos.Y,
                            1500,
                            100,
                            text1,
                            TextAlligment.Left,
                            textColor: Color.White,
                            activeImage: Arts.WeaponBuyInactive,
                            inactiveImage: Arts.WeaponBuyActive,
                            clickImage: Arts.WeaponBuyActive);
            Buttons.Add(btn1);
            ButtonsCosts[btn1] = 100;

            var resumeAction = new Action<View>((v) => { v.Game.State = GameState.Loading; });
            var resumeButton = new Button(resumeAction, (int)pos.X + 1100, Height - 150, 400, 100, "Продолжить");
            Buttons.Add(resumeButton);
        }

        public override void OnResume()
        {
            Buttons.Clear();
            OnCreate();
        }

        public override void Update(View view)
        {
            var money = view.Game.Player.Money;
            var w = view.Game.Player.Weapon;
            currentWeaponStats = $"{w.Name} - (УР:{w.Damage}, СКОР:{w.BulletSpeed}, КД:{w.Cooldown})";
            currentMoney = money;
            foreach (var button in Buttons)
                if (ButtonsCosts.ContainsKey(button))
                    button.IsActive = money >= ButtonsCosts[button];
            base.Update(view);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Arts.Font, currentWeaponStats, new Vector2(270, 100), Color.White);
            spriteBatch.DrawString(Arts.Font, currentMoney.ToString(), new Vector2(330, Height - 120), Color.White);
            spriteBatch.Draw(Arts.Money, new Rectangle(220, Height - 150, 100, 100), Color.White);
        }

        private Action<View> CreateBuyFunction(WeaponName weapon)
        {
            return new Action<View>((v) =>
            {
                var cost = Costs[weapon];
                v.Game.Player.Money -= cost;
                v.Game.Player.Weapon = WeaponsFactory.CreateWeapon(weapon, new List<Type> { typeof(Enemy) });
            });
        }
    }
}
