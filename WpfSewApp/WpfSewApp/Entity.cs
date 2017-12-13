using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSewApp
{
    public static class Entity
    {
        private static EntitiesSew entities = new EntitiesSew();
        private static Пользователь user = null;

        public static EntitiesSew Entities { get => entities; }
        public static Пользователь User { get => user; set => user = value; }
    }
}
