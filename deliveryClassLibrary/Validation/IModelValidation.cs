using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Validation
{
    public interface IModelValidation<T>
    {
        bool IsValid(T model);
    }
}
