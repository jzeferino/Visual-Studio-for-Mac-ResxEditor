using Gtk;
using NUnit.Framework;
using ResxEditor.Core.Controllers;
using ResxEditor.Core.Enums;
using ResxEditor.Core.Models;
using ResxEditor.Core.Interfaces;

namespace ResxEditor.Tests
{
    [TestFixture]
    public class ResourceListTests
    {
        ResourceController ResourceController { get; set; }
        IResourceListStore StoreController { get; set; }
        ListStore Store { get; set; }
        TreeModelFilter Filter { get; set; }

        void AssertValidResource(TreeIter iter, IResourceModel resource)
        {
            Assert.AreEqual(resource.Name, Store.GetValue(iter, (int)ResourceColumns.Name));
            Assert.AreEqual(resource.Value, Store.GetValue(iter, (int)ResourceColumns.Value));
            Assert.AreEqual(resource.Comment, Store.GetValue(iter, (int)ResourceColumns.Comment));
        }

        [SetUp]
        public void Setup()
        {
            ResourceController = new ResourceController();
            Assert.IsNotNull(ResourceController?.StoreController);
            StoreController = ResourceController.StoreController;
            Filter = ResourceController.StoreController.Model as TreeModelFilter;
            Store = Filter.Model as ListStore ?? ResourceController.StoreController.Model as ListStore;
        }

        [TestCase("test", null, null)]
        [TestCase("test", "test", null)]
        [TestCase("test", "test", "test")]
        [TestCase(null, null, null)]
        public void SimpleAdditionFromEmpty(string name, string value, string comment)
        {
            var testData = new ResourceModel(name, value, comment);
            Assert.AreEqual(0, Store.IterNChildren());
            StoreController.AppendValues(testData);
            Assert.AreEqual(1, Store.IterNChildren());
            TreeIter iter;
            Assert.IsTrue(Store.GetIterFirst(out iter));
            AssertValidResource(iter, testData);
        }

        [Test]
        public void RemoveResourceTest()
        {
            var testData1 = new ResourceModel("a", null);
            var testData2 = new ResourceModel("b", null);

            Assert.AreEqual(0, Store.IterNChildren());
            StoreController.AppendValues(testData1);
            StoreController.AppendValues(testData2);
            Assert.AreEqual(2, Store.IterNChildren());

            TreeIter iter;
            Assert.IsTrue(Store.GetIterFirst(out iter));
            AssertValidResource(iter, testData1);
            var path = Store.GetPath(iter);

            StoreController.Remove(path);

            Assert.IsTrue(Store.GetIterFirst(out iter));
            AssertValidResource(iter, testData2);
        }


        [Test]
        public void FilterTest()
        {
            var testData1 = new ResourceModel("a", null);
            var testData2 = new ResourceModel("b", null);

            Assert.AreEqual(0, Store.IterNChildren());
            StoreController.AppendValues(testData1);
            StoreController.AppendValues(testData2);
            Assert.AreEqual(2, Store.IterNChildren());

            ResourceController.ResourceEditorView.ResourceControlBar.FilterEntry.Text = "b";
            Store = Filter.Model as ListStore;
            StoreController.Refilter();
            Assert.AreEqual(1, Filter.IterNChildren());

            TreeIter iter;
            Assert.IsTrue(Filter.GetIterFirst(out iter));
            var path = Filter.GetPath(iter);
            Assert.AreEqual(testData2.Name, StoreController.GetName(path));
            ResourceController.ResourceEditorView.ResourceControlBar.FilterEntry.Text = "a";
            StoreController.Refilter();
            Assert.IsTrue(Filter.GetIterFirst(out iter));
            path = Filter.GetPath(iter);
            Assert.AreEqual(testData1.Name, StoreController.GetName(path));
        }
    }
}

