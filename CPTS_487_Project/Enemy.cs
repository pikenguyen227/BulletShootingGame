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
    public class Enemy : Entity
    {
        private float timer = 0;
        private bool foward = true;
        private int current = 1;
        private int enemyMovePath = 0;
        private int switchSide = 1;
        private int firstOrSecond = 1;
        private int wave = 1;
        private Image counter;
        private List<bool> bf;
        private List<bool> exit;
        private int iterator = 0;
        private int iteration = 0;
        private bool change = false;
        private bool signal = false;
        private bool bigenemysignal = false;
        private bool last = false;
        private int begin = 400;
        Random r = new Random();
        private int time = 0;
        public bool isTime = false;

        // private GameTime bossGametime = new GameTime(;
        public Enemy()
        {
            Velocity = Vector2.Zero;
            bf = new List<bool>();
            counter = new Image();
            counter.text = new List<string>();
            for (int i = 0; i < 23; i++)
            {
                bf.Add(false);
            }
            exit = new List<bool>();
            for (int i = 0; i < 20; i++)
            {
                exit.Add(false);
            }
        }

        public Vector2 getPosition(int i)
        {
            return image[i].Position;
        }

        public override void LoadContent()
        {
            for (int i = 0; i < 28; i++)
            {
                image[i].LoadContent();
                image[i].deactivateEffect("Fade");
            }
        }
        public override void UnloadContent()
        {
            foreach (Image im in image)
            {
                im.UnloadContent();
            }
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < current; i++)
            {
                if (image[i].Name == "SmallEnemy")
                {
                    if (wave == 1)
                    {
                        waveOne(gameTime, i);
                    }
                    else if (wave == 2)
                    {
                        waveTwo(gameTime, i);
                    }
                    else if (wave == 3)
                    {
                        waveThree(gameTime, i);
                    }
                    else if (wave == 4)
                    {
                        waveFour(gameTime, i);
                    }
                }
            }
            foreach (Image im in image)
            {
                im.Update(gameTime);
            }
        }

        public int getWave()
        {
            return wave;
        }

        public bool getSignal()
        {
            return signal;
        }

        private void waveThree(GameTime gameTime, int i)
        {
            // Big enemy
            if (signal == false && image[23].Position.X > -50)
            {
                image[23].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[23].Position.Y += Velocity.Y = MoveSpeed / 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[23].Position.X -= Velocity.X = MoveSpeed / 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (signal == true)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (foward == true && signal == false)
            {
                wave3Foward(gameTime, i);
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((timer > 3 || signal == true) && time == 0)
                {
                    wave3Back(gameTime, i);
                }
                else if (timer > 89)
                {
                    // Big enemy
                    if (image[23].Position.Y <= 130)
                    {
                        image[23].Position.Y += Velocity.Y = MoveSpeed / 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        image[23].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                        image[23].Position.Y += Velocity.Y = MoveSpeed / 90 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        image[23].Position.X += Velocity.X = MoveSpeed / 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    // Big enemy
                    if (image[24].Position.Y <= 130)
                    {
                        image[24].Position.Y += Velocity.Y = MoveSpeed / 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        image[24].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                        image[24].Position.Y += Velocity.Y = MoveSpeed / 90 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        image[24].Position.X -= Velocity.X = MoveSpeed / 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if(image[24].Position.X < 100)
                    {
                        // Big enemy
                        if (image[25].Position.Y <= 130)
                        {
                            image[25].Position.Y += Velocity.Y = MoveSpeed / 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else
                        {
                            if (image[24].Position.X < 0)
                            {
                                image[25].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                                image[25].Position.Y += Velocity.Y = MoveSpeed / 190 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                image[25].Position.X += Velocity.X = MoveSpeed / 130 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            }
                        }
                        // Big enemy
                        if (image[26].Position.Y <= 130)
                        {
                            image[26].Position.Y += Velocity.Y = MoveSpeed / 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else
                        {
                            if (image[24].Position.X < 0)
                            {
                                image[26].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                                image[26].Position.Y += Velocity.Y = MoveSpeed / 190 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                image[26].Position.X -= Velocity.X = MoveSpeed / 130 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            }
                        }
                    }
                    if (timer > 91)
                        image[i].Position.Y += Velocity.Y = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[current - 1].Position.Y > 0)
                    {
                        if (current < 20)
                        {
                            current++;
                        }
                    }
                }
                if (image[19].Position.Y > 600)
                {
                    wave = 4;
                    current = 1;
                    iterator = 18;
                    iteration = 19;
                    timer = 0;
                    foward = true;
                    setPositionWave3();
                    for (int j = 0; j < 23; j++)
                    {
                        bf[j] = true;
                        image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                    }
                    for (int j = 18; j < 23; j++)
                    {
                        bf[j] = false;
                        image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                        image[j].Position = new Vector2(450, -20);
                    }

                }

            }
        }

        private void waveFour(GameTime gameTime, int i)
        {
            if (image[26].Position.X > -30)
            {
                image[26].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[26].Position.Y += Velocity.Y = MoveSpeed / 190 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[26].Position.X -= Velocity.X = MoveSpeed / 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (image[25].Position.X < 530)
            {
                image[25].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[25].Position.Y += Velocity.Y = MoveSpeed / 190 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[25].Position.X += Velocity.X = MoveSpeed / 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (change == false && bf[i] && (image[18].Position.X < 50 || bf[18] == true))
            {
                wave3Foward(gameTime, i);
                if(bf[22] == true && image[22].Position.Y > 180)
                {
                    change = true;
                    current = 1;
                }
            }
            if(change == true)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 3)
                {
                    wave3Back(gameTime, i);
                }
            }
            for (int j = iterator; j < iteration; j++)
            {
                if (bf[j] == false)
                {
                    if (image[j].Position.Y <= 250)
                    {
                        image[j].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (image[j].Position.Y > 200)
                        {
                            image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                            image[j].Position.X -= 3.5f;
                        }
                    }
                    else
                    {
                        leftMove(gameTime, j);
                        if (image[j].Position.X < -10)
                        {
                            bf[j] = true;
                            image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                            if (j == 18)
                            {
                                image[j].Position = new Vector2(400, -20);
                            }
                            else if (j == 19)
                            {
                                image[j].Position = new Vector2(75, -20);
                            }
                            else if (j == 20)
                            {
                                image[j].Position = new Vector2(125, -20);
                            }
                            else if (j == 21)
                            {
                                image[j].Position = new Vector2(375, -20);
                            }
                            else if (j == 22)
                            {
                                image[j].Position = new Vector2(425, -20);
                            }
                        }
                    }
                    if (image[iteration - 1].Position.Y > 50 && iteration < 23)
                    {
                        iteration++;
                    }
                }
            }
        }

        private void wave3Foward(GameTime gameTime, int i)
        {
            if (i == 0 && image[i].Position.Y <= 180)
            {
                enterScreen(gameTime, i);
            }
            else if (i == 0 && image[i].Position.Y > 180)
            {
                current = 4;
            }

            if (i == 3 && image[i].Position.Y <= 180)
            {
                enterScreen(gameTime, i);
            }
            else if (i == 3 && image[i].Position.Y > 180)
            {
                current = 8;
            }

            if (i == 7 && image[i].Position.Y <= 180)
            {
                enterScreen(gameTime, i);
            }
            else if (i == 7 && image[i].Position.Y > 180)
            {
                current = 12;
            }

            if (i == 11 && image[i].Position.Y <= 180)
            {
                enterScreen(gameTime, i);
            }
            else if (i == 11 && image[i].Position.Y > 180)
            {
                current = 16;
            }

            if (i == 15 && image[i].Position.Y <= 180)
            {
                enterScreen(gameTime, i);
            }
            else if (i == 15 && image[i].Position.Y > 180)
            {
                current = 20;
            }

            if (i == 19 && image[i].Position.Y <= 180)
            {
                enterScreen(gameTime, i);
            }
            else if (i == 19 && image[i].Position.Y > 180)
            {
                current = 1;
                foward = false;
            }
        }

        private void wave3Back(GameTime gameTime, int i)
        {
            if (i == 0 && image[i].Position.Y >= -20)
            {
                leaveScreen(gameTime, i);
                if (image[i].Position.Y < 150)
                {
                    current = 4;
                }
                if (image[i].Position.Y < 50)
                {
                    signal = true;
                    image[23].Position = new Vector2(200, -50);
                    image[24].Position = new Vector2(300, -50);
                    image[23].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                    image[24].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                }
            }
            if (i == 3 && image[i].Position.Y >= -20)
            {
                leaveScreen(gameTime, i);
                if (image[i].Position.Y < 150)
                {
                    current = 8;
                }
            }
            if (i == 7 && image[i].Position.Y >= -20)
            {
                leaveScreen(gameTime, i);
                if (image[i].Position.Y < 150)
                {
                    current = 12;
                }
            }
            if (i == 11 && image[i].Position.Y >= -20)
            {
                leaveScreen(gameTime, i);
                if (image[i].Position.Y < 150)
                {
                    current = 16;
                }
            }

            if (i == 15 && image[i].Position.Y >= -20)
            {
                leaveScreen(gameTime, i);
                if (image[i].Position.Y < 150)
                {
                    current = 20;
                }
            }

            if (i == 19 && image[i].Position.Y >= -20)
            {
                leaveScreen(gameTime, i);
                if (image[i].Position.Y < -20)
                {
                    current = 1;
                    foward = true;
                    time = 1;
                    // wave = 4;
                    for (int j = 0; j < 20; j++)
                    {
                        image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                        image[j].Position = new Vector2(r.Next(50, 450), -20);
                    }
                }
            }
        }

        private void enterScreen(GameTime gameTime, int i)
        {
            if (i == 0)
            {
                image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 3].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void leaveScreen(GameTime gameTime, int i)
        {
            if (i == 0)
            {
                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[i].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[i + 1].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[i + 2].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (image[i].Position.Y < 120)
                {
                    image[i].Position.X -= Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 1].Position.X -= Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 2].Position.X += Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 1].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 2].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[i].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[i + 1].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[i + 2].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 3].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[i + 3].Position.Y -= Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (image[i].Position.Y < 120)
                {
                    image[i].Position.X -= Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 1].Position.X -= Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 2].Position.X += Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 3].Position.X += Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 1].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 2].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 3].Position.Y -= Velocity.Y = MoveSpeed / 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        private void setPositionWave3()
        {
            image[0].Position = new Vector2(200, -20);
            image[1].Position = new Vector2(250, -20);
            image[2].Position = new Vector2(300, -20);

            image[3].Position = new Vector2(175, -20);
            image[4].Position = new Vector2(225, -20);
            image[5].Position = new Vector2(275, -20);
            image[6].Position = new Vector2(325, -20);

            image[7].Position = new Vector2(150, -20);
            image[8].Position = new Vector2(200, -20);
            image[9].Position = new Vector2(300, -20);
            image[10].Position = new Vector2(350, -20);

            image[11].Position = new Vector2(125, -20);
            image[12].Position = new Vector2(175, -20);
            image[13].Position = new Vector2(325, -20);
            image[14].Position = new Vector2(375, -20);

            image[15].Position = new Vector2(100, -20);
            image[16].Position = new Vector2(150, -20);
            image[17].Position = new Vector2(350, -20);
            image[18].Position = new Vector2(400, -20);

            image[19].Position = new Vector2(75, -20);
            image[20].Position = new Vector2(125, -20);
            image[21].Position = new Vector2(375, -20);
            image[22].Position = new Vector2(425, -20);
        }


        private void waveTwo(GameTime gameTime, int i)
        {
            // Big enemy.
            if (image[24].Position.Y <= 150)
            {
                image[24].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                image[24].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[24].Position.Y += Velocity.Y = MoveSpeed / 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[24].Position.X += Velocity.X = MoveSpeed / 17 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (i < 10)
            {

                if (foward == true)
                {
                    fowardMove(gameTime, i);
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer > 3)
                    {
                        if (bf[i] == false && image[i].Position.Y > -20)
                        {
                            backMove(gameTime, i);
                        }
                        else if (bf[i] == false)
                        {
                            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                            if (i < 5 && image[i].Position.X > 100)
                            {
                                image[i].Position.X = 100;
                                image[i].Position.Y = -155;
                            }
                            else if ((i < 10 && image[i].Position.X > 150) || i > 7)
                            {
                                image[i].Position.X = 150;
                                image[i].Position.Y = -40;
                            }
                        }

                        if (time == 0 && i + 10 > 9 && i + 10 < 15 && image[i + 10].Position.X < 0)
                        {

                            exit[i + 10] = true;
                            image[i + 10].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                            image[i + 10].Position.Y = -40;
                            image[i + 10].Position.X = 300;
                            if (exit[14] == true)
                            {
                                time = 1;
                            }
                        }

                        if (exit[i + 10] == false && image[i + 10].Position.Y <= 250)
                        {
                            image[i + 10].Position.Y += 3f;
                            if (image[i + 10].Position.Y > 200)
                            {
                                image[i + 10].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                                image[i + 10].Position.X -= 3.5f;
                            }
                        }
                        else
                        {
                            bf[i] = true;
                            if (exit[i + 10] == false && image[i + 10].Position.Y > 250)
                            {
                                leftMove(gameTime, i + 10);
                            }
                            else if (exit[i + 10] == true && image[i + 10].Position.Y <= 250)
                            {
                                if (image[19].Position.Y > 50 && (i == 0 || image[10].Position.Y > 250 || (i != 0 && image[i + 9].Position.Y <= 250 && image[i + 9].Position.Y - image[i + 10].Position.Y >= 80)))
                                {
                                    image[i + 10].Position.Y += 3f;
                                    if (image[i + 10].Position.Y > 200)
                                    {
                                        image[i + 10].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                                        image[i + 10].Position.X -= 3.5f;
                                    }
                                }
                            }
                            else if (exit[i + 10] == true && image[i + 10].Position.Y > 250)
                            {
                                leftMove(gameTime, i + 10);
                                if (image[i + 10].Position.X < 0)
                                {
                                    exit[i + 10] = false;
                                }
                            }
                            if (time == 1 && i < 5 && image[i].Position.X > 500)
                            {
                                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                                image[i].Position.X = 225;
                                image[i].Position.Y = -200;
                                if (i == 4)
                                {
                                    time = 2;
                                }
                            }

                            if (i < 10 && image[i].Position.Y <= 250)
                            {
                                if (i > 4)
                                {
                                    if ((time == 3) || (time == 2) ||
                                        (time == 2 && i == 9) ||
                                        image[4].Position.Y > 50 && (i == 5 || image[5].Position.Y > 250 || (i != 5 && image[i - 1].Position.Y <= 250 && image[i - 1].Position.Y - image[i].Position.Y >= 80)))
                                    {
                                        image[i].Position.Y += 3f;
                                    }
                                }
                                else
                                {
                                    image[i].Position.Y += 3f;
                                }


                                if (image[i].Position.Y > 200)
                                {
                                    image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                                    image[i].Position.X += 3.5f;
                                }
                            }
                            else
                            {
                                rightMove(gameTime, i);
                                // Big enemy
                                if (image[23].Position.Y <= 130)
                                {
                                    image[23].Position.Y += Velocity.Y = MoveSpeed / 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                }
                                else
                                {
                                    image[23].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                                    image[23].Position.Y += Velocity.Y = MoveSpeed / 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                    image[23].Position.X -= Velocity.X = MoveSpeed / 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                }
                                if (time == 2 && i < 5 && image[i].Position.X > 500)
                                {
                                    image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                                    image[i + 5].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                                    image[i].Position.X = 100;
                                    image[i].Position.Y = -250;
                                    image[i + 5].Position.X = 400;
                                    image[i + 5].Position.Y = -700;
                                    if (time == 2 && i == 4)
                                    {
                                        time = 3;
                                    }
                                }
                                else if (time == 3 && image[9].Position.X > 500)
                                {
                                    time = 0;
                                    wave = 3;

                                    setPositionWave3();
                                    current = 1;
                                    foward = true;
                                    timer = 0;
                                    for (int j = 0; j < 23; j++)
                                    {
                                        bf[j] = false;
                                        image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                                    }

                                }


                            }
                        }

                    }
                }
            }
        }




        private void backMove(GameTime gameTime, int i)
        {
            if (foward == false && image[i].Position.Y > -20)
            {
                image[i].Position.Y += Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (wave == 1)
                {
                    if (i < 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                    }
                    else if (i == 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 3;
                    }
                    else if (i == 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 3;
                    }
                    else if (i > 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                    }

                    if (i < 4)
                    {
                        image[i].Position.X += -0.25f;
                    }
                    else if (i == 4)
                    {
                        image[i].Position.X += -0.12f;
                    }
                    else if (i == 5)
                    {
                        image[i].Position.X += 0.12f;
                    }
                    else if (i > 5)
                    {
                        image[i].Position.X += 0.25f;
                    }
                }
                else if (wave == 2)
                {
                    if (i > 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                    }
                    else if (i == 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 3;
                    }
                    else if (i == 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 3;
                    }
                    else if (i < 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                    }
                    if (i < 4)
                    {
                        image[i].Position.X += 0.25f;
                    }
                    else if (i == 4)
                    {
                        image[i].Position.X += 0.12f;
                    }
                    else if (i == 5)
                    {
                        image[i].Position.X -= 0.12f;
                    }
                    else if (i > 5)
                    {
                        image[i].Position.X -= 0.25f;
                    }
                }
            }

            if (image[current - 1].Position.Y < 90)
            {
                if (current < 10)
                {
                    current++;
                }
            }
            if (wave == 1 && image[current - 1].Position.Y < -20)
            {
                current = 11;
                foward = true;
                timer = 1;
                enemyMovePath = 1;
                int begin = 0;
                if (wave == 1)
                {
                    for (int j = 9; j >= 0; j--)
                    {
                        begin = begin + 50;
                        image[j].Position = new Vector2(begin, -20);
                    }
                }
            }
            /*
            else if (wave == 2 && image[i].Position.Y < 0)
            {
                foward = true;
                if (i < 5)
                {
                    image[i].Position = new Vector2(50, -20);
                }
                else if (i < 10)
                {
                    image[i].Position = new Vector2(100, -20);
                }
            }*/

        }



        private void waveOne(GameTime gameTime, int i)
        {
            if (enemyMovePath == 0 && i < 10)
            {
                if (foward == true)
                {
                    //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    fowardMove(gameTime, i);
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer > 3)
                    {
                        backMove(gameTime, i);
                    }
                }
            }

            else if (enemyMovePath == 1 && i >= 10)
            {
                fowardStraightMove(gameTime, i);
                // Big enemy
                if (image[23].Position.Y <= 150)
                {
                    image[23].Position.Y += Velocity.Y = MoveSpeed / 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    image[23].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                    image[23].Position.Y += Velocity.Y = MoveSpeed / 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[23].Position.X -= Velocity.X = MoveSpeed / 33 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (image[i].Position.Y >= 250)
                {
                    if (firstOrSecond == 1)
                    {
                        if (i < 15)
                        {
                            rightMove(gameTime, i);
                        }
                        else
                        {
                            leftMove(gameTime, i);
                        }
                    }
                    else if (firstOrSecond == 2)
                    {
                        if (i < 15)
                        {
                            leftMove(gameTime, i);
                        }
                        else
                        {
                            rightMove(gameTime, i);
                        }
                    }

                }
            }
        }

        private void fowardStraightMove(GameTime gameTime, int i)
        {
            if (i < 20 && image[i].Position.Y <= 250)
            {
                if (image[i].Position.Y > 230)
                {
                    if (switchSide == 1)
                    {
                        if (i < 15)
                        {
                            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                            image[i].Position.X += Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else
                        {
                            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                            image[i].Position.X -= Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                    else if (switchSide == 2)
                    {
                        if (i < 15)
                        {
                            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                            image[i].Position.X -= Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else
                        {
                            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                            image[i].Position.X += Velocity.X = MoveSpeed / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }

                image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (current < 20 && image[current - 1].Position.Y > 20)
            {
                if (current != 15)
                {
                    current++;
                }
                else if ((switchSide == 1 && image[10].Position.X > 400) || (switchSide == 2 && image[10].Position.X < 200))
                {
                    current = 16;
                }
            }
            if (image[19].Position.X < 0)
            {
                for (int j = 10; j < 20; j++)
                {
                    image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                    if (j >= 15)
                    {
                        image[j].Position = new Vector2(50, -20);
                    }
                    else
                    {
                        image[j].Position = new Vector2(450, -20);
                    }
                }
                firstOrSecond = 2;
                current = 11;
                switchSide = 2;
            }
            else if (image[19].Position.X > 500)
            {
                for (int j = 10; j < 20; j++)
                {
                    image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                    if (j >= 15)
                    {
                        image[j].Position = new Vector2(350, -20);
                    }
                    else
                    {
                        image[j].Position = new Vector2(450, -20);
                    }
                }
                firstOrSecond = 2;
                current = 1;
                image[23].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                image[23].Position = new Vector2(350, -500);
                wave = 2;
                enemyMovePath = 0;
            }
        }

        private void leftMove(GameTime gameTime, int i)
        {
            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
            image[i].Position.X += Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void rightMove(GameTime gameTime, int i)
        {
            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
            image[i].Position.X += Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void fowardMove(GameTime gameTime, int i)
        {
            if (foward == true && image[i].Position.Y <= 180)
            {

                image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (wave == 1)
                {
                    if (i < 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                        image[i].Position.X += 0.25f;
                    }
                    else if (i == 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                        image[i].Position.X += 0.12f;
                    }
                    else if (i == 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                        image[i].Position.X -= 0.12f;
                    }
                    else if (i > 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                        image[i].Position.X -= 0.25f;
                    }
                    if (image[i].Position.Y > 180)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                    }
                }
                else if (wave == 2)
                {
                    if (i < 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                        image[i].Position.X -= 0.25f;
                    }
                    else if (i == 4)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                        image[i].Position.X -= 0.12f;
                    }
                    else if (i == 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                        image[i].Position.X += 0.12f;
                    }
                    else if (i > 5)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                        image[i].Position.X += 0.25f;
                    }
                    if (image[i].Position.Y > 180)
                    {
                        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
                    }
                }

            }




            if (image[current - 1].Position.Y > 90)
            {
                if (current < 10)
                {
                    current++;
                }
            }



            if (image[current - 1].Position.Y > 180)
            {
                current = 1;
                foward = false;
                timer = 0;
            }



        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 27; i++)
            {
                image[i].Draw(spriteBatch);
            }
        }
    }
}
/*

 if (leftorRight == false)
            {            
                if (image[i].Position.Y > 250)
                {
                    image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                    image[i].Position.X += Velocity.X = MoveSpeed* (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (image[current - 1].Position.Y > 20)
                {
                    current++;
                    if(current == 6)
                    {
                        current = 1;
                    }
                }
                if(image[4].Position.X > 500)
                {
                    leftorRight = true;
                    current = 6;
                }
            }
            else
            {
                if (image[i].Position.Y > 250)
                {
                    image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                    image[i].Position.X += Velocity.X = -MoveSpeed* (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (image[current - 1].Position.Y > 20)
                {
                    current++;
                    if (current == image.Count)
                    {
                        current = 6;
                    }
                }
                if (image[image.Count].Position.X< 0)
                {
                    leftorRight = false;
                    current = 1;
                }
            }*/


/*
       else if (image[image.Count - 1].Position.X > 500)
       {
           for (int j = 11; j < image.Count; j++)
           {
               image[j].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
               if (j < 15)
               {
                   image[j].Position = new Vector2(50, -20);
               }
               else
               {
                   image[j].Position = new Vector2(450, -20);
               }
           }
           current = 10;
           firstOrSecond = 1;
       }*/

/*
if (image[i].Position.Y > 180)
{
    if (angle >= Math.PI / 2)
    {
        angle = 0;
    }
    float nextX = (float)(1 + Math.Sqrt(2) * Math.Cos(angle = angle + 0.1f));
    float nextY = (float)(1 + Math.Sqrt(2) * Math.Sin(angle = angle + 0.1f));
    if (firstOrSecond == 1)
    {
        if (i < 15)
        {
            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
            image[i].Position.X += nextX;
        }
        else
        {
            image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
            image[i].Position.X -= nextX;
        }
    }
    image[i].Position.Y += nextY;
}
}
/*
if (image[i].Position.Y <= 250)
{
    if (image[i].Position.Y > 180)
    {
        if (angle >= Math.PI / 2)
        {
            angle = 0;
        }
        float nextX = (float)(1 + Math.Sqrt(2) * Math.Cos(angle = angle + 0.1f));
        float nextY = (float)(1 + Math.Sqrt(2) * Math.Sin(angle = angle + 0.1f));
        if (firstOrSecond == 1)
        {
            if (i < 15)
            {
                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[i].Position.X += nextX;
            }
            else
            {
                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[i].Position.X -= nextX;
            }
        }
        else if (firstOrSecond == 2)
        {
            if (i < 15)
            {
                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 1;
                image[i].Position.X -= nextX;
            }
            else
            {
                image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 2;
                image[i].Position.X += nextX;
            }
        }
        image[i].Position.Y += nextY;
    }
    else
    {
        image[i].SpriteSheetEffectEnemy.CurrentFrame.Y = 0;
        image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}
if (firstOrSecond == 1)
{
    if (image[14].Position.Y > 0 && image[14].Position.X < 100)
    {
        current = 14;
    }
    else
    {
        switchSide = 2;
    }
}
else
{
    if (image[14].Position.Y > 0 && image[14].Position.X > 400)
    {
        current = 14;
    }
    else
    {
        switchSide = 2;
    }
}

if (image[current - 1].Position.Y > 20 && current < image.Count)
{
    if (current != 15 || switchSide == 2)
    {
        current++;
    }
}
/*
if (image[image.Count - 1].Position.X < 0 || image[image.Count - 1].Position.X > 500)
{
    if (firstOrSecond == 1)
    {
        for (int j = 10; j < 15; j++)
        {
            image[j].Position = new Vector2(450, -20);
        }
        for (int j = 15; j < image.Count; j++)
        {
            image[j].Position = new Vector2(50, -20);
        }
        firstOrSecond = 2;
    }
    else
    {
        enemyMovePath = 0;
        int begin = 0;
        firstOrSecond = 1;
        for (int j = 10; j < 15; j++)
        {
            image[j].Position = new Vector2(100, -20);
        }
        for (int j = 15; j < 20; j++)
        {
            image[j].Position = new Vector2(400, -20);
        }
    }
    switchSide = 1;
    current = 11;
}
*/
