using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utils
{
    public static class AutoMapper<A,B>
    {
        public static B Map(Func<int, A> z,int temp)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(z.Invoke(temp));
        }

        public static B Map(Func<A> z)
        {
            if (typeof(A).GetGenericArguments().Length > 0) {
                Type x = typeof(A).GetGenericArguments()[0];
                Type y = typeof(B).GetGenericArguments()[0];
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap(x,y)).CreateMapper();
                return mapper.Map<A, B>(z.Invoke());
            }else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap <A, B > ()).CreateMapper();
                return mapper.Map<A, B>(z.Invoke());
            }
        }

        public static B Map(Func<string, A> z, string temp)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(z.Invoke(temp));
        }

        public static B Map(A a)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(a);
        }
    }
}
