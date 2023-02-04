using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.SaveSystem
{
    public class SaveSystem
    {
        // Singleton init
        private static readonly Lazy<SaveSystem> _instance = new Lazy<SaveSystem>(() => new SaveSystem());
        private SaveSystem() { }
        public static SaveSystem Instance { get { return _instance.Value; } }

        public void Save(ISaveSystemType saveSystemType, object objectToSave)
        {
            saveSystemType.Save(objectToSave);
        }

        public object Load(ISaveSystemType saveSystemType)
        {
            return saveSystemType.Load();
        }
    }
}
