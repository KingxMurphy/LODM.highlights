using System;
using LODM.highlights.Services.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LODM.highlights.TagHelpers
{
    public class MemberListTagHelper : TagHelper
    {
        private readonly IMember _memberService;

        public MemberListTagHelper(IMember memberService)
        {
            _memberService = memberService;
        }

        public string SelectedMember { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li"; //replaces the member-list with the <li> tag
            output.Content.Clear();

            //Removing the person we clicked on from the list. 
            foreach (var member in _memberService.GetAll())
            {
                if (!SelectedMember.Equals(member.GamerTag, StringComparison.CurrentCultureIgnoreCase))
                {
                    //This could be expanded so that the drop down has another property to determine WHERE we are going to go.
                    //Granted a dynamic list could be less than desirable... for more than one programmer to deal with.

                    var href = $"/PlayerBio/PlayerBio?selectedPlayerGamerTag={member.GamerTag}";

                    var listItem = $"<a href=\"{href}\">{member.GamerTag}</a>";
                    output.Content.AppendHtml(listItem);
                }
            }
        }
    }
}
