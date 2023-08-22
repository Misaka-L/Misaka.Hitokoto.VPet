using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misaka.Hitokoto.VPet.Models
{
    public enum HitokotoType
    {
        /// <summary>
        /// 动画
        /// </summary>
        Animation,

        /// <summary>
        /// 漫画
        /// </summary>
        Comics,

        /// <summary>
        /// 游戏
        /// </summary>
        Game,

        /// <summary>
        /// 文学
        /// </summary>
        Literature,

        /// <summary>
        /// 原创
        /// </summary>
        Original,

        /// <summary>
        /// 来自互联网
        /// </summary>
        FromInternet,

        /// <summary>
        /// 其他
        /// </summary>
        Other,

        /// <summary>
        /// 影视
        /// </summary>
        Movies,

        /// <summary>
        /// 诗词
        /// </summary>
        Poem,

        /// <summary>
        /// 网易云
        /// </summary>
        NeteaseCloudMusic,

        /// <summary>
        /// 哲学
        /// </summary>
        Philosophy,

        /// <summary>
        /// 抖机灵
        /// </summary>
        Snarky
    }

    public static class HitokotoTypeExtenstion
    {
        public static string ToQueryString(this IEnumerable<HitokotoType> types)
        {
            var stringBuilder = new StringBuilder();
            var hitokotoTypes = types as HitokotoType[] ?? types.ToArray();

            for (var index = 0; index < hitokotoTypes.Length; index++)
            {
                var hitokotoType = hitokotoTypes[index];
                stringBuilder.AppendFormat("c={0}", hitokotoType.ToQueryString());

                if (index != hitokotoTypes.Length - 1)
                    stringBuilder.Append("&");
            }

            return stringBuilder.ToString();
        }

        public static HitokotoType FromReadableString(string type)
        {
            return type switch
            {
                "动画" => HitokotoType.Animation,
                "漫画" => HitokotoType.Comics,
                "游戏" => HitokotoType.Game,
                "文学" => HitokotoType.Literature,
                "原创" => HitokotoType.Original,
                "来自网络" => HitokotoType.FromInternet,
                "其他" => HitokotoType.Other,
                "影视" => HitokotoType.Movies,
                "诗词" => HitokotoType.Poem,
                "网易云" => HitokotoType.NeteaseCloudMusic,
                "哲学" => HitokotoType.Philosophy,
                "抖机灵" => HitokotoType.Snarky,
                _ => HitokotoType.Animation
            };
        }

        public static string ToReadableString(this HitokotoType type)
        {
            return type switch
            {
                HitokotoType.Animation => "动画",
                HitokotoType.Comics => "漫画",
                HitokotoType.Game => "游戏",
                HitokotoType.Literature => "文学",
                HitokotoType.Original => "原创",
                HitokotoType.FromInternet => "来自网络",
                HitokotoType.Other => "其他",
                HitokotoType.Movies => "影视",
                HitokotoType.Poem => "诗词",
                HitokotoType.NeteaseCloudMusic => "网易云",
                HitokotoType.Philosophy => "哲学",
                HitokotoType.Snarky => "抖机灵",
                _ => "其他"
            };
        }

        public static string ToQueryString(this HitokotoType type)
        {
            return type switch
            {
                HitokotoType.Animation => "a",
                HitokotoType.Comics => "b",
                HitokotoType.Game => "c",
                HitokotoType.Literature => "d",
                HitokotoType.Original => "e",
                HitokotoType.FromInternet => "f",
                HitokotoType.Other => "g",
                HitokotoType.Movies => "h",
                HitokotoType.Poem => "i",
                HitokotoType.NeteaseCloudMusic => "j",
                HitokotoType.Philosophy => "k",
                HitokotoType.Snarky => "l",
                _ => "a"
            };
        }
    }
}