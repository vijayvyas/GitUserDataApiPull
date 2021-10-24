using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfWealthTest
{
    public interface ICacheService<T> where T:class
    {
        void updateCache(string key, T userModel);

        T getFromCache(string key);
    }
}
