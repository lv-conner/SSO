using System;
using System.Collections.Generic;
using System.Collections;


namespace SSO.IRepository
{
    public interface IRepository<T> where T: class
    {
        T Query(string id);
        IEnumerable<T> QueryAll();
        
    }
}
