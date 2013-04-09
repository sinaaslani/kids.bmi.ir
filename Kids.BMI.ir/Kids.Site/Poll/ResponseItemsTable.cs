using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kids.Common;
using Kids.EntitiesModel;

namespace Site.Kids.bmi.ir.Poll
{
    /// <summary>
    /// Summary description for ResponseItemsTable.
    /// </summary>
    public class ResponseItemsTable
    {
        public static Control ShowAdminResponseItem(QuestionnaireForm_Questions q)
        {
            List<QuestionnaireForms_ResponseItems> riList = q.QuestionnaireForms_ResponseItems.ToList();
            ResponseItemsType resType = (ResponseItemsType)q.ItemsType;

            Table tbl = new Table();

            for (int i = 0; i < riList.Count; i++)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell { VerticalAlign = VerticalAlign.Top };
                QuestionnaireForms_ResponseItems ri = (QuestionnaireForms_ResponseItems)riList[i];
                switch (resType)
                {
                    case ResponseItemsType.CheckBox:
                        CheckBox chkbox = new CheckBox
                            {
                                ID = ri.ItemId.ToString(),
                                Text = " " + ri.ItemText,
                                Checked = false,
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(chkbox);
                        if (ri.HasInputText)
                        {
                            TextBox inputBox = new TextBox
                                {
                                    TextMode = TextBoxMode.MultiLine,
                                    Rows = 2,
                                    Width = Unit.Pixel(330),
                                    ID = "txt" + ri.ItemId
                                };
                            Label br = new Label();
                            br.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            td.Controls.Add(br);
                            td.Controls.Add(inputBox);
                        }
                        break;

                    case ResponseItemsType.InputText:
                        TextBox txtBox = new TextBox
                            {
                                TextMode = TextBoxMode.MultiLine,
                                Rows = 2,
                                Width = Unit.Pixel(330),
                                ID = ri.ItemId.ToString()
                            };
                        Label l = new Label
                            {
                                Text = " " + ri.ItemText + " ", 
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(l);
                        Label blankF = new Label {Text = ""};
                        if (l.Text.Trim().Length > 0)
                            blankF.Text = "<br>";
                        blankF.Text += "&nbsp;&nbsp;&nbsp;";
                        td.Controls.Add(blankF);
                        td.Controls.Add(txtBox);
                        break;

                    case ResponseItemsType.RadioButton:
                        RadioButton radioBtn = new RadioButton
                            {
                                ID = ri.ItemId.ToString(),
                                Text = " " + ri.ItemText,
                                GroupName = ri.QuestionsId.ToString(),
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(radioBtn);
                        if (ri.HasInputText)
                        {
                            TextBox inputBox = new TextBox
                                {
                                    TextMode = TextBoxMode.MultiLine,
                                    Rows = 2,
                                    Width = Unit.Pixel(330),
                                    ID = "txt" + ri.ItemId
                                };
                            Label br = new Label {Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"};
                            td.Controls.Add(br);
                            td.Controls.Add(inputBox);
                        }
                        break;
                }


                // set admin links
                HyperLink editItem = new HyperLink();
                HyperLink delitem = new HyperLink();
                editItem.Text = "ÊÌ—«Ì‘";
                delitem.Text = "Õ–›";
                //editItem.NavigateUrl = SystemConfigs.UrlAdminEditResponseItem + ri.ItemId;
                //delitem.NavigateUrl = SystemConfigs.UrlAdminDeleteResponseItem + ri.ItemId;
                editItem.CssClass = "LinkCadetBlue";
                delitem.CssClass = "LinkCadetBlue";
                Label blankLblPre = new Label();
                Label blankLblPost = new Label();
                blankLblPre.Text = "&nbsp;&nbsp;";
                blankLblPost.Text = "&nbsp;&nbsp;";
                td.Controls.Add(blankLblPre);
                td.Controls.Add(editItem);
                td.Controls.Add(blankLblPost);
                td.Controls.Add(delitem);


                tr.Cells.Add(td);
                tbl.Rows.Add(tr);
            }
            return tbl;
        }


        public static Control ShowUserResponseItem(QuestionnaireForm_Questions q)
        {
            List<QuestionnaireForms_ResponseItems> riList = q.QuestionnaireForms_ResponseItems.ToList();
            ResponseItemsType resType = (ResponseItemsType)q.ItemsType;

            Table tbl = new Table();

            for (int i = 0; i < riList.Count; i++)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell {VerticalAlign = VerticalAlign.Top};
                QuestionnaireForms_ResponseItems ri = riList[i];
                switch (resType)
                {
                    case ResponseItemsType.CheckBox:
                        CheckBox chkbox = new CheckBox
                            {
                                ID = ri.ItemId.ToString(),
                                Text = " " + ri.ItemText,
                                Checked = false,
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(chkbox);

                        if (ri.HasInputText)
                        {
                            TextBox inputBox = new TextBox
                                {
                                    TextMode = TextBoxMode.MultiLine,
                                    Rows = 2,
                                    Width = Unit.Pixel(330),
                                    ID = "txt" + ri.ItemId
                                };
                            Label br = new Label {Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"};
                            td.Controls.Add(br);
                            td.Controls.Add(inputBox);
                        }
                        break;

                    case ResponseItemsType.InputText:
                        TextBox txtBox = new TextBox
                            {
                                TextMode = TextBoxMode.MultiLine,
                                Rows = 2,
                                Width = Unit.Pixel(330),
                                ID = ri.ItemId.ToString()
                            };
                        Label l = new Label {Text = " " + ri.ItemText + " ", CssClass = "normaltextSmall"};
                        td.Controls.Add(l);
                        Label blankF = new Label {Text = ""};
                        if (l.Text.Trim().Length > 0)
                            blankF.Text = "<br>";
                        blankF.Text += "&nbsp;&nbsp;&nbsp;";
                        td.Controls.Add(blankF);
                        td.Controls.Add(txtBox);
                        break;

                    case ResponseItemsType.RadioButton:
                        RadioButton radioBtn = new RadioButton
                            {
                                ID = ri.ItemId.ToString(),
                                Text = " " + ri.ItemText,
                                GroupName = "qroup" + ri.QuestionsId,
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(radioBtn);
                        if (ri.HasInputText)
                        {
                            TextBox inputBox = new TextBox
                                {
                                    TextMode = TextBoxMode.MultiLine,
                                    Rows = 2,
                                    Width = Unit.Pixel(330),
                                    ID = "txt" + ri.ItemId
                                };
                            Label br = new Label {Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"};
                            td.Controls.Add(br);
                            td.Controls.Add(inputBox);
                        }
                        break;
                }

                tr.Cells.Add(td);
                tbl.Rows.Add(tr);
            }
            return tbl;
        }

        public static Control ShowResponseItem(QuestionnaireForm_Questions q, string userMode)
        {
            List<QuestionnaireForms_ResponseItems> riList = q.QuestionnaireForms_ResponseItems.ToList();
            ResponseItemsType resType = (ResponseItemsType)q.ItemsType;

            Table tbl = new Table();

            for (int i = 0; i < riList.Count; i++)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.VerticalAlign = VerticalAlign.Top;
                QuestionnaireForms_ResponseItems ri = riList[i];
                switch (resType)
                {
                    case ResponseItemsType.CheckBox:
                        CheckBox chkbox = new CheckBox
                            {
                                ID = ri.ItemId.ToString(),
                                Text = " " + ri.ItemText,
                                Checked = false,
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(chkbox);
                        if (ri.HasInputText)
                        {
                            TextBox inputBox = new TextBox
                                {
                                    TextMode = TextBoxMode.MultiLine,
                                    Rows = 2,
                                    Width = Unit.Pixel(330),
                                    ID = "txt" + ri.ItemId
                                };
                            Label br = new Label();
                            br.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            td.Controls.Add(br);
                            td.Controls.Add(inputBox);
                        }
                        break;

                    case ResponseItemsType.InputText:
                        TextBox txtBox = new TextBox { ID = ri.ItemId.ToString() };
                        Label l = new Label();
                        l.Text = " " + ri.ItemText + " ";
                        l.CssClass = "normaltextSmall";
                        td.Controls.Add(l);
                        td.Controls.Add(txtBox);
                        break;

                    case ResponseItemsType.RadioButton:
                        RadioButton radioBtn = new RadioButton
                            {
                                ID = ri.ItemId.ToString(),
                                Text = " " + ri.ItemText,
                                GroupName = ri.QuestionsId.ToString(),
                                CssClass = "normaltextSmall"
                            };
                        td.Controls.Add(radioBtn);
                        if (ri.HasInputText)
                        {
                            TextBox inputBox = new TextBox();
                            inputBox.TextMode = TextBoxMode.MultiLine;
                            inputBox.Rows = 2;
                            inputBox.Width = Unit.Pixel(330);
                            inputBox.ID = "txt" + ri.ItemId;
                            Label br = new Label {Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"};
                            td.Controls.Add(br);
                            td.Controls.Add(inputBox);
                        }
                        break;
                }
                if (userMode.ToLower() == "admin")
                {
                    // set admin links
                    HyperLink editItem = new HyperLink();
                    HyperLink delitem = new HyperLink();
                    editItem.Text = "ÊÌ—«Ì‘";
                    delitem.Text = "Õ–›";
                    //editItem.NavigateUrl = SystemConfigs.UrlAdminEditResponseItem + ri.ItemId;
                    //delitem.NavigateUrl = SystemConfigs.UrlAdminDeleteResponseItem + ri.ItemId;
                    editItem.CssClass = "LinkCadetBlue";
                    delitem.CssClass = "LinkCadetBlue";
                    Label blankLblPre = new Label();
                    Label blankLblPost = new Label();
                    blankLblPre.Text = "&nbsp;&nbsp;";
                    blankLblPost.Text = "&nbsp;&nbsp;";
                    td.Controls.Add(blankLblPre);
                    td.Controls.Add(editItem);
                    td.Controls.Add(blankLblPost);
                    td.Controls.Add(delitem);
                }

                tr.Cells.Add(td);
                tbl.Rows.Add(tr);
            }
            return tbl;
        }
    }
}