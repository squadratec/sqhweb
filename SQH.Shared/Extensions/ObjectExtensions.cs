using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;

namespace SQH.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToSerialize<T>(this T requisitor)
        {
            var dados = HttpUtility.ParseQueryString(string.Empty);

            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(requisitor))
            {
                string value = propertyDescriptor.GetValue(requisitor).ToString();
                dados.Add(propertyDescriptor.Name, value);
            }
            return dados.ToString();
        }

        public static byte[] ToSerializeBytes<T>(this T requisitor)
        {
            return Encoding.UTF8.GetBytes(requisitor.ToSerialize());
        }

        public static T2 CopiarPara<T1, T2>(this T1 refrencia, T2 instancia) where T2 : new()
        {
            var propriedades = typeof(T1).GetProperties();

            foreach (var p in propriedades)
            {
                var p2 = typeof(T2).GetProperty(p.Name);

                if (p2 != null && p2.PropertyType.Equals(p.PropertyType))
                    p2.SetValue(instancia, p.GetValue(refrencia), null);
            }
            return instancia;
        }

        public static IEnumerable<T2> CopiarListaPara<T1, T2>(this IEnumerable<T1> refrencia, IEnumerable<T2> instancia) where T2 : new()
        {
            foreach (var item in refrencia)
                yield return item.CopiarPara(new T2());
        }
    }
}
