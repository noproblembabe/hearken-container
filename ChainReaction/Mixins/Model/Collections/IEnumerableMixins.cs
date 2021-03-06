﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;

namespace ChainReaction.Mixins.Model.Collections
{
    public static class IEnumerableMixins
    {
        /// <summary>
        /// Retrieves the first item, of a given enumerabled object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [TargetedPatchingOptOut("Performance needed")]
        public static T Foremost<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            foreach (var item in self)
            {
                if (predicate(item))
                { return item; }
            }
            return default(T);
        }

        /// <summary>
        /// Retrieves the first item, of a given enumerabled object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [TargetedPatchingOptOut("Performance needed")]
        public static KeyValuePair<int, T> Foremost<T>(this IEnumerable<T> self, Func<int, T, bool> predicate)
        {
            int i = 0;
            var value = 
                self.Foremost(item => predicate(i++, item));
            return new KeyValuePair<int, T>(i, value);
        }

        /// <summary>
        /// Applies to each item, of a given enumerabled object, an action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="do"></param>
        [TargetedPatchingOptOut("Performance needed")]
        public static void Each<T>(this IEnumerable<T> self, Action<int, T> @do)
        {
            int i = 0;
            foreach(var item in self)
            {
                @do(i++, item);
            }
        }

        /// <summary>
        /// Applies to each item, of a given enumerabled object, an action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="do"></param>
        [TargetedPatchingOptOut("Performance needed")]
        public static void Each<T>(this IEnumerable<T> self, Action<T> @do)
        {
            self.Each((i, item) => @do(item));
        }

        /// <summary>
        /// Selects a portion of a given enumerabled object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [TargetedPatchingOptOut("Performance needed")]
        public static IEnumerable<T> Which<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            foreach (var item in self)
            {
                if(predicate(item))
                { yield return item; }
            }
        }        

        /// <summary>
        /// Counts a given enumerabled object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        [TargetedPatchingOptOut("Performance needed")]
        public static int Size<T>(this IEnumerable<T> self)
        {
            return Size(self, null);
        }

        /// <summary>
        /// Counts a portion of a given enumerabled object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [TargetedPatchingOptOut("Performance needed")]
        public static int Size<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            int i = 0;
            foreach (var item in self)
            {
                if (predicate == null) { i++; } 
                if (predicate(item)) { i++; }
            }
            return i;
        }
    }
}
