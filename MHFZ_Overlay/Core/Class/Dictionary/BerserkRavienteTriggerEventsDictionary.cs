﻿// © 2023 The mhfz-overlay Authors.
// Use of this source code is governed by a MIT license that can be
// found in the LICENSE file.
using System.Collections.Generic;

namespace MHFZ_Overlay.Core.Class.Dictionary;

/// <summary>
/// The Berserk Raviente Practice Trigger Events list
/// </summary>
public static class BerserkRavientePracticeTriggerEvents
{
    public static IReadOnlyDictionary<int, string> BerserkRavientePracticeTriggerEventIDs { get; } = new Dictionary<int, string>
    {
        {0, "Slay 1"},
        {1, "Sedation 1"},
        {2, "Support and Combat 1"},
        {3, "Destruction 2"},
        {4, "Destruction 3"},
        {5, "Destruction 4"},
        {6, "Support 2"},
        {7, "Sedation 4"},
        {8, "Sedation 5"},
        {9, "Sedation 6"},
    };
};

/// <summary>
/// The Berserk Raviente Trigger Events list
/// </summary>
public static class BerserkRavienteTriggerEvents
{
    public static IReadOnlyDictionary<int, string> BerserkRavienteTriggerEventIDs { get; } = new Dictionary<int, string>
    {
        {0, "Slay 1"},
        {1, "Sedation 1"},
        {2, "Sedation 2"},
        {3, "Destruction 2"},
        {4, "Support 1 Done"},
        {5, "Support 2"},
        {6, "Destruction 3"},
        {7, "Part Break"},
        {8, "Destruction 4"},
        {9, "Destruction 5"},
        {10, "Support 2 Done"},
        {11, "Destruction 6"},
        {12, "Destruction 7"},
        {13, "Slay Done"}
    };
};