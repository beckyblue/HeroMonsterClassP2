using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HeroMonsterClassP2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hero Character "specs"
            Character hero = new Character();
            hero.Name = "Thor";
            hero.Health = 85;
            hero.DamageMaximum = 20;
            hero.AttackBonus = false;

            //Monster Character "specs"
            Character monster = new Character();
            monster.Name = "Shinigami";
            monster.Health = 75;
            monster.DamageMaximum = 25;
            monster.AttackBonus = true;

            //Added Dice class 
            Dice dice = new Dice();

            //Adding bonus attacks (both if statements due to both being able to have true outcomes)

            if (hero.AttackBonus)
                monster.Defend(hero.Attack(dice));

            if (monster.AttackBonus)
                hero.Defend(monster.Attack(dice));


            //While loop to continue battle till 0 health
            while (hero.Health > 0 && monster.Health > 0)
            {
                monster.Defend(hero.Attack(dice));
                hero.Defend(monster.Attack(dice));
                //placed here to display stats after each battle
                printStats(hero);
                printStats(monster);
            }

            displayResult(hero, monster);

        }

        //Helper method to show winner and looser
        private void displayResult(Character opponent1, Character opponent2)
        {
            if (opponent1.Health <= 0 && opponent2.Health <= 0)
                resultLabel.Text += string.Format("<p>Both {0} and {1} died.", opponent1.Name, opponent2.Name);
            else if (opponent1.Health <= 0)
                resultLabel.Text += string.Format("<p>{0} defeats {1}</p>", opponent2.Name, opponent1.Name);
            else
                resultLabel.Text += string.Format("<p>{0} defeats {1}</p>", opponent1.Name, opponent2.Name);
        }

        private void printStats(Character character)
        {
            resultLabel.Text += String.Format("<p> Name: {0} | Health: {1} | Damage: {2} | Attack Bonus: {3} </p>",
                character.Name,
                character.Health,
                character.DamageMaximum.ToString(),
                character.AttackBonus.ToString());
        }
    }
    // The additions to P1
    // w/Dice 

    class Dice //Class-Dice, Property-Sides, Method-Roll
    {
        public int Sides { get; set; }

        Random random = new Random(); //Set outside Roll to generate a real random int
        public int Roll()
        {
            return random.Next(0, this.Sides);//setting upper/lower bound
        }
    }

    class Character//Class w/Properties Name, Health, DamageMaximum, AttackBonus
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        // Changed to call 'Dice' method in (), removed excess Random 
        public int Attack(Dice dice)//Attack method
        {
            //add value of Damage Max
            dice.Sides = this.DamageMaximum;
            //removed random and replaced w/dice.Roll()
            int damage = dice.Roll(); // return value for the damage this "attack" will do. 

            return damage;//value sent to damage method
        }

        public void Defend(int damage)//Defend method
        {
            this.Health -= damage;// 'this' links to Health properties to take from "health" of the hero/monster who called it
        }
        
    }


}