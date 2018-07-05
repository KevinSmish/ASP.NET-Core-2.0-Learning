using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_13_11_HtmlTargetElement.TagHelpers
{
    /*
    Атрибут HtmlTargetElement указывает, что класс будет применяться к элементам, у которых установлен 
    атрибут header. Важно, что название хелпера опять же соответствует целевому объекту - атрибуту header.

    <div header>Первый параграф</div>
    <div header>Второй параграф</div>

    В самом классе происходит замена существующего элемента на элемент <h2> и удаление атрибута header. 
    Все внутреннее содержание, текст, который определен в блоках div, при этом сохраняется. 
    В итоге вместо двух блоков div будут созданы следующие заголовки:
    
    <h2>Первый параграф</h2>
    <h2>Второй параграф</h2>

    Мы также можем определить набор атрибутов, которым должен соответствовать tag-хелпер:
        [HtmlTargetElement(Attributes = "header, divtitle")]
        public class HeaderTagHelper : TagHelper
        {
            //.......................
        }

    В этом случае элемент должен иметь сразу два атрибута: header и divtitle.
        <div header divtitle>Первый параграф</div>
        <div header divtitle>Второй параграф</div>

    Переопределение имени элемента
    Мы можем переопределить имя элемента, передав в атрибут HtmlTargetElement 
    другое название элемента, которое отличается от имени tag-хелпера. Например:
	
    [HtmlTargetElement("article-header")]
    public class HeaderTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
            output.Attributes.RemoveAll("article-header");
        }
    }

    Данный tag-хелпер будет применяться к элементу "article-header":
        <article-header>Заголовок!</article-header>

    Установка родительского тега
    Через свойство ParentTag можно установить элемент, в котором должен использоваться наш tag-хелпер:
    [HtmlTargetElement(ParentTag ="form")]
    public class HeaderTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
        }
    }

    В этом случае tag-хелпер будет применяться только к тем элементам header, который расположены 
    внутри элемента form:
    <header>Заголовок 1</header>
    <form>
        <header>Заголовок 2</header>
    </form>

    В этом примере только второй элемент header будет обрабатываться tag-хелпером.

    Сочетание нескольких условий
    Мы можем определить сразу несколько параметров, чтобы конкретизировать диапазон действия 
    tag-хелпера:

        using Microsoft.AspNetCore.Razor.TagHelpers;
 
        namespace TagHelpersApp.TagHelpers
        {
            [HtmlTargetElement("form-header", ParentTag ="form", Attributes ="form-title")]
            public class HeaderTagHelper : TagHelper
            {
                public override void Process(TagHelperContext context, TagHelperOutput output)
                {
                    output.TagName = "h2";
                    output.Attributes.RemoveAll("form-title");
                }
            }
        }

    В данном случае класс HeaderTagHelper будет применяться к элементу "form-header", 
    который обязательно должен иметь атрибут "form-title" и который обязательно должен 
    находиться внутри элемента "form". То есть в следующем случае будет обработан только 
    третий элемент "form-header":
        <form-header>Заголовок1</form-header>
        <form-header form-title>Заголовок2</form-header>
        <form>
            <form-header form-title>Заголовок3</form-header>
        </form>
    */

    [HtmlTargetElement(Attributes = "header")]
    public class HeaderTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
            output.Attributes.RemoveAll("header");
        }
    }
}
