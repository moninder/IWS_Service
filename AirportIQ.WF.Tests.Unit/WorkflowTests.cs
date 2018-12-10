using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirportIQ.WF;
using Moq;
using AirportIQ.WF.Steps;
using AirportIQ.WF.Config;

namespace AirportIQ.Tests.WF
{
    [TestClass]
    public class WorkflowTests
    {
        private static WorkflowService _workflowService;

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            _workflowService = new WorkflowService()
                                .WithSMTPInfo(new SMTPInfo
                                {
                                    Host = "some server",
                                    Port = "8080",
                                    Username = "username",
                                    Password = "password"
                                })
                                .WithDatabaseInfo(new TestDatabaseInfo())
                                .Start();
        }



        [TestMethod, ExpectedException(typeof(Exception), "Cannot add a pre-existing workflow to the workflow service.")]
        public void The_same_workflow_cannot_be_added_twice()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" };
            workflow.AddStep(step1);

            // Act
            _workflowService.AddWorkflow(workflow);
            _workflowService.AddWorkflow(workflow);
        }

        [TestMethod]
        public void Can_add_new_workflows()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" };
            workflow.AddStep(step1);

            int worflowCountBeforeAdd = _workflowService.Workflows.Count;

            // Act
            _workflowService.AddWorkflow(workflow);

            // Assert
            Assert.AreEqual(worflowCountBeforeAdd + 1, _workflowService.Workflows.Count);
        }

        [TestMethod]
        public void Can_add_workflow_steps()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" };
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" };
            TestWorkflowStep step3 = new TestWorkflowStep { Title = "step3", AssignedTo = "Someone3" };

            // Act
            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);

            // Assert
            Assert.AreEqual(3, workflow.Steps.Count);
        }

        [TestMethod, ExpectedException(typeof(Exception), "Cannot add a pre-existing workflow step to a workflow.")]
        public void The_same_step_cannot_be_added_twice()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;

            // Act
            workflow.AddStep(step);
            workflow.AddStep(step);
        }

        [TestMethod]
        public void Can_cancel_workflow_step_approval()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" }; ;
            workflow.AddStep(step1);
            workflow.AddStep(step2);

            // Act
            step1.OnBeforeWorkflowStepApproved += (s, e) =>
                {
                    e.Cancel = true;
                };

            workflow.CurrentStep.Approve(); // should cancel out;
            workflow.CurrentStep.Approve(); // should cancel out;

            // Assert
            Assert.AreEqual<WorkflowStep>(step1, workflow.CurrentStep);
        }

        [TestMethod]
        public void Can_reject_a_workflow_step()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" }; ;
            workflow.AddStep(step1);
            workflow.AddStep(step2);

            // Act
            workflow.CurrentStep.Approve();
            workflow.CurrentStep.Reject();

            // Assert
            Assert.AreEqual<WorkflowStep>(step1, workflow.CurrentStep);
        }

        [TestMethod, ExpectedException(typeof(Exception), "Cannot move to a previous workflow step.")]
        public void Rejecting_the_first_step_throws_an_exception()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            workflow.AddStep(step1);

            // Act
            workflow.CurrentStep.Reject();  // should throw here because of only 1 step
        }

        [TestMethod]
        public void CurrentStep_calculates_correctly()
        {
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" }; ;
            TestWorkflowStep step3 = new TestWorkflowStep { Title = "step3", AssignedTo = "Someone3" }; ;
            TestWorkflowStep step4 = new TestWorkflowStep { Title = "step4", AssignedTo = "Someone4" }; ;
            TestWorkflowStep step5 = new TestWorkflowStep { Title = "step5", AssignedTo = "Someone5" }; ;

            workflow.AddStep(step1);

            Assert.AreEqual<WorkflowStep>(step1, workflow.CurrentStep);

            workflow = new TestWorkflow();
            step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" }; ;
            step3 = new TestWorkflowStep { Title = "step3", AssignedTo = "Someone3" }; ;
            step4 = new TestWorkflowStep { Title = "step4", AssignedTo = "Someone4" }; ;
            step5 = new TestWorkflowStep { Title = "step5", AssignedTo = "Someone5" }; ;
            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);
            workflow.AddStep(step4);
            workflow.AddStep(step5);
            workflow.CurrentStep.Approve();
            workflow.CurrentStep.Approve();

            Assert.AreEqual<WorkflowStep>(step3, workflow.CurrentStep);

            workflow = new TestWorkflow();
            step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" }; ;
            step3 = new TestWorkflowStep { Title = "step3", AssignedTo = "Someone3" }; ;
            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);

            workflow.CurrentStep.Approve();
            workflow.CurrentStep.Approve();
            workflow.CurrentStep.Reject();

            Assert.AreEqual<WorkflowStep>(step2, workflow.CurrentStep);
        }

        [TestMethod]
        public void Approving_all_steps_completes_the_workflow_and_removes_it_from_the_service()
        {
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" };
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" };
            TestWorkflowStep step3 = new TestWorkflowStep { Title = "step3", AssignedTo = "Someone3" };

            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);

            _workflowService.AddWorkflow(workflow);

            workflow.CurrentStep.Approve();
            workflow.CurrentStep.Approve();
            workflow.CurrentStep.Approve();

            Assert.IsNull(workflow.CurrentStep);
            Assert.IsFalse(_workflowService.Workflows.ContainsKey(workflow.Id));
        }

        [TestMethod]
        public void Can_add_notes()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" }; ;

            // Act
            Note note = new Note { CreatedBy = "ldunnehoo", Text = "blah blah blah" };
            step1.AddNote(note);

            note = new Note { CreatedBy = "burt reynolds", Text = "fedfdsfdf" };
            step2.AddNote(note);

            note = new Note { CreatedBy = "bob dole", Text = "upoo98i 9o698" };
            step2.AddNote(note);

            workflow.AddStep(step1);
            workflow.AddStep(step2);

            _workflowService.AddWorkflow(workflow);

            // Assert
            Assert.AreEqual(1, step1.Notes.Count);
            Assert.AreEqual(2, step2.Notes.Count);
            Assert.AreEqual(3, workflow.Notes.Count);
        }

        [TestMethod]
        public void Can_add_attachments()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" };
            TestWorkflowStep step2 = new TestWorkflowStep { Title = "step2", AssignedTo = "Someone2" };

            // Act
            Attachment attachment = new Attachment { CreatedBy = "ldunnehoo", Filename = "blah.txt" };
            step1.AddAttachment(attachment);

            attachment = new Attachment { CreatedBy = "burt reynolds", Filename = "yup.txt" };
            step2.AddAttachment(attachment);

            attachment = new Attachment { CreatedBy = "bob dole", Filename = "dsgsdgsg.zip" };
            step2.AddAttachment(attachment);

            workflow.AddStep(step1);
            workflow.AddStep(step2);

            _workflowService.AddWorkflow(workflow);

            // Assert
            Assert.AreEqual(1, step1.Attachments.Count);
            Assert.AreEqual(2, step2.Attachments.Count);
            Assert.AreEqual(3, workflow.Attachments.Count);
        }

        [TestMethod]
        public void Can_retrieve_a_specific_users_workflows()
        {
            // Arrange
            TestWorkflow workflow1 = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "ldunnehoo" };
            workflow1.AddStep(step1);
            _workflowService.AddWorkflow(workflow1);

            workflow1 = new TestWorkflow();
            step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "breynolds" };
            workflow1.AddStep(step1);
            _workflowService.AddWorkflow(workflow1);

            workflow1 = new TestWorkflow();
            step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "bdole" };
            workflow1.AddStep(step1);
            _workflowService.AddWorkflow(workflow1);

            workflow1 = new TestWorkflow();
            step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "ldunnehoo" };
            workflow1.AddStep(step1);
            _workflowService.AddWorkflow(workflow1);

            // Act
            var workflows = _workflowService.GetWorkflowsByUser("ldunnehoo");

            // Assert
            Assert.AreEqual(2, workflows.Count);
        }

        [TestMethod, ExpectedException(typeof(Exception), "Cannot save a workflow that isn't attached to a workflow service.")]
        public void Can_not_save_a_workflow_that_is_not_attached_to_a_service()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1" }; ;
            workflow.AddStep(step1);

            // Act
            workflow.Save();
        }

        [TestMethod, ExpectedException(typeof(Exception), "Cannot save a workflow that doesn't contain any workflow steps.")]
        public void Can_not_save_a_workflow_that_doesnt_contain_steps()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();

            // Act
            workflow.Save();
        }

        [TestMethod]
        public void Can_expire_a_workflow()
        {
            // Arrange
            TestWorkflow workflow = new TestWorkflow();
            TestWorkflowStep step1 = new TestWorkflowStep { Title = "step1", AssignedTo = "Someone1", Duration = new TimeSpan(-1, 0, 0) };

            // Act
            workflow.AddStep(step1);

            // Assert
            Assert.IsTrue(workflow.IsExpired);
        }
    }
}
