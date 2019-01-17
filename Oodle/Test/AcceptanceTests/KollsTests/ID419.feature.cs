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
namespace Test.AcceptanceTests.KollsTests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("ID419 -- Teacher Index Page", Description=@"    As a teacher on the Oodle site
    I would like my teacher landing home page to have features then what is currently in place (class name and description)
    So that I can view some quick information about my students, assignments, and other upcoming information or
    So that I can have a better understanding of how to be most productive as a teacher and use the teacher features I have provided to me", SourceFile="AcceptanceTests\\KollsTests\\ID419.feature", SourceLine=5)]
    public partial class ID419_TeacherIndexPageFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ID419.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ID419 -- Teacher Index Page", @"    As a teacher on the Oodle site
    I would like my teacher landing home page to have features then what is currently in place (class name and description)
    So that I can view some quick information about my students, assignments, and other upcoming information or
    So that I can have a better understanding of how to be most productive as a teacher and use the teacher features I have provided to me", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [TechTalk.SpecRun.ScenarioAttribute("Index page should be available for a teacher", SourceLine=11)]
        public virtual void IndexPageShouldBeAvailableForATeacher()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Index page should be available for a teacher", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
    testRunner.Given("I am visiting a class that I, the teacher, created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
    testRunner.When("I visit the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
    testRunner.Then("the information should be about that specific classs", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Information at a glance section holds information", SourceLine=16)]
        public virtual void InformationAtAGlanceSectionHoldsInformation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Information at a glance section holds information", ((string[])(null)));
#line 17
this.ScenarioSetup(scenarioInfo);
#line 18
    testRunner.Given("I am visiting the teacher index page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
    testRunner.When("I click a link on the information at a glance section", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
    testRunner.Then("I am shown upcoming tasks if any exist or", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 21
    testRunner.Then("data shows class naviation tips or", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 22
    testRunner.Then("I am shown upcoming assignments if any exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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