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
using SharpDX;

namespace SharpDX.Direct3D10
{
    public partial class Font
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="fontDescription">The font description.</param>
        public Font(Device device, FontDescription fontDescription) : base(IntPtr.Zero)
        {
            D3DX10.CreateFontIndirect(device, ref fontDescription, this);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="mipLevels">The mip levels.</param>
        /// <param name="isItalic">if set to <c>true</c> [is italic].</param>
        /// <param name="characterSet">The character set.</param>
        /// <param name="precision">The precision.</param>
        /// <param name="quality">The quality.</param>
        /// <param name="pitchAndFamily">The pitch and family.</param>
        /// <param name="faceName">Name of the face.</param>
        public Font(Device device, int height, int width, FontWeight weight, int mipLevels, bool isItalic, FontCharacterSet characterSet, FontPrecision precision, FontQuality quality, FontPitchAndFamily pitchAndFamily, string faceName)
        {
            D3DX10.CreateFont(device, height, width, (int) weight, mipLevels, isItalic, (int) characterSet, (int) precision, (int) quality, (int) pitchAndFamily,
                              faceName, this);
        }

        /// <summary>	
        /// Load formatted text into video memory to improve the efficiency of rendering to the device. This method supports ANSI and Unicode strings.	
        /// </summary>	
        /// <remarks>	
        /// The compiler setting also determines the function version. If Unicode is defined, the function call resolves to PreloadTextW. Otherwise, the function call resolves to PreloadTextA because ANSI strings are being used. This method generates textures that contain glyphs that represent the input text. The glyphs are drawn as a series of triangles. Text will not be rendered to the device; ID3DX10Font::DrawText must still be called to render the text. However, by preloading text into video memory, ID3DX10Font::DrawText will use substantially fewer CPU resources. This method internally converts characters to glyphs using the GDI function {{GetCharacterPlacement}}. 	
        /// </remarks>	
        /// <param name="stringRef">Pointer to a string of characters to be loaded into video memory. If the compiler settings require Unicode, the data type LPCTSTR resolves to LPCWSTR; otherwise, the data type resolves to LPCSTR. See Remarks. </param>
        /// <returns>If the method succeeds, the return value is S_OK. If the method fails, the return value can be one of the following: D3DERR_INVALIDCALL, D3DXERR_INVALIDDATA. </returns>
        /// <unmanaged>HRESULT ID3DX10Font::PreloadTextW([None] const wchar_t* pString,[None] int Count)</unmanaged>
        public SharpDX.Result PreloadText(string stringRef)
        {
            return PreloadText(stringRef, stringRef.Length);
        }

        /// <summary>	
        /// Draw formatted text. This method supports ANSI and Unicode strings.	
        /// </summary>	
        /// <remarks>	
        /// The parameters of this method are very similar to those of the {{GDI DrawText}} function. This method supports both ANSI and Unicode strings. Unless the DT_NOCLIP format is used, this method clips the text so that it does not appear outside the specified rectangle. All formatting is assumed to have multiple lines unless the DT_SINGLELINE format is specified. If the selected font is too large for the rectangle, this method does not attempt to substitute a smaller font. This method supports only fonts whose escapement and orientation are both zero. 	
        /// </remarks>	
        /// <param name="sprite">Reference to an ID3DX10Sprite object that contains the string you wish to draw. Can be NULL, in which case Direct3D will render the string with its own sprite object. To improve efficiency, a sprite object should be specified if ID3DX10Font::DrawText is to be called more than once in a row. </param>
        /// <param name="text">Pointer to a string to draw. If UNICODE is defined, this parameter type resolves to an LPCWSTR, otherwise, the type resolves to an LPCSTR. If the Count parameter is -1, the string must be NULL terminated. </param>
        /// <param name="rect">Pointer to a <see cref="SharpDX.Rectangle"/> structure that contains the rectangle, in logical coordinates, in which the text is to be formatted. As with any RECT object, the coordinate value of the rectangle's right side must be greater than that of its left side. Likewise, the coordinate value of the bottom must be greater than that of the top. </param>
        /// <param name="drawFlags">Specify the method of formatting the text. It can be any combination of the following values:    ItemDescription  DT_BOTTOM  Justify the text to the bottom of the rectangle. This value must be combined with DT_SINGLELINE.   DT_CALCRECT  Tell DrawText to automatically calculate the width and height of the rectangle based on the length of the string you tell it to draw. If there are multiple lines of text, ID3DX10Font::DrawText uses the width of the rectangle pointed to by the pRect parameter and extends the base of the rectangle to bound the last line of text. If there is only one line of text, ID3DX10Font::DrawText modifies the right side of the rectangle so that it bounds the last character in the line. In either case, ID3DX10Font::DrawText returns the height of the formatted text but does not draw the text.   DT_CENTER  Center text horizontally in the rectangle.   DT_EXPANDTABS  Expand tab characters. The default number of characters per tab is eight.   DT_LEFT  Align text to the left.   DT_NOCLIP  Draw without clipping. ID3DX10Font::DrawText is somewhat faster when DT_NOCLIP is used.   DT_RIGHT  Align text to the right.   DT_RTLREADING  Display text in right-to-left reading order for bidirectional text when a Hebrew or Arabic font is selected. The default reading order for all text is left-to-right.   DT_SINGLELINE  Display text on a single line only. Carriage returns and line feeds do not break the line.   DT_TOP  Top-justify text.   DT_VCENTER  Center text vertically (single line only).   DT_WORDBREAK  Break words. Lines are automatically broken between words if a word would extend past the edge of the rectangle specified by the pRect parameter. A carriage return/line feed sequence also breaks the line.   ? </param>
        /// <param name="color">Color of the text. See <see cref="SharpDX.Color4"/>. </param>
        /// <returns>If the function succeeds, the return value is the height of the text in logical units. If DT_VCENTER or DT_BOTTOM is specified, the return value is the offset from pRect (top to the bottom) of the drawn text. If the function fails, the return value is zero. </returns>
        /// <unmanaged>int ID3DX10Font::DrawTextW([None] LPD3DX10SPRITE pSprite,[None] const wchar_t* pString,[None] int Count,[None] RECT* pRect,[None] int Format,[None] D3DXCOLOR Color)</unmanaged>
        public int DrawText(SharpDX.Direct3D10.Sprite sprite, string text, SharpDX.Rectangle rect, FontDrawFlags drawFlags, SharpDX.Color4 color)
        {
            int value = DrawText(sprite, text, text.Length, rect, (int) drawFlags, color);
            if (value == 0)
                throw new SharpDXException("Draw failed");
            return value;
        }

        /// <summary>
        /// Measures the specified sprite.
        /// </summary>
        /// <param name="sprite">Reference to an ID3DX10Sprite object that contains the string you wish to draw. Can be NULL, in which case Direct3D will render the string with its own sprite object. To improve efficiency, a sprite object should be specified if ID3DX10Font::DrawText is to be called more than once in a row. </param>
        /// <param name="text">A string to measure. </param>
        /// <param name="rect">A <see cref="SharpDX.Rectangle"/> structure that contains the rectangle, in logical coordinates, in which the text is to be formatted. As with any RECT object, the coordinate value of the rectangle's right side must be greater than that of its left side. Likewise, the coordinate value of the bottom must be greater than that of the top.</param>
        /// <param name="drawFlags">Specify the method of formatting the text.</param>
        /// <returns></returns>
        public SharpDX.Rectangle Measure(Sprite sprite, string text, SharpDX.Rectangle rect, FontDrawFlags drawFlags)
        {
            // DT_CALCRECT
            DrawText(sprite, text, text.Length, rect, ((int) drawFlags)|0x400, new Color4(1, 1, 1, 1));
            return rect;
        }
    }
}