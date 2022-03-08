using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;
using Plantjes.Dao;

namespace Plantjes.ViewModels
{
    internal class ViewModelAdd : ViewModelBase
    {
        private readonly DAOLogic dao;

        public ViewModelAdd()
        {
            dao = DAOLogic.Instance();
        }
    }
}
