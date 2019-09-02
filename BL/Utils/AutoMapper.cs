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
        public static B MapObject(Func<int, A> z,int temp)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(z.Invoke(temp));
        }

        public static B MapObject(Func<A> z)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(z.Invoke());
        }

        public static B MapObject(Func<string, A> z, string temp)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(z.Invoke(temp));
        }

        public static B MapObject(A a)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<A, B>(a);
        }

        public static IEnumerable<B> MapList(Func<IEnumerable<A>> z)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<IEnumerable<A>, IEnumerable<B>>(z.Invoke());
        }

        public static IEnumerable<B> MapList(Func<int,IEnumerable<A>> z, int temp)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<IEnumerable<A>, IEnumerable<B>>(z.Invoke(temp));
        }

        public static IEnumerable<B> MapList(Func<string, IEnumerable<A>> z, string temp)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<IEnumerable<A>, IEnumerable<B>>(z.Invoke(temp));
        }

        public static IEnumerable<B> MapList(IEnumerable<A> z)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<A, B>()).CreateMapper();
            return mapper.Map<IEnumerable<A>, IEnumerable<B>>(z);
        }
    }
}
