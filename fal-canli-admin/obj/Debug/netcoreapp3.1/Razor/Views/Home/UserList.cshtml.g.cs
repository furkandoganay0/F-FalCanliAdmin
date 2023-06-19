#pragma checksum "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b111b91b3d04e74dd586afdb58be41325c6a0f3a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UserList), @"mvc.1.0.view", @"/Views/Home/UserList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/_ViewImports.cshtml"
using fal_canli_admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/_ViewImports.cshtml"
using fal_canli_admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b111b91b3d04e74dd586afdb58be41325c6a0f3a", @"/Views/Home/UserList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db30989141a419fa579ae61f9ae209bfdede367a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UserList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<UserModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
  
    ViewData["Title"] = "Kullanıcı Listesi";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<div class=""container"" id=""kt_content_container"">
    <div class=""card"">
        <div class=""card-body fs-6 p-10 p-lg-15"">
            <div class=""py-10"">
                <h1 class=""anchor fw-bolder mb-5"" id=""zero-configuration"">
                    <a href=""#zero-configuration""></a>Kullanıcılar
                </h1>
                <a href=""/Home/UserAddOrUpdate"" class=""btn btn-primary"">Yeni Ekle</a>
                <div class=""py-5"">
                    <div id=""kt_datatable_example_1_wrapper"" class=""dataTables_wrapper dt-bootstrap4"">
                        <div class=""table-responsive"">
                            <table id=""kt_datatable_example_1"" class=""table table-row-bordered gy-5"">
                                <thead>
                                    <tr class=""fw-bold fs-6 text-muted"">
                                        <th>Ad Soyad</th>
                                        <th>E-mail</th>
                                        <th>Toplam Alınan/Kullanılan Kredi(Bugün)</th>
       ");
            WriteLiteral(@"                                 <th>Toplam Alınan/Kullanılan Kredi(Bu Ay)</th>
                                        <th>Toplam Alınan/Kullanılan Kredi(Bu Yıl)</th>
                                        <th>İşlemler</th>
                                    </tr>
                                </thead>
                                <tbody>
");
#nullable restore
#line 31 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
                                      
                                        foreach (var item in Model)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr>\n                                                <td>");
#nullable restore
#line 35 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
                                               Write(item.name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 35 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
                                                          Write(item.lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                                <td>");
#nullable restore
#line 36 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
                                               Write(item.email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                                <td>10/4</td>\n                                                <td>50/17</td>\n                                                <td>150/140</td>\n                                                <td><a");
            BeginWriteAttribute("href", " href=\"", 2040, "\"", 2081, 2);
            WriteAttributeValue("", 2047, "/Home/UserAddOrUpdate?id=", 2047, 25, true);
#nullable restore
#line 40 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
WriteAttributeValue("", 2072, item._id, 2072, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Güncelle</a></td>\n                                            </tr>\n");
#nullable restore
#line 42 "/Users/furkandoganay/Desktop/fal-canli-admin/fal-canli-admin/Views/Home/UserList.cshtml"
                                        }
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>Ad Soyad</th>
                                        <th>E-mail</th>
                                        <th>Toplam Alınan/Kullanılan Kredi(Bugün)</th>
                                        <th>Toplam Alınan/Kullanılan Kredi(Bu Ay)</th>
                                        <th>Toplam Alınan/Kullanılan Kredi(Bu Yıl)</th>
                                        <th>İşlemler</th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    <script>$(\"#kt_datatable_example_1\").DataTable();</script>\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<UserModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
