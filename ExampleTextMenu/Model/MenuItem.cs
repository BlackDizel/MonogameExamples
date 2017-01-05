using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTextMenu.Model
{
    class MenuItem<T> where T : IMenuItem
    {
        private T data;
        private List<MenuItem<T>> childs;
        private MenuItem<T> parent;
        private static readonly string field_data = "data";
        private static readonly string field_childs = "childs";

        public T Data { get { return data; } }
        public List<MenuItem<T>> Childs { get { return childs; } }
        public MenuItem<T> Parent { get { return parent; } }

        private void setData(T data, List<MenuItem<T>> childs, MenuItem<T> parent)
        {
            this.data = data;
            this.childs = childs;
            this.parent = parent;
        }

        internal static MenuItem<T> deserealize(MenuItem<T> parent, string jsonString)
        {
            MenuItem<T> result = new MenuItem<T>();

            JObject jo = JObject.Parse(jsonString);

            T data = jo[field_data].ToObject<T>();
            List<MenuItem<T>> childs = parseChilds(result, jo[field_childs]);
            result.setData(data, childs, parent);

            return result;
        }

        private static List<MenuItem<T>> parseChilds(MenuItem<T> parent, JToken jToken)
        {
            if (jToken == null) return null;

            List<MenuItem<T>> result = new List<MenuItem<T>>();

            JArray jarray = jToken as JArray;
            if (jarray == null) return null;
            foreach (JToken jitem in jarray){
                var item = deserealize(parent, jitem.ToString());
                result.Add(item);
            }

            return result.Count == 0 ? null : result;

        }
    }
}
