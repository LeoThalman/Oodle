﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Test.AcceptanceTests.KollsTests.KollTestsSprint6
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("ID470 -- No unused pages exist", Description=@"    As a user on the Oodle site
    I would like to be able to be able to only go to pages that have a purpose and not have to worry about accidentally being shown a page without working functionality
    So that I dont have to worry about being on a page I shouldn't be on or at a page that is created but not functional ", SourceFile="AcceptanceTests\\KollsTests\\KollTestsSprint6\\ID470.feature", SourceLine=9)]
    public partial class ID470_NoUnusedPagesExistFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ID470.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ID470 -- No unused pages exist", @"    As a user on the Oodle site
    I would like to be able to be able to only go to pages that have a purpose and not have to worry about accidentally being shown a page without working functionality
    So that I dont have to worry about being on a page I shouldn't be on or at a page that is created but not functional ", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("As a user if I try and navigate to any of the pages like \"confirmEmail.cshtml\" or" +
            " \"verifyCode.cshtml\" I won\'t be able to because they have been removed by Oodle " +
            "devs", SourceLine=14)]
        public virtual void AsAUserIfITryAndNavigateToAnyOfThePagesLikeConfirmEmail_CshtmlOrVerifyCode_CshtmlIWontBeAbleToBecauseTheyHaveBeenRemovedByOodleDevs()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("As a user if I try and navigate to any of the pages like \"confirmEmail.cshtml\" or" +
                    " \"verifyCode.cshtml\" I won\'t be able to because they have been removed by Oodle " +
                    "devs", ((string[])(null)));
#line 15
this.ScenarioSetup(scenarioInfo);
#line 16
    testRunner.Given("I am visiting a page like Oodle/confirmEmail", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 17
    testRunner.When("I try and visit the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
    testRunner.Then("I wont be shown that page but rather a templated error page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore
#endregion