﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Common
{
    /// <summary>
    /// Provides an implementation of the <see cref="BaseDisposable"/> with a supressed finalizer.
    /// </summary>
    /// <remarks>
    /// <see href="http://stackoverflow.com/questions/18020861/how-to-get-notified-before-static-variables-are-finalized/18316325#18316325">StackOverflow</see>, <see href="http://stackoverflow.com/questions/8011001/can-anyone-explain-this-finalisation-behaviour">Also</see> some for details
    /// </remarks>
    public class SuppressedFinalizerDisposable : BaseDisposable
    {

        /// <summary>
        /// Should never run unless immediately finalized.
        /// </summary>
//        ~SuppressedFinalizerDisposable()
//        {            

//            Dispose(ShouldDispose);

//#if DEBUG
//            System.Diagnostics.Debug.WriteLine(ToString() + "@Finalize Completed");
//#endif
//        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SuppressedFinalizerDisposable(bool shouldDispose)
            : base(shouldDispose) //Suppress Finalize may not be called more than once without a matching Reregister
        {
            //Suppress the finalizer of this instance always.
            GC.SuppressFinalize(this);
        }

        //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        //internal protected override void Dispose(bool disposing)
        //{
        //    //If already disposed or disposing and should not dispose return.
        //    if (false.Equals(disposing)) return;

        //    base.Dispose(disposing);
        //}

        /// <summary>
        /// 
        /// </summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Resurrect() { int register = System.Threading.Thread.CurrentThread.ManagedThreadId; Resurrect(ref register); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="managedThreadId"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Resurrect(ref int managedThreadId)
        {
            //Need to retrieve the state from this instance.
            long state = 0;

            //Not already disposed or destructing?
            if (IsUndisposed.Equals(false) | BaseDisposable.IsDestructing(this, ref state).Equals(false)) return;

            //Check for a race condition or otherwise...
            if (managedThreadId.Equals(state >> 32))
            {
                //Ressurection is possible and likely to succeed context from whence it was marked disposed. 
            }

            ///
        }
    }
}