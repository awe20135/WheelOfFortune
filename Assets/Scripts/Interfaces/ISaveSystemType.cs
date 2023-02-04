using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Interfaces
{
    public interface ISaveSystemType
    {
        public void Save(object objectToSave);
        public object Load();
    }
}
