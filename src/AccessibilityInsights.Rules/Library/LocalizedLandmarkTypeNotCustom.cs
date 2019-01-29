// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using AccessibilityInsights.Core.Bases;
using AccessibilityInsights.Core.Enums;
using AccessibilityInsights.Rules.PropertyConditions;
using AccessibilityInsights.Rules.Resources;
using static AccessibilityInsights.Rules.PropertyConditions.StringProperties;

namespace AccessibilityInsights.Rules.Library
{
    [RuleInfo(ID = RuleId.LocalizedLandmarkTypeNotCustom)]
    class LocalizedLandmarkTypeNotCustom : Rule
    {
        public LocalizedLandmarkTypeNotCustom()
        {
            this.Info.Description = Descriptions.LocalizedLandmarkTypeNotCustom;
            this.Info.HowToFix = HowToFix.LocalizedLandmarkTypeNotCustom;
            this.Info.Standard = A11yCriteriaId.InfoAndRelationships;
        }

        public override EvaluationCode Evaluate(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return LocalizedLandmarkType.IsNoCase("custom").Matches(e) ? EvaluationCode.Error : EvaluationCode.Pass;
        }

        protected override Condition CreateCondition()
        {
            return Landmarks.Custom & LocalizedLandmarkType.NotNullOrEmpty;
        }
    } // class
} // namespace
