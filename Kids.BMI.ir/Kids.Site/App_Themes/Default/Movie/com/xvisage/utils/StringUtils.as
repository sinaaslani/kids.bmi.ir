/**
 * version: 1.1
 * date: 01/26/2010
 * Actionscript 2.0
 * Flash Player 8
 * @author Ahmed Abbas, ahmed.abbas@ixd-consultant.com
 * Persian Developed by malekiciw@gmail.com ;)
 * requirements:
 
 	1. dynamic TextField
	2. HTML enabled TextField
	3. pre-defined TextFormat
	4. arabic fonts must include a complete Arabic Presentation Forms-B (glyphs from FE70 to FEFE according to the Unicode Standard 5.2)
		reference: http://www.unicode.org/charts/PDF/UFE70.pdf
 
 *
 * features supported:
 
 	1. embedding fonts (just put a dynamic textfield on-stage and select at least Basic Latin (95 glyphs) and Arabic (1088 glyphs) from the Character Embedding menu).
	2. arabic ligatures.
	3. word wrapping.
	4. bi-directional text.
	5. HTML Text.
	6. loading external text.
	7. Persian ligatures. ;)
 
 *
 * features not supported:
 
 	1. arabic diacritics.
 
 *
 * fixed bugs:
 
 	1. correct arabic ligatures with/without embedding fonts
	2. correct brackets directions
	3. clean line-breaks
 
 *
 * example:
 	
	import com.xvisage.utils.StringUtils;
	var format:TextFormat = new TextFormat();
	format.font = "Arial";
	format.size = 24;
	format.color = 0x0066CC;
	var output:TextField = this.createTextField("output", 1, 10, 10, Stage.width-20, format.size);
	output.autoSize = true;
	output.embedFonts = true;
	output.wordWrap  =true;
	output.multiline = true;
	output.html = true;
	var xml:XML = new XML();
	xml.ignoreWhite = true;
	xml.onLoad = function(done:Boolean) {
		if (done) {
			output.htmlText = StringUtils.parseArabic(this.firstChild.firstChild.nodeValue, output, format);
			output.setTextFormat(format);
		}
	}
	xml.load("persian.xml");
	
	
 
 **/
class com.xvisage.utils.StringUtils extends String {
	
	static function parseArabic(input:String, tag:TextField, format:TextFormat):String {
		var string:String = "";
		var arabicString:String = "";
		var htmlString:String = "";
		var mixedArray:Array = [];
		// strip hard lines-breaks
		input = input.split("\r").join("<br />");
		input = input.split("\n").join("<br />");
		// prepare brackets for later arabic re-order
		input = reverseBrackets(input);
		var chars:Array = input.split("");
		// remove diacritics from arabic characters (to enable diacritics we need to reconsider ligatures conditions after finding a way to render diacritics properly).
		chars = stripDiacritics(chars);
		var toggleHTML:Boolean = false;
		// strip HTML tags
		var i:Number = 0;
		for (i=0; i<chars.length; i++) {
			if (chars[i-1] == ">") {
				toggleHTML = false;
			}
			if (chars[i] == "<") {
				toggleHTML = true;
			}
			if (toggleHTML) {
				if (arabicString.length>0) {
					mixedArray.push(wrapArabic(arabicString, tag, format));
				}
				arabicString = "";
				htmlString += chars[i];
			} else {
				if (htmlString.length>0) {
					mixedArray.push(htmlString);
				}
				htmlString = "";
				arabicString += getCharState(chars, i);
			}
		}
		if (!toggleHTML) {
			if (arabicString.length>0) {
				mixedArray.push(wrapArabic(arabicString, tag, format));
			}
		} else {
			if (htmlString.length>0) {
				mixedArray.push(htmlString);
			}
		}
		string = mixedArray.join("");
		// returning proper arabic text with the specified font
		return '<p align="right"><font face="'+format.font+'">'+string+'</font></p>';
	}
	/**
	 * @static
	 * helper method handles arabic ligatures and wrapping with bi-directional text but without HTML tags.
	 * 
	 * @param input String value of original arabic string with HTML tags stripped out.
	 * @param tag TextField target html enabled textfield.
	 * @param format TextFormat text format applied to target textfield.
	 * @return String value with correct arabic text.
	 */
	static function wrapArabic(input:String, tag:TextField, format:TextFormat):String {
		var latinChars:String = "abcdefghijklmnopqrstuvwxyz1234567890</>";
		var arabicIntegers:String = String.fromCharCode(0x0660, 0x0661, 0x0662, 0x0663, 0x0664, 0x0665, 0x0666, 0x0667, 0x0668, 0x0669);
		var string:String = "";
		var i:Number = 0;
		var chars:Array = input.split("");
		// reverse all characters for proper right to left order
		if (tag.embedFonts) {
			chars.reverse();
		}
		for (i=0; i<chars.length; i++) {
			// proper arabic ligatures
			string += getCharState(chars, i);
		}
		var words:Array = string.split(" ");
		if (tag.embedFonts) {
			words.reverse();
		}
		var latinWords:Array = [];
		for (i=0; i<words.length; i++) {
			// proper Allah (God) rendering
			if (words[i] == "ﺔﻠﻟﺍ" || words[i] == "ﺔﻠﻟﺃ" || words[i] == "ﻪﻠﻟﺍ" || words[i] == "ﻪﻠﻟﺃ" || words[i] == "ﺍﻟﻠﻪ" || words[i] == "ﺃﻟﻠﻪ" || words[i] == "ﺍﻟﻠﺔ" || words[i] == "ﺃﻟﻠﺔ") {
				if (tag.embedFonts) {
					words.splice(i, 1, String.fromCharCode(0xFDF2, 0xFE8D));
				} else {
					words.splice(i, 1, String.fromCharCode(0xFE8D, 0xFDF2));
				}
			}
			if (words[i] == "ﻪﻠﻟ" || words[i] == "ﺔﻠﻟ" || words[i] == "ﻟﻠﻪ" || words[i] == "ﻟﻠﺔ") {
				words.splice(i, 1, String.fromCharCode(0xFDF2));
			}
			// search for numeric/latin characters
			var hasDigitsOrLatin:Boolean = false;
			var word:Array = words[i].split("");
			var v:Number = 0;
			for (v=0; v<word.length; v++) {
				if (arabicIntegers.indexOf(word[v]) != -1 || latinChars.indexOf(word[v]) != -1) {
					hasDigitsOrLatin = true;
				}
			}
			if (hasDigitsOrLatin) {
				// re-order numeric/latin characters
				word.reverse();
				// check for mixed arabic/latin/numeric characters
				var mixedWord:Array = [];
				var tempLatinChars:Array = [];
				var tempArabicChars:Array = [];
				for (v=0; v<word.length; v++) {
					if (arabicIntegers.indexOf(word[v]) != -1 || latinChars.indexOf(word[v]) != -1) {
						if (tempArabicChars.length>0) {
							tempArabicChars.reverse();
							mixedWord = mixedWord.concat(tempArabicChars);
							tempArabicChars = [];
						}
						tempLatinChars.push(word[v]);
					} else {
						if (tempLatinChars.length>0) {
							mixedWord = mixedWord.concat(tempLatinChars);
							tempLatinChars = [];
						}
						tempArabicChars.push(word[v]);
					}
				}
				if (tempLatinChars.length>0) {
					mixedWord = mixedWord.concat(tempLatinChars);
				}
				word = mixedWord;
				// save numeric/latin words for later reverse
				latinWords.push(word.join(""));
				// replace numeric/latin words with the new reversed ones
				words.splice(i, 1, word.join(""));
			} else {
				// maintain saved words order
				latinWords.push("");
			}
		}
		// reverse non arabic words order
		var toggleLatin:Boolean = false;
		var tempLatin:Array = [];
		for (i=0; i<latinWords.length; i++) {
			if (latinWords[i].length>0) {
				toggleLatin = true;
				// save numeric/latin words temporary
				tempLatin.push({index:i, value:latinWords[i]});
			} else {
				if (toggleLatin) {
					// re-order numeric/latin words
					for (var v:Number = 0; v<tempLatin.length; v++) {
						words.splice(tempLatin[v].index, 1, tempLatin[tempLatin.length-v-1].value);
					}
					tempLatin = [];
				}
				toggleLatin = false;
			}
		}
		string = "";
		// temporarily create an autosized textfield off-stage to use for proper right to left wrapping
		// if using V2 Components somewhere, then use a static depth instead of tag._parent.getNextHighestDepth()
		var temp:TextField = tag._parent.createTextField("temp", tag._parent.getNextHighestDepth(), -1000, -1000, format.size, format.size+2);
		temp.autoSize = true;
		temp.selectable = false;
		var tagFormat:TextFormat = tag.getTextFormat();
		var reserveLine:String = "";
		var lines:Array = [];
		for (i=0; i<words.length; i++) {
			if (reserveLine.length>0) {
				temp.text += " "+words[i];
			} else{
				temp.text = words[i];
			}
			temp.setTextFormat(format);
			if (temp._width<tag._width-tagFormat.leftMargin-tagFormat.rightMargin) {
				if (reserveLine.length>0) {
					reserveLine += " "+words[i];
				} else{
					reserveLine = words[i];
				}
			} else {
				lines.push(reserveLine);
				reserveLine = words[i];
			}
			temp.text = reserveLine;
			temp.setTextFormat(format);
		}
		temp.removeTextField();
		lines.push(reserveLine);
		// re-order words in wrapped lines
		for (i=0; i<lines.length; i++) {
			if (tag.embedFonts) {
				lines.splice(i, 1, lines[i].split(" ").reverse().join(" "));
			} else {
				lines.splice(i, 1, lines[i].split(" ").join(" "));
			}
		}
		// re-joining lines
		string = lines.join("\n");
		return string;
	}
	/**
	 * @static
	 * uses unicode character codes to get proper arabic characters with various cases.
	 * 
	 * @param chars Array containing splitted characters of original text.
	 * @param i Number index of target character.
	 * @return String value with correct arabic character.
	 */
	static function getCharState(chars:Array, i:Number):String {
		var string:String;
		switch (chars[i]) {
			case "ا":
			string = setChar(chars, i, String.fromCharCode(0xFE8D), String.fromCharCode(0xFE8D), String.fromCharCode(0xFE8E), String.fromCharCode(0xFE8E));
			break;
			case "أ":
			string = setChar(chars, i, String.fromCharCode(0xFE83), String.fromCharCode(0xFE83), String.fromCharCode(0xFE84), String.fromCharCode(0xFE84));
			break;
			case "إ":
			string = setChar(chars, i, String.fromCharCode(0xFE87), String.fromCharCode(0xFE87), String.fromCharCode(0xFE88), String.fromCharCode(0xFE88));
			break;
			case "آ":
			string = setChar(chars, i, String.fromCharCode(0xFE81), String.fromCharCode(0xFE81), String.fromCharCode(0xFE82), String.fromCharCode(0xFE82));
			break;
			case "ب":
			string = setChar(chars, i, String.fromCharCode(0xFE8F), String.fromCharCode(0xFE91), String.fromCharCode(0xFE92), String.fromCharCode(0xFE90));
			break;
				case "پ":
			string = setChar(chars, i, String.fromCharCode(0xFB56), String.fromCharCode(0xFB58), String.fromCharCode(0xFB59), String.fromCharCode(0xFB57));
			break;
			case "ت":
			string = setChar(chars, i, String.fromCharCode(0xFE95), String.fromCharCode(0xFE97), String.fromCharCode(0xFE98), String.fromCharCode(0xFE96));
			break;
		
			case "ث":
			string = setChar(chars, i, String.fromCharCode(0xFE99), String.fromCharCode(0xFE9B), String.fromCharCode(0xFE9C), String.fromCharCode(0xFE9A));
			break;
			case "ج":
			string = setChar(chars, i, String.fromCharCode(0xFE9D), String.fromCharCode(0xFE9F), String.fromCharCode(0xFEA0), String.fromCharCode(0xFE9E));
			break;
			case "چ":
			string = setChar(chars, i, String.fromCharCode(0xFB7A), String.fromCharCode(0xFB7C), String.fromCharCode(0xFB7D), String.fromCharCode(0xFB7B));
			break;
			case "ح":
			string = setChar(chars, i, String.fromCharCode(0xFEA1), String.fromCharCode(0xFEA3), String.fromCharCode(0xFEA4), String.fromCharCode(0xFEA2));
			break;
			case "خ":
			string = setChar(chars, i, String.fromCharCode(0xFEA5), String.fromCharCode(0xFEA7), String.fromCharCode(0xFEA8), String.fromCharCode(0xFEA6));
			break;
			case "د":
			string = setChar(chars, i, String.fromCharCode(0xFEA9), String.fromCharCode(0xFEA9), String.fromCharCode(0xFEAA), String.fromCharCode(0xFEAA));
			break;
			case "ذ":
			string = setChar(chars, i, String.fromCharCode(0xFEAB), String.fromCharCode(0xFEAB), String.fromCharCode(0xFEAC), String.fromCharCode(0xFEAC));
			break;
			case "ر":
			string = setChar(chars, i, String.fromCharCode(0xFEAD), String.fromCharCode(0xFEAD), String.fromCharCode(0xFEAE), String.fromCharCode(0xFEAE));
			break;
			case "ز":
			string = setChar(chars, i, String.fromCharCode(0xFEAF), String.fromCharCode(0xFEAF), String.fromCharCode(0xFEB0), String.fromCharCode(0xFEB0));
			break;
			
			case "ژ":
			string = setChar(chars, i, String.fromCharCode(0xFB8A), String.fromCharCode(0xFB8A), String.fromCharCode(0xFB8B), String.fromCharCode(0xFB8B));
			break;
			case "س":
			string = setChar(chars, i, String.fromCharCode(0xFEB1), String.fromCharCode(0xFEB3), String.fromCharCode(0xFEB4), String.fromCharCode(0xFEB2));
			break;
			case "ش":
			string = setChar(chars, i, String.fromCharCode(0xFEB5), String.fromCharCode(0xFEB7), String.fromCharCode(0xFEB8), String.fromCharCode(0xFEB6));
			break;
			case "ص":
			string = setChar(chars, i, String.fromCharCode(0xFEB9), String.fromCharCode(0xFEBB), String.fromCharCode(0xFEBC), String.fromCharCode(0xFEBA));
			break;
			case "ض":
			string = setChar(chars, i, String.fromCharCode(0xFEBD), String.fromCharCode(0xFEBF), String.fromCharCode(0xFEC0), String.fromCharCode(0xFEBE));
			break;
			case "ط":
			string = setChar(chars, i, String.fromCharCode(0xFEC1), String.fromCharCode(0xFEC3), String.fromCharCode(0xFEC4), String.fromCharCode(0xFEC2));
			break;
			case "ظ":
			string = setChar(chars, i, String.fromCharCode(0xFEC5), String.fromCharCode(0xFEC7), String.fromCharCode(0xFEC8), String.fromCharCode(0xFEC6));
			break;
			case "ع":
			string = setChar(chars, i, String.fromCharCode(0xFEC9), String.fromCharCode(0xFECB), String.fromCharCode(0xFECC), String.fromCharCode(0xFECA));
			break;
			case "غ":
			string = setChar(chars, i, String.fromCharCode(0xFECD), String.fromCharCode(0xFECF), String.fromCharCode(0xFED0), String.fromCharCode(0xFECE));
			break;
			case "ف":
			string = setChar(chars, i, String.fromCharCode(0xFED1), String.fromCharCode(0xFED3), String.fromCharCode(0xFED4), String.fromCharCode(0xFED2));
			break;
			case "ق":
			string = setChar(chars, i, String.fromCharCode(0xFED5), String.fromCharCode(0xFED7), String.fromCharCode(0xFED8), String.fromCharCode(0xFED6));
			break;
			case "ك":
			string = setChar(chars, i, String.fromCharCode(0xFED9), String.fromCharCode(0xFEDB), String.fromCharCode(0xFEDC), String.fromCharCode(0xFEDA));
			break;
			case "ک":
			string = setChar(chars, i, String.fromCharCode(0xFB8E), String.fromCharCode(0xFB90), String.fromCharCode(0xFB91), String.fromCharCode(0xFB8F));
			break;
case "گ":
			string = setChar(chars, i, String.fromCharCode(0xFB92), String.fromCharCode(0xFB94), String.fromCharCode(0xFB95), String.fromCharCode(0xFB93));
			break;
			case "ل":
			string = setChar(chars, i, String.fromCharCode(0xFEDD), String.fromCharCode(0xFEDF), String.fromCharCode(0xFEE0), String.fromCharCode(0xFEDE));
			break;
			case "م":
			string = setChar(chars, i, String.fromCharCode(0xFEE1), String.fromCharCode(0xFEE3), String.fromCharCode(0xFEE4), String.fromCharCode(0xFEE2));
			break;
			case "ن":
			string = setChar(chars, i, String.fromCharCode(0xFEE5), String.fromCharCode(0xFEE7), String.fromCharCode(0xFEE8), String.fromCharCode(0xFEE6));
			break;
			case "ه":
			string = setChar(chars, i, String.fromCharCode(0xFEE9), String.fromCharCode(0xFEEB), String.fromCharCode(0xFEEC), String.fromCharCode(0xFEEA));
			break;
			case "ة":
			string = setChar(chars, i, String.fromCharCode(0xFE93), "", "", String.fromCharCode(0xFE94));
			break;
			
			case "و":
			string = String.fromCharCode(0x0648);
			break;
			case "و":
			string = setChar(chars, i, String.fromCharCode(0xFEED), String.fromCharCode(0xFEED), String.fromCharCode(0xFEEE), String.fromCharCode(0xFEEE));
			break;
			case "ؤ":
			string = setChar(chars, i, String.fromCharCode(0xFE85), String.fromCharCode(0xFE85), String.fromCharCode(0xFE86), String.fromCharCode(0xFE86));
			break;
			case "ی":
			string = setChar(chars, i, String.fromCharCode(0xFEEF), String.fromCharCode(0xFBFE), String.fromCharCode(0xFBFF), String.fromCharCode(0xFBFD));
			break;
			case "ى":
			string = setChar(chars, i, String.fromCharCode(0xFEEF), String.fromCharCode(0xFEEF), String.fromCharCode(0xFEF0), String.fromCharCode(0xFEF0));
			break;
			case "ي":
			string = setChar(chars, i, String.fromCharCode(0xFEF1), String.fromCharCode(0xFEF3), String.fromCharCode(0xFEF4), String.fromCharCode(0xFEF2));
			break;
			case "ئ":
			string = setChar(chars, i, String.fromCharCode(0xFE89), String.fromCharCode(0xFE8B), String.fromCharCode(0xFE8C), String.fromCharCode(0xFE8A));
			break;
			case "ء":
			string = String.fromCharCode(0xFE80);
			break;
			case "0":
			string = String.fromCharCode(0x0660);
			break;
			case "1":
			string = String.fromCharCode(0x0661);
			break;
			case "2":
			string = String.fromCharCode(0x0662);
			break;
			case "3":
			string = String.fromCharCode(0x0663);
			break;
			case "4":
			string = String.fromCharCode(0x0664);
			break;
			case "5":
			string = String.fromCharCode(0x0665);
			break;
			case "6":
			string = String.fromCharCode(0x0666);
			break;
			case "7":
			string = String.fromCharCode(0x0667);
			break;
			case "8":
			string = String.fromCharCode(0x0668);
			break;
			case "9":
			string = String.fromCharCode(0x0669);
			break;
			case "?":
			string = String.fromCharCode(0x061F);
			break;
			case ",":
			string = String.fromCharCode(0x060C);
			break;
			case ";":
			string = String.fromCharCode(0x061B);
			break;
			case "%":
			string = String.fromCharCode(0x066A);
			break;
			default:
			string = chars[i];
			break;
		}
		return string;
	}
	/**
	 * @static
	 * detects special cases for arabic ligatures.
	 * 
	 * @param chars Array containing splitted characters of original text.
	 * @param i Number index of target character.
	 * @param solo String arabic character without joints.
	 * @param begin String arabic character with a trailing joint.
	 * @param middle String arabic character with both joint.
	 * @param end String arabic character with an initial joint.
	 * @return String value with correct arabic character.
	 */
	static function setChar(chars:Array, i:Number, solo:String, begin:String, middle:String, end:String):String {
		var string:String = "";
		var arabicChars:String = "ءئيىؤوةیهنملكچقفغعژطظضصشسزرذدخحجثتپبآإکﻭگاأـ";
		var specialChars:String = "اأإآدذرزوؤءژ";
		// detect lam-alef (ﻻ) cases
		if (chars[i] == "ل" && chars[i+1] == "ا") {
			if (arabicChars.indexOf(chars[i-1]) != -1 && specialChars.indexOf(chars[i-1]) == -1) {
				string = String.fromCharCode(0xFEFC);
			} else {
				string = String.fromCharCode(0xFEFB);
			}
			chars.splice(i+1, 1, "");
		} else if (chars[i] == "ل" && chars[i+1] == "أ") {
			if (arabicChars.indexOf(chars[i-1]) != -1 && specialChars.indexOf(chars[i-1]) == -1) {
				string = String.fromCharCode(0xFEF8);
			} else {
				string = String.fromCharCode(0xFEF7);
			}
			chars.splice(i+1, 1, "");
		} else if (chars[i] == "ل" && chars[i+1] == "إ") {
			if (arabicChars.indexOf(chars[i-1]) != -1 && specialChars.indexOf(chars[i-1]) == -1) {
				string = String.fromCharCode(0xFEFA);
			} else {
				string = String.fromCharCode(0xFEF9);
			}
			chars.splice(i+1, 1, "");
		} else if (chars[i] == "ل" && chars[i+1] == "آ") {
			if (arabicChars.indexOf(chars[i-1]) != -1 && specialChars.indexOf(chars[i-1]) == -1) {
				string = String.fromCharCode(0xFEF6);
			} else {
				string = String.fromCharCode(0xFEF5);
			}
			chars.splice(i+1, 1, "");
		} else {
			var msddd=0;
		
			// check for arabic character position in word (solo, begin, middle, end)
			if (arabicChars.indexOf(chars[i-1]) != -1 && arabicChars.indexOf(chars[i+1]) != -1) {
				if (specialChars.indexOf(chars[i-1]) != -1) {
					if (specialChars.indexOf(chars[i]) != -1 ) {
						string = solo;
					} else {
						string = begin;
					}
				} else {
					if (specialChars.indexOf(chars[i]) != -1 || chars[i+1] == "ء") {
						string = end;
					} else {
						string = middle;
					}
				}
			} else {
				if (arabicChars.indexOf(chars[i-1]) != -1 && arabicChars.indexOf(chars[i+1]) == -1) {
					if (specialChars.indexOf(chars[i-1]) != -1) {
						string = solo;
					} else {
						string = end;
					}
				} else if (arabicChars.indexOf(chars[i-1]) == -1 && arabicChars.indexOf(chars[i+1]) != -1) {
					if (specialChars.indexOf(chars[i]) != -1) {
						string = solo;
					} else {
						string = begin;
					}
				}
			}
		}
		return string;
	}
	/**
	 * @static
	 * reverse brackets
	 *
	 * @param input String value of original string
	 * @return String value of new string with all brackets reversed
	 */
	static function reverseBrackets(input:String):String {
		input = input.split("(").join("$LEFT$");
		input = input.split(")").join("$RIGHT$");
		input = input.split("$LEFT$").join(")");
		input = input.split("$RIGHT$").join("(");
		input = input.split("[").join("$LEFT$");
		input = input.split("]").join("$RIGHT$");
		input = input.split("$LEFT$").join("]");
		input = input.split("$RIGHT$").join("[");
		input = input.split("{").join("$LEFT$");
		input = input.split("}").join("$RIGHT$");
		input = input.split("$LEFT$").join("}");
		input = input.split("$RIGHT$").join("{");
		return input;
	}
	/**
	 * @static
	 * remove diacritics from arabic characters.
	 *
	 * @param chars Array containing splitted characters with diacritics.
	 * @return Array containing splitted characters without diacritics.
	 */
	 static function stripDiacritics(chars:Array):Array {
		var array:Array = [];
		for (var i:Number  = 0; i<chars.length; i++) {
			// arabic diacritics range from charcode 1611 and 1618
			if (chars[i].charCodeAt(0)<1611 || chars[i].charCodeAt(0)>1618) {
				array.push(chars[i]);
			}
		}
		return array;
	 }
}