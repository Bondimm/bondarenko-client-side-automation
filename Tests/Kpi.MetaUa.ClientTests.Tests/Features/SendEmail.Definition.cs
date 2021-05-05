﻿using FluentAssertions;
using Kpi.MetaUa.ClientTests.Model.Domain.Login;
using Kpi.MetaUa.ClientTests.Model.Domain.SendEmail;
using Kpi.MetaUa.ClientTests.TestsData.Storage;
using TechTalk.SpecFlow;

namespace Kpi.MetaUa.ClientTests.Tests.Features
{
    [Binding, Scope(Feature = "Send Email")]
    public class SendEmailDefinition
    {
        private readonly ISendEmailContext _sendEmailContext;
        private UserInformation _userInformation;
        private EmailInformation _emailInformation;

        public SendEmailDefinition(
            ISendEmailContext sendEmailContext)
        {
            _sendEmailContext = sendEmailContext;
        }

        [Given(@"I have '(.*)' as a sender")]
        public void GivenIHaveAsASender(string entityName)
        {
            _userInformation = UsersStorage.Users[entityName];
            _sendEmailContext.OpenAndLogin(_userInformation);
        }

        [Given(@"I have '(.*)' email that will be sent")]
        public void GivenIHaveEmailThatWillBeSent(string entityName)
        {
            _emailInformation = EmailStorage.Emails[entityName];
        }

        [When(@"I open email form")]
        public void WhenIOpenEmailForm()
        {
            _sendEmailContext.OpenEmailForm();
        }

        [When(@"I send email to myself")]
        public void WhenISendEmailToMyself()
        {
            _sendEmailContext.SendEmail(_userInformation.Login, _emailInformation);
        }

        [Then(@"I see received email in my inbox")]
        public void ThenISeeReceivedEmailInMyInbox()
        {
            _sendEmailContext.GetLastEmailTitle().Should().BeEquivalentTo(_emailInformation.Subject);
        }

        [When(@"I send mail with (.*) invalid email address")]
        public void WhenISendMailWithInvalidUserAddressInvalidEmailAddress(string invalidAddress)
        {
            _sendEmailContext.SendEmail(invalidAddress, _emailInformation);
        }

        [Then(@"I see (.*) response")]
        public void ThenISeeResponse(string response)
        {
            _sendEmailContext.GetErrorMessage().Should().BeEquivalentTo(
                response);
        }
    }
}