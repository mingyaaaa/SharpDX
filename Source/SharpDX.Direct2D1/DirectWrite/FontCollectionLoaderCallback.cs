// Copyright (c) 2010-2011 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpDX.DirectWrite
{
    /// <summary>
    /// Internal FontCollectionLoader Callback
    /// </summary>
    internal class FontCollectionLoaderCallback : SharpDX.ComObjectCallbackNative
    {
        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>The callback.</value>
        public FontCollectionLoader Callback { get; private set; }

        private Factory _factory;

        public static IntPtr CallbackToPtr(FontCollectionLoader fontFileEnumerator)
        {
            return CallbackToPtr<FontCollectionLoader, FontCollectionLoaderCallback>(fontFileEnumerator);
        }

        public static IntPtr CallbackToPtr(Factory factory, FontCollectionLoader fontFileEnumerator)
        {
            var callback = QueryCallback<FontCollectionLoader, FontCollectionLoaderCallback>(fontFileEnumerator);
            callback._factory = factory;
            return callback.NativePointer;
        }

        public override void Attach<T>(T callback)
        {
            Attach(callback, 1);
            Callback = (FontCollectionLoader)callback;
            AddMethod(new CreateEnumeratorFromKeyDelegate(CreateEnumeratorFromKeyImpl));
        }
        

        /// <unmanaged>HRESULT IDWriteFontCollectionLoader::CreateEnumeratorFromKey([None] IDWriteFactory* factory,[In, Buffer] const void* collectionKey,[None] int collectionKeySize,[Out] IDWriteFontFileEnumerator** fontFileEnumerator)</unmanaged>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int CreateEnumeratorFromKeyDelegate(
            IntPtr thisPtr, IntPtr factory, IntPtr collectionKey, int collectionKeySize, out IntPtr fontFileEnumerator);
        private int CreateEnumeratorFromKeyImpl(IntPtr thisPtr, IntPtr factory, IntPtr collectionKey, int collectionKeySize, out IntPtr fontFileEnumerator)
        {
            fontFileEnumerator = IntPtr.Zero;
            try
            {
                Debug.Assert(factory == _factory.NativePointer);
                var enumerator = Callback.CreateEnumeratorFromKey(_factory, new DataStream(collectionKey, collectionKeySize, true, true));
                fontFileEnumerator = FontFileEnumeratorCallback.CallbackToPtr(enumerator);
            }
            catch (SharpDXException exception)
            {
                return exception.ResultCode.Code;
            }
            catch (Exception)
            {
                return Result.Fail.Code;
            }
            return Result.Ok.Code;
        }

    }
}