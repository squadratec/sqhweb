using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SQH.Shared.Extensions
{
    public static class HtmlDocumentExtensions
    {
        public static List<SelectListItem> ToListItem(this HtmlDocument doc, string filtro)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var documentoFiltrado = doc.DocumentNode.SelectNodes(filtro);

            if (documentoFiltrado != null)
            {
                foreach (HtmlNode elemento in documentoFiltrado)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = elemento.Attributes["value"].Value.Trim();
                    item.Text = elemento.InnerText.Trim(); //System.Web.HttpUtility.UrlDecode(nome, System.Text.Encoding.GetEncoding("iso-8859-1"));

                    if (!String.IsNullOrEmpty(item.Text.Trim()))
                        list.Add(item);
                }

                if (list.Count > 0 && !list.Any(x => x.Text.ToLower().Contains("selecione") || x.Value.ToLower().Contains("selecione")))
                    list.Insert(0, new SelectListItem()
                    {
                        Text = "Selecione",
                        Value = "0"
                    });
            }
            return list;
        }

        public static List<SelectBase> ToDropdownList(this HtmlDocument doc, string filtro)
        {
            List<SelectBase> list = new List<SelectBase>();

            var documentoFiltrado = doc.DocumentNode.SelectNodes(filtro);

            if (documentoFiltrado != null)
            {
                foreach (HtmlNode elemento in documentoFiltrado)
                {
                    SelectBase item = new SelectBase();

                    item.value = elemento.Attributes["value"].Value.Trim();
                    item.text = elemento.InnerText.Trim();

                    if (!String.IsNullOrEmpty(item.text.Trim()))
                        list.Add(item);
                }
            }
            return list;
        }
    }
}
