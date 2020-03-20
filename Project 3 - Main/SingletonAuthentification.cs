using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public sealed class SingletonAuthentification
    {
        private static SingletonAuthentification instance = null;

        private SingletonAuthentification()
        { }        

        public static SingletonAuthentification GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonAuthentification();
                }
                return instance;
            }
        }
    }
}
