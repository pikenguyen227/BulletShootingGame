using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CPTS_487_Project
{
    public class GameplayScreen : GameScreen
    {
        Player player;
        Enemy enemy;
        float timer = 0;
        SingleBullet singlebullet;
        List<TriangleBullet> aListT;
        List<TriangleBullet> aListT1;
        List<HalfCircleBullet> aListH;
        List<FullCircleBullet> aListF;
        int tShoottime = 1;
        int hShoottime = 1;
        int fShoottime = 1;
        int currentBigEnemy = 23;
        int singlecurrent = 1;
        bool singleE = false;
        bool doubleE = false;
        bool bossTime = false;
        MidBoss midBoss;
        int temp = 1;
        public override void LoadContent()
        {
            base.LoadContent();
            aListT = new List<TriangleBullet>();
            aListT1 = new List<TriangleBullet>();
            aListH = new List<HalfCircleBullet>();
            aListF = new List<FullCircleBullet>();
            XMLManager<Player> playerLoader = new XMLManager<Player>();
            player = playerLoader.Load("Content/GamePlay/Player/Player.xml");
            XMLManager<Enemy> eneymyLoader = new XMLManager<Enemy>();
            enemy = eneymyLoader.Load("Content/GamePlay/Enemy/SmallEnemy.xml");
            XMLManager<SingleBullet> bulletLoader = new XMLManager<SingleBullet>();
            singlebullet = bulletLoader.Load("Content/GamePlay/Bullet/SingleBullet.xml");
            XMLManager<TriangleBullet> tbulletloader = new XMLManager<TriangleBullet>();
            for (int i = 0; i < 4; i++)
            {
                aListT.Add(tbulletloader.Load("Content/GamePlay/Bullet/TriangleBullet.xml"));
                aListT[i].LoadContent();
            }
            for (int i = 0; i < 4; i++)
            {
                aListT1.Add(tbulletloader.Load("Content/GamePlay/Bullet/TriangleBullet.xml"));
                aListT1[i].LoadContent();
            }
            XMLManager<HalfCircleBullet> hbulletloader = new XMLManager<HalfCircleBullet>();
            for (int i = 0; i < 3; i++)
            {
                aListH.Add(hbulletloader.Load("Content/GamePlay/Bullet/HalfCircleBullet.xml"));
                aListH[i].LoadContent();
            }
            XMLManager<FullCircleBullet> fbulletloader = new XMLManager<FullCircleBullet>();
            for (int i = 0; i < 3; i++)
            {
                aListF.Add(fbulletloader.Load("Content/GamePlay/Bullet/FullCircleBullet.xml"));
                aListF[i].LoadContent();
            }

            singlebullet.LoadContent();
            enemy.LoadContent();
            player.LoadContent();
            XMLManager<MidBoss> midBossLoader = new XMLManager<MidBoss>();
            midBoss = midBossLoader.Load("Content/GamePlay/Boss/MidBoss.xml");
            midBoss.LoadContent();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            enemy.UnloadContent();
            player.UnloadContent();
            midBoss.UnloadContent();
            singlebullet.UnloadContent();
            for (int i = 0; i < 4; i++)
            {
                aListT[i].UnloadContent();
            }
            for (int i = 0; i < 4; i++)
            {
                aListT1[i].UnloadContent();
            }
            for (int i = 0; i < 3; i++)
            {
                aListH[i].UnloadContent();
            }
            for (int i = 0; i < 3; i++)
            {
                aListF[i].UnloadContent();
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            enemy.Update(gameTime);
            player.Update(gameTime);
            if ((timer >= 100 || (timer >= 55 && timer < 70)) && singlebullet.image[22].Position.Y < 650)
            {
                singlebullet.fireSignal = true;
            }
            else
            {
                singlecurrent = 1;
                singlebullet.fireSignal = false;
            }
            if (singlebullet.fireSignal == true)
            {
                singlebullet.bulletType = "Normal";
                singlebullet.timeFrame = timer;
                singlebullet.setTargetPosition(player.getPosition(0));
                if (singlecurrent < 24 && enemy.getPosition(singlecurrent - 1).Y > 180)
                {
                    singlebullet.image[singlecurrent - 1].Position = enemy.getPosition(singlecurrent - 1);
                    if (singlebullet.image[singlecurrent - 1].Position.X > player.image[0].Position.X)
                    {
                        singlebullet.leftOrRight[singlecurrent - 1] = "right";
                    }
                    else if (singlebullet.image[singlecurrent - 1].Position.X < player.image[0].Position.X)
                    {
                        singlebullet.leftOrRight[singlecurrent - 1] = "left";
                    }
                    if (singlecurrent < 24)
                    {
                        singlecurrent++;
                    }
                }
            }
            singlebullet.Update(gameTime);
            if (timer > 20)
            {
                if ((enemy.getPosition(currentBigEnemy).Y > 0 && enemy.getPosition(currentBigEnemy + 1).Y < 0)
                    || (enemy.getPosition(currentBigEnemy).Y < 0 && enemy.getPosition(currentBigEnemy + 1).Y > 0))
                {
                    singleE = true;
                }
                else if (timer > 84 && singleE == false && enemy.getPosition(currentBigEnemy).Y > 0 && enemy.getPosition(currentBigEnemy + 1).Y > 0)
                {
                    if (enemy.getPosition(currentBigEnemy).X < 500 && enemy.getPosition(currentBigEnemy + 1).X > 0)
                    {
                        doubleE = true;
                    }
                }
                if (singleE)
                {
                    singleMove(gameTime);
                }
                else if (doubleE)
                {
                    doubleMove(gameTime);
                }
            }
            if (timer > 62 )
            {
                if (midBoss.getPosition(0).Y > 0)
                {
                    for (int i = 0; i < hShoottime; i++)
                    {
                        aListH[i].fireSignal = true;
                        if ((midBoss.getPosition(0).X >= 350 || midBoss.getPosition(0).X <= 150) && aListH[i].setted == false)
                        {
                            temp = 2;
                            for (int j = 0; j < 6; j++)
                            {
                                aListH[i].image[j].Position = midBoss.getPosition(0);
                            }
                            aListH[i].setted = true;
                        }
                        if (hShoottime < 3 && aListH[i].setted == true && aListH[hShoottime - 1].image[2].Position.Y - midBoss.getPosition(0).Y >= 30)
                        {
                            hShoottime++;
                        }

                    }

                    if (midBoss.getPosition(0).X > 150 && midBoss.getPosition(0).X < 350)
                    {

                        for (int i = 0; i < fShoottime; i++)
                        {
                            if (temp == 2 && midBoss.getPosition(0).X <= 250 && midBoss.getPosition(0).X >= 185 && aListF[i].setted == false)
                            {
                                for (int j = 0; j < 16; j++)
                                {
                                    aListF[i].image[j].Position = midBoss.getPosition(0);
                                }
                                aListF[i].setted = true;
                            }
                            if (fShoottime < 3 && aListF[i].setted == true && aListF[fShoottime - 1].image[12].Position.Y - midBoss.getPosition(0).Y >= 30)
                            {
                                fShoottime++;
                            }
                        }

                    }

                    if (fShoottime == 3 && aListF[fShoottime - 1].image[10].Position.Y > 600)
                    {
                        for (int j = 0; j < fShoottime; j++)
                        {
                            aListF[j].fireSignal = false;
                            aListF[j].setted = false;
                        }
                        fShoottime = 1;
                    }
                    if (hShoottime == 3 && aListH[hShoottime - 1].image[2].Position.Y > 600)
                    {
                        for (int i = 0; i < hShoottime; i++)
                        {
                            aListH[i].fireSignal = false;
                            aListH[i].setted = false;
                        }
                        hShoottime = 1;
                    }
                    for (int i = 0; i < fShoottime; i++)
                    {
                        aListF[i].Update(gameTime);
                    }
                    for (int i = 0; i < hShoottime; i++)
                    {
                        aListH[i].Update(gameTime);
                    }
                }
                midBoss.Update(gameTime);
            }



            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void singleMove(GameTime gameTime)
        {
            for (int i = 0; i < tShoottime; i++)
            {
                aListT[i].fireSignal = true;
                aListT[i].bulletType = "Trinagle";
                aListT[i].timeFrame = timer;
                aListT[i].setTargetPosition(player.getPosition(0));
                if (enemy.getPosition(currentBigEnemy).Y > 100 && aListT[i].setted == false)
                {
                    for (int j = 0; j < aListT[i].tCurrent; j++)
                    {
                        aListT[i].image[j].Position = enemy.getPosition(currentBigEnemy);
                    }
                    aListT[i].setted = true;
                }
                if (tShoottime < 4 && aListT[i].setted == true && aListT[tShoottime - 1].image[5].Position.Y >= 250)
                {
                    tShoottime++;
                }
                aListT[i].Update(gameTime);
            }
            if (tShoottime == 4 && aListT[tShoottime - 1].image[5].Position.Y > 610)
            {
                tShoottime = 1;
                if (currentBigEnemy == 23)
                {
                    currentBigEnemy = 24;
                }

                else if (currentBigEnemy == 24)
                {
                    currentBigEnemy = 23;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        aListT[i].image[j].Position = new Vector2(-50, -50);
                    }
                    aListT[i].setted = false;
                    aListT[i].fireSignal = false;
                    singleE = false;
                }
            }
        }

        private void doubleMove(GameTime gameTime)
        {
            for (int i = 0; i < tShoottime; i++)
            {
                aListT[i].fireSignal = true;
                aListT[i].bulletType = "Trinagle";
                aListT[i].timeFrame = timer;
                aListT[i].setTargetPosition(player.getPosition(0));
                aListT1[i].fireSignal = true;
                aListT1[i].bulletType = "Trinagle";
                aListT1[i].timeFrame = timer;
                aListT1[i].setTargetPosition(player.getPosition(0));
                if (enemy.getPosition(currentBigEnemy).Y > 100 && aListT[i].setted == false)
                {
                    for (int j = 0; j < aListT[i].tCurrent; j++)
                    {
                        aListT[i].image[j].Position = enemy.getPosition(currentBigEnemy);
                    }
                    aListT[i].setted = true;
                }
                if (enemy.getPosition(currentBigEnemy + 1).Y > 100 && aListT1[i].setted == false)
                {
                    for (int j = 0; j < aListT1[i].tCurrent; j++)
                    {
                        aListT1[i].image[j].Position = enemy.getPosition(currentBigEnemy + 1);
                    }
                    aListT1[i].setted = true;
                }
                if (tShoottime < 4 && aListT[i].setted == true && aListT[tShoottime - 1].image[5].Position.Y > 200)
                {
                    tShoottime++;
                }
                aListT[i].Update(gameTime);
                aListT1[i].Update(gameTime);
            }
            if (tShoottime == 4 && aListT[tShoottime - 1].image[5].Position.Y > 610)
            {
                tShoottime = 1;
                if (currentBigEnemy == 23)
                {
                    currentBigEnemy = currentBigEnemy + 2;
                }
                else if (currentBigEnemy == 25)
                {
                    currentBigEnemy = 23;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        aListT[i].image[j].Position = new Vector2(-50, -50);
                        aListT1[i].image[j].Position = new Vector2(-50, -50);
                    }

                    aListT[i].setted = false;
                    aListT[i].fireSignal = false;
                    aListT1[i].setted = false;
                    aListT1[i].fireSignal = false;
                    singleE = false;
                    doubleE = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);
            singlebullet.Draw(spriteBatch);
            for (int i = 0; i < 4; i++)
            {
                aListT[i].Draw(spriteBatch);
            }
            for (int i = 0; i < 4; i++)
            {
                aListT1[i].Draw(spriteBatch);
            }

            if (enemy.getWave() == 3 && enemy.getSignal() && 88 - timer >= 0)
            {
                if (88 - timer >= 3)
                {
                    enemy.isTime = true;
                }
                spriteBatch.DrawString(enemy.image[27].font, (88 - timer).ToString("0"), new Vector2(460, 0), Color.White);
                spriteBatch.DrawString(enemy.image[27].font, "________________________________________", new Vector2(10, -10), Color.White);
                spriteBatch.DrawString(enemy.image[27].font, "Aki Shizuha", new Vector2(10, 10), Color.Green);
            }
            spriteBatch.DrawString(enemy.image[27].font, timer.ToString("0"), new Vector2(450, 550), Color.White);
            midBoss.Draw(spriteBatch);
            for (int i = 0; i < 3; i++)
            {
                aListH[i].Draw(spriteBatch);
            }
            for (int i = 0; i < 3; i++)
            {
                aListF[i].Draw(spriteBatch);
            }
        }
    }
}
