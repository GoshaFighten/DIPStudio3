using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;
using System.Collections;

namespace SelectionObjects {
    public class SelectionObjsSettings : ValidatedPluginSettings {
        public SelectionObjsSettings(IPlugin plugin)
            : base(plugin) {
        }

        protected override bool IsResultSeries {
            get { return false; }
        }

        private bool fFollowName;
        private bool fPrognosis;
        private bool fIntermediate;
        private ValueObject fThreshold = new ValueObject();
        private ValueObject fStrob = new ValueObject();
        private IList<Criteria> fCriterias = new BindingList<Criteria>();
        private string fInputTable = string.Empty;

        [DisplayName("�������� ������")]
        public string InputTable {
            get { return fInputTable; }
            set { fInputTable = value; }
        }

        [DisplayName("��������")]
        public IList<Criteria> Criterias {
            get { return fCriterias; }
            set { fCriterias = value; }
        }

        [DisplayName("�����")]
        public ValueObject Strob {
            get { return fStrob; }
            set { fStrob = value; }
        }

        [DisplayName("�����")]
        public ValueObject Threshold {
            get { return fThreshold; }
            set { fThreshold = value; }
        }

        [DisplayName("������� ������������� ������")]
        public bool Intermediate {
            get { return fIntermediate; }
            set { fIntermediate = value; }
        }

        [DisplayName("�������")]
        public bool Prognosis {
            get { return fPrognosis; }
            set { fPrognosis = value; }
        }

        [DisplayName("����� �� �����")]
        public bool FollowName {
            get { return fFollowName; }
            set { fFollowName = value; }
        }
    }
}
