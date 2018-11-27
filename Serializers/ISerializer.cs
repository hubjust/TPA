using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Serializers
{
    public interface ISerializer
    {
        void Serialize(DataContext dataContext);
    }
}
