using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.UserInterface
{
    internal interface IUserInterface: IReadUserInterface, IWriteUserInterface
    {
        void Run();
    }
}
