// © 2023 The mhfz-overlay developers.
// Use of this source code is governed by a MIT license that can be
// found in the LICENSE file.

namespace MHFZ_Overlay.ViewModels.Windows;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using EZlion.Mapper;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Memory;
using MHFZ_Overlay;
using MHFZ_Overlay.Models;
using MHFZ_Overlay.Models.Collections;
using MHFZ_Overlay.Models.Constant;
using MHFZ_Overlay.Services;
using RESTCountries.NET.Models;
using RESTCountries.NET.Services;
using SkiaSharp;
using Application = System.Windows.Application;

/// <summary>
/// Abstract class that defines the base properties and methods that are shared between AddressModelNotHGE and AddressModelHGE classes. It has a Mem object, which is used to read and write data from the game's memory. It also has properties that represent the selected monster, the hit count value, and the monster's health points.
/// </summary>
/// <seealso cref="INotifyPropertyChanged" />
public abstract class AddressModel : INotifyPropertyChanged
{
    private static readonly NLog.Logger LoggerInstance = NLog.LogManager.GetCurrentClassLogger();

    private static readonly DatabaseService DatabaseManagerInstance = DatabaseService.GetInstance();

    public readonly Mem M;

    private int savedMonster1MaxHP;

    private int savedMonster2MaxHP;

    private int savedMonster3MaxHP;

    private int savedMonster4MaxHP;

    private int savedMonster1ID;

    private int savedMonster2ID;

    public AddressModel(Mem m)
    {
        LoggerInstance.Info(CultureInfo.InvariantCulture, $"AddressModel initialized");
        LoggerInstance.Trace(new StackTrace().ToString());
        M = m;
    }

    public int SelectedMonster { get; set; }

    public bool ShowMonsterAtkMult { get; set; }

    public bool ShowMonsterDefrate { get; set; }

    public bool ShowMonsterSize { get; set; }

    public bool ShowMonsterPoison { get; set; }

    public bool ShowMonsterSleep { get; set; }

    public bool ShowMonsterPara { get; set; }

    public bool ShowMonsterBlast { get; set; }

    public bool ShowMonsterStun { get; set; }

    public bool ShowPersonalBestInfo { get; set; }

    public bool ShowTimerInfo { get; set; }

    public bool ShowHitCountInfo { get; set; }

    public bool ShowPlayerAtkInfo { get; set; }

    public bool ShowPlayerHitsTakenBlockedInfo { get; set; }

    public bool ShowQuestID { get; set; }

    public bool ShowQuestAttemptsInfo { get; set; }

    public bool ShowPersonalBestTimePercentInfo { get; set; }

    public bool ShowPersonalBestAttemptsInfo { get; set; }

    public bool ShowMonster1HPBar { get; set; }

    public bool ShowMonster2HPBar { get; set; }

    public bool ShowMonster3HPBar { get; set; }

    public bool ShowMonster4HPBar { get; set; }

    public bool ShowSharpness { get; set; }

    public bool ShowSessionTimeInfo { get; set; }

    public bool ShowMonsterPartHP { get; set; }

    public bool ShowKBMLayout { get; set; }

    public bool ShowAPM { get; set; }

    public bool ShowGamepadLayout { get; set; }

    public bool ShowMonster1Icon { get; set; }

    public bool ShowFrameCounter { get; set; }

    public bool ShowMap { get; set; }

    public bool ShowDamagePerSecond { get; set; }

    public bool ShowOverlayModeWatermark { get; set; }

    public bool ShowSaveIcon { get; set; } = true;

    public bool ShowLocationTextInfo { get; set; } = true;

    public bool ShowQuestNameInfo { get; set; } = true;

    public bool ShowPlayerAttackGraph { get; set; } = true;

    public bool ShowPlayerDPSGraph { get; set; } = true;

    public bool ShowPlayerAPMGraph { get; set; } = true;

    public bool ShowPlayerHitsPerSecondGraph { get; set; } = true;

    public abstract bool IsNotRoad();

    public abstract int HitCountInt();

    public abstract int DamageDealt();

    //New addresses
    public abstract int AreaID();

    public abstract int GRankNumber();

    public abstract int GSR();

    public abstract int RoadFloor();

    public abstract int WeaponStyle();

    public abstract int QuestID();

    public abstract int UrukiPachinkoFish();

    public abstract int UrukiPachinkoMushroom();

    public abstract int UrukiPachinkoSeed();

    public abstract int UrukiPachinkoMeat();

    public abstract int UrukiPachinkoChain();

    public abstract int UrukiPachinkoScore();

    public abstract int UrukiPachinkoBonusScore();

    public abstract int NyanrendoScore();

    public abstract int DokkanBattleCatsScore();

    public abstract int DokkanBattleCatsScale();

    public abstract int DokkanBattleCatsShell();

    public abstract int DokkanBattleCatsCamp();

    public abstract int GuukuScoopSmall();

    public abstract int GuukuScoopMedium();

    public abstract int GuukuScoopLarge();

    public abstract int GuukuScoopGolden();

    public abstract int GuukuScoopScore();

    public abstract int PanicHoneyScore();

    public abstract int Sharpness();

    public abstract int CaravanPoints();

    public abstract int MezeportaFestivalPoints();

    public abstract int DivaBond();

    public abstract int DivaItemsGiven();

    public abstract int GCP();

    public abstract int RoadPoints();

    public abstract int ArmorColor();

    public abstract int RaviGg();

    public abstract int Ravig();

    public abstract int GZenny();

    public abstract int GuildFoodSkill();

    public abstract int GalleryEvaluationScore();

    public abstract int PoogiePoints();

    public abstract int PoogieItemUseID();

    public abstract int PoogieCostume();

    //zero-indexed
    public abstract int CaravenGemLevel();

    public abstract int RoadMaxStagesMultiplayer();

    public abstract int RoadTotalStagesMultiplayer();

    public abstract int RoadTotalStagesSolo();

    public abstract int RoadMaxStagesSolo();

    public abstract int RoadFatalisSlain();

    public abstract int RoadFatalisEncounters();

    public abstract int FirstDistrictDuremudiraEncounters();

    public abstract int FirstDistrictDuremudiraSlays();

    public abstract int SecondDistrictDuremudiraEncounters();

    public abstract int SecondDistrictDuremudiraSlays();

    public abstract int DeliveryQuestPoints(); //doesn't seem to work

    //red is 0
    public abstract int SharpnessLevel();

    public abstract int PartnerLevel();

    public abstract int ObjectiveType();

    public abstract int DivaSkillUsesLeft();

    public abstract int HalkFullness();

    public abstract int RankBand();

    public abstract int PartnyaRankPoints();

    //parts
    public abstract int Monster1Part1();

    public abstract int Monster1Part2();

    public abstract int Monster1Part3();

    public abstract int Monster1Part4();

    public abstract int Monster1Part5();

    public abstract int Monster1Part6();

    public abstract int Monster1Part7();

    public abstract int Monster1Part8();

    public abstract int Monster1Part9();

    public abstract int Monster1Part10();

    public abstract int Monster2Part1();

    public abstract int Monster2Part2();

    public abstract int Monster2Part3();

    public abstract int Monster2Part4();

    public abstract int Monster2Part5();

    public abstract int Monster2Part6();

    public abstract int Monster2Part7();

    public abstract int Monster2Part8();

    public abstract int Monster2Part9();

    public abstract int Monster2Part10();

    public abstract int TimeInt();

    public abstract int TimeDefInt();

    public abstract int WeaponRaw();

    public abstract int WeaponType();

    public abstract int LargeMonster1ID();

    public abstract int LargeMonster2ID();

    public abstract int LargeMonster3ID();

    public abstract int LargeMonster4ID();

    public abstract int Monster1HPInt();

    public abstract int Monster2HPInt();

    public abstract int Monster3HPInt();

    public abstract int Monster4HPInt();

    public abstract string Monster1AtkMult();

    public abstract string Monster2AtkMult();

    public abstract decimal Monster1DefMult();

    public abstract decimal Monster2DefMult();

    public abstract int Monster1Poison();

    public abstract int Monster1PoisonNeed();

    public abstract int Monster1Sleep();

    public abstract int Monster1SleepNeed();

    public abstract int Monster1Para();

    public abstract int Monster1ParaNeed();

    public abstract int Monster1Blast();

    public abstract int Monster1BlastNeed();

    public abstract int Monster1Stun();

    public abstract int Monster1StunNeed();

    public abstract string Monster1Size();

    public abstract int Monster2Poison();

    public abstract int Monster2PoisonNeed();

    public abstract int Monster2Sleep();

    public abstract int Monster2SleepNeed();

    public abstract int Monster2Para();

    public abstract int Monster2ParaNeed();

    public abstract int Monster2Blast();

    public abstract int Monster2BlastNeed();

    public abstract int Monster2Stun();

    public abstract int Monster2StunNeed();

    public abstract string Monster2Size();

    public abstract int Objective1ID();

    public abstract int Objective1Quantity();

    public abstract int Objective1CurrentQuantityMonster();

    public abstract int Objective1CurrentQuantityItem();

    //ravi
    public abstract int RavienteTriggeredEvent();

    public abstract int GreatSlayingPoints();

    public abstract int GreatSlayingPointsSaved();

    //normal and violent. berserk support
    public abstract int RavienteAreaID();

    public abstract int RoadSelectedMonster();

    // TODO Yamas and Beru
    public abstract int AlternativeMonster1HPInt();

    public abstract int AlternativeMonster1AtkMult();

    public abstract int AlternativeMonster1DefMult();

    public abstract int AlternativeMonster1Size();

    public abstract int AlternativeMonster1Poison();

    public abstract int AlternativeMonster1PoisonNeed();

    public abstract int AlternativeMonster1Sleep();

    public abstract int AlternativeMonster1SleepNeed();

    public abstract int AlternativeMonster1Para();

    public abstract int AlternativeMonster1ParaNeed();

    public abstract int AlternativeMonster1Blast();

    public abstract int AlternativeMonster1BlastNeed();

    public abstract int AlternativeMonster1Stun();

    public abstract int AlternativeMonster1StunNeed();

    public abstract int AlternativeMonster1Part1();

    public abstract int AlternativeMonster1Part2();

    public abstract int AlternativeMonster1Part3();

    public abstract int AlternativeMonster1Part4();

    public abstract int AlternativeMonster1Part5();

    public abstract int AlternativeMonster1Part6();

    public abstract int AlternativeMonster1Part7();

    public abstract int AlternativeMonster1Part8();

    public abstract int AlternativeMonster1Part9();

    public abstract int AlternativeMonster1Part10();

    public abstract int DivaSkill();

    public abstract int StarGrades();

    public abstract int CaravanSkill1();

    public abstract int CaravanSkill2();

    public abstract int CaravanSkill3();

    public abstract int CurrentFaints();

    //road and normal
    public abstract int MaxFaints();

    //shitens, conquests, pioneer, daily, caravan, interception
    public abstract int AlternativeMaxFaints();

    public abstract int CaravanScore();

    public abstract int CaravanMonster1ID();

    //unsure
    public abstract int CaravanMonster2ID();

    public abstract int BlademasterWeaponID();

    //same as melee afaik
    public abstract int GunnerWeaponID();

    public abstract int WeaponDeco1ID();

    public abstract int WeaponDeco2ID();

    public abstract int WeaponDeco3ID();

    public abstract int ArmorHeadID();

    public abstract int ArmorHeadDeco1ID();

    public abstract int ArmorHeadDeco2ID();

    public abstract int ArmorHeadDeco3ID();

    public abstract int ArmorChestID();

    public abstract int ArmorChestDeco1ID();

    public abstract int ArmorChestDeco2ID();

    public abstract int ArmorChestDeco3ID();

    public abstract int ArmorArmsID();

    public abstract int ArmorArmsDeco1ID();

    public abstract int ArmorArmsDeco2ID();

    public abstract int ArmorArmsDeco3ID();

    public abstract int ArmorWaistID();

    public abstract int ArmorWaistDeco1ID();

    public abstract int ArmorWaistDeco2ID();

    public abstract int ArmorWaistDeco3ID();

    public abstract int ArmorLegsID();

    public abstract int ArmorLegsDeco1ID();

    public abstract int ArmorLegsDeco2ID();

    public abstract int ArmorLegsDeco3ID();

    public abstract int Cuff1ID();

    public abstract int Cuff2ID();

    public abstract int TotalDefense();

    public abstract int PouchItem1ID();

    public abstract int PouchItem1Qty();

    public abstract int PouchItem2ID();

    public abstract int PouchItem2Qty();

    public abstract int PouchItem3ID();

    public abstract int PouchItem3Qty();

    public abstract int PouchItem4ID();

    public abstract int PouchItem4Qty();

    public abstract int PouchItem5ID();

    public abstract int PouchItem5Qty();

    public abstract int PouchItem6ID();

    public abstract int PouchItem6Qty();

    public abstract int PouchItem7ID();

    public abstract int PouchItem7Qty();

    public abstract int PouchItem8ID();

    public abstract int PouchItem8Qty();

    public abstract int PouchItem9ID();

    public abstract int PouchItem9Qty();

    public abstract int PouchItem10ID();

    public abstract int PouchItem10Qty();

    public abstract int PouchItem11ID();

    public abstract int PouchItem11Qty();

    public abstract int PouchItem12ID();

    public abstract int PouchItem12Qty();

    public abstract int PouchItem13ID();

    public abstract int PouchItem13Qty();

    public abstract int PouchItem14ID();

    public abstract int PouchItem14Qty();

    public abstract int PouchItem15ID();

    public abstract int PouchItem15Qty();

    public abstract int PouchItem16ID();

    public abstract int PouchItem16Qty();

    public abstract int PouchItem17ID();

    public abstract int PouchItem17Qty();

    public abstract int PouchItem18ID();

    public abstract int PouchItem18Qty();

    public abstract int PouchItem19ID();

    public abstract int PouchItem19Qty();

    public abstract int PouchItem20ID();

    public abstract int PouchItem20Qty();

    public abstract int AmmoPouchItem1ID();

    public abstract int AmmoPouchItem1Qty();

    public abstract int AmmoPouchItem2ID();

    public abstract int AmmoPouchItem2Qty();

    public abstract int AmmoPouchItem3ID();

    public abstract int AmmoPouchItem3Qty();

    public abstract int AmmoPouchItem4ID();

    public abstract int AmmoPouchItem4Qty();

    public abstract int AmmoPouchItem5ID();

    public abstract int AmmoPouchItem5Qty();

    public abstract int AmmoPouchItem6ID();

    public abstract int AmmoPouchItem6Qty();

    public abstract int AmmoPouchItem7ID();

    public abstract int AmmoPouchItem7Qty();

    public abstract int AmmoPouchItem8ID();

    public abstract int AmmoPouchItem8Qty();

    public abstract int AmmoPouchItem9ID();

    public abstract int AmmoPouchItem9Qty();

    public abstract int AmmoPouchItem10ID();

    public abstract int AmmoPouchItem10Qty();

    public abstract int ArmorSkill1();

    public abstract int ArmorSkill2();

    public abstract int ArmorSkill3();

    public abstract int ArmorSkill4();

    public abstract int ArmorSkill5();

    public abstract int ArmorSkill6();

    public abstract int ArmorSkill7();

    public abstract int ArmorSkill8();

    public abstract int ArmorSkill9();

    public abstract int ArmorSkill10();

    public abstract int ArmorSkill11();

    public abstract int ArmorSkill12();

    public abstract int ArmorSkill13();

    public abstract int ArmorSkill14();

    public abstract int ArmorSkill15();

    public abstract int ArmorSkill16();

    public abstract int ArmorSkill17();

    public abstract int ArmorSkill18();

    public abstract int ArmorSkill19();

    public abstract int BloatedWeaponAttack();

    public abstract int ZenithSkill1();

    public abstract int ZenithSkill2();

    public abstract int ZenithSkill3();

    public abstract int ZenithSkill4();

    public abstract int ZenithSkill5();

    public abstract int ZenithSkill6();

    public abstract int ZenithSkill7();

    public abstract int AutomaticSkillWeapon();

    public abstract int AutomaticSkillHead();

    public abstract int AutomaticSkillChest();

    public abstract int AutomaticSkillArms();

    public abstract int AutomaticSkillWaist();

    public abstract int AutomaticSkillLegs();

    public abstract int StyleRank1();

    public abstract int StyleRank2();

    public abstract int GRWeaponLv();

    public abstract int GRWeaponLvBowguns();

    public abstract int Sigil1Name1();

    public abstract int Sigil1Value1();

    public abstract int Sigil1Name2();

    public abstract int Sigil1Value2();

    public abstract int Sigil1Name3();

    public abstract int Sigil1Value3();

    public abstract int Sigil2Name1();

    public abstract int Sigil2Value1();

    public abstract int Sigil2Name2();

    public abstract int Sigil2Value2();

    public abstract int Sigil2Name3();

    public abstract int Sigil2Value3();

    public abstract int Sigil3Name1();

    public abstract int Sigil3Value1();

    public abstract int Sigil3Name2();

    public abstract int Sigil3Value2();

    public abstract int Sigil3Name3();

    public abstract int Sigil3Value3();

    public abstract int FelyneHunted();

    public abstract int MelynxHunted();

    public abstract int ShakalakaHunted();

    public abstract int VespoidHunted();

    public abstract int HornetaurHunted();

    public abstract int GreatThunderbugHunted();

    public abstract int KelbiHunted();

    public abstract int MosswineHunted();

    public abstract int AntekaHunted();

    public abstract int PopoHunted();

    public abstract int AptonothHunted();

    public abstract int ApcerosHunted();

    public abstract int BurukkuHunted();

    public abstract int ErupeHunted();

    public abstract int VelocipreyHunted();

    public abstract int VelocidromeHunted();

    public abstract int GenpreyHunted();

    public abstract int GendromeHunted();

    public abstract int IopreyHunted();

    public abstract int IodromeHunted();

    public abstract int GiapreyHunted();

    public abstract int YianKutKuHunted();

    public abstract int BlueYianKutKuHunted();

    public abstract int YianGarugaHunted();

    public abstract int GypcerosHunted();

    public abstract int PurpleGypcerosHunted();

    public abstract int HypnocHunted();

    public abstract int BrightHypnocHunted();

    public abstract int SilverHypnocHunted();

    public abstract int FarunokkuHunted();

    public abstract int ForokururuHunted();

    public abstract int ToridclessHunted();

    public abstract int RemobraHunted();

    public abstract int RathianHunted();

    public abstract int PinkRathianHunted();

    public abstract int GoldRathianHunted();

    public abstract int RathalosHunted();

    public abstract int AzureRathalosHunted();

    public abstract int SilverRathalosHunted();

    public abstract int KhezuHunted();

    public abstract int RedKhezuHunted();

    public abstract int BasariosHunted();

    public abstract int GraviosHunted();

    public abstract int BlackGraviosHunted();

    public abstract int MonoblosHunted();

    public abstract int WhiteMonoblosHunted();

    public abstract int DiablosHunted();

    public abstract int BlackDiablosHunted();

    public abstract int TigrexHunted();

    public abstract int EspinasHunted();

    public abstract int OrangeEspinasHunted();

    public abstract int WhiteEspinasHunted();

    public abstract int AkantorHunted();

    public abstract int BerukyurosuHunted();

    public abstract int DoragyurosuHunted();

    public abstract int PariapuriaHunted();

    public abstract int DyuragauaHunted();

    public abstract int GurenzeburuHunted();

    public abstract int OdibatorasuHunted();

    public abstract int HyujikikiHunted();

    public abstract int AnorupatisuHunted();

    public abstract int ZerureusuHunted();

    public abstract int MeraginasuHunted();

    public abstract int DiorexHunted();

    public abstract int PoborubarumuHunted();

    public abstract int VarusaburosuHunted();

    public abstract int GureadomosuHunted();

    public abstract int BariothHunted();

    public abstract int NargacugaHunted();

    public abstract int ZenaserisuHunted();

    public abstract int SeregiosHunted();

    public abstract int BogabadorumuHunted();

    public abstract int CephalosHunted();

    public abstract int CephadromeHunted();

    public abstract int PlesiothHunted();

    public abstract int GreenPlesiothHunted();

    public abstract int VolganosHunted();

    public abstract int RedVolganosHunted();

    public abstract int HermitaurHunted();

    public abstract int DaimyoHermitaurHunted();

    public abstract int CeanataurHunted();

    public abstract int ShogunCeanataurHunted();

    public abstract int ShenGaorenHunted();

    public abstract int AkuraVashimuHunted();

    public abstract int AkuraJebiaHunted();

    public abstract int TaikunZamuzaHunted();

    public abstract int KusubamiHunted();

    public abstract int BullfangoHunted();

    public abstract int BulldromeHunted();

    public abstract int CongaHunted();

    public abstract int CongalalaHunted();

    public abstract int BlangoHunted();

    public abstract int BlangongaHunted();

    public abstract int GogomoaHunted();

    public abstract int RajangHunted();

    public abstract int KamuOrugaronHunted();

    public abstract int NonoOrugaronHunted();

    public abstract int MidogaronHunted();

    public abstract int GougarfHunted();

    public abstract int VoljangHunted();

    public abstract int KirinHunted();

    public abstract int KushalaDaoraHunted();

    public abstract int RustedKushalaDaoraHunted();

    public abstract int ChameleosHunted();

    public abstract int LunastraHunted();

    public abstract int TeostraHunted();

    public abstract int LaoShanLungHunted();

    public abstract int AshenLaoShanLungHunted();

    public abstract int YamaTsukamiHunted();

    public abstract int RukodioraHunted();

    public abstract int RebidioraHunted();

    public abstract int FatalisHunted();

    public abstract int ShantienHunted();

    public abstract int DisufiroaHunted();

    public abstract int GarubaDaoraHunted();

    public abstract int InagamiHunted();

    public abstract int HarudomeruguHunted();

    public abstract int YamaKuraiHunted();

    public abstract int ToaTesukatoraHunted();

    public abstract int GuanzorumuHunted();

    public abstract int KeoaruboruHunted();

    public abstract int ShagaruMagalaHunted();

    public abstract int ElzelionHunted();

    public abstract int AmatsuHunted();

    public abstract int AbioruguHunted();

    public abstract int GiaoruguHunted();

    public abstract int GasurabazuraHunted();

    public abstract int DeviljhoHunted();

    public abstract int BrachydiosHunted();

    public abstract int UragaanHunted();

    public abstract int KuarusepusuHunted();

    public abstract int PokaraHunted();

    public abstract int PokaradonHunted();

    public abstract int BaruragaruHunted();

    public abstract int ZinogreHunted();

    public abstract int StygianZinogreHunted();

    public abstract int GoreMagalaHunted();

    public abstract int BlitzkriegBogabadorumuHunted();

    public abstract int SparklingZerureusuHunted();

    public abstract int StarvingDeviljhoHunted();

    public abstract int CrimsonFatalisHunted();

    public abstract int WhiteFatalisHunted();

    public abstract int CactusHunted();

    public abstract int ArrogantDuremudiraHunted();// untested

    public abstract int MiRuHunted();

    public abstract int UnknownHunted();

    public abstract int GoruganosuHunted();

    public abstract int AruganosuHunted();

    public abstract int PSO2RappyHunted();

    public abstract int RocksHunted();

    public abstract int UrukiHunted();

    public abstract int GorgeObjectsHunted();

    public abstract int BlinkingNargacugaHunted();

    public abstract int KingShakalakaHunted();

    public abstract int QuestState();

    public abstract int RoadDureSkill1Name();

    public abstract int RoadDureSkill1Level();

    public abstract int RoadDureSkill2Name();

    public abstract int RoadDureSkill2Level();

    public abstract int RoadDureSkill3Name();

    public abstract int RoadDureSkill3Level();

    public abstract int RoadDureSkill4Name();

    public abstract int RoadDureSkill4Level();

    public abstract int RoadDureSkill5Name();

    public abstract int RoadDureSkill5Level();

    public abstract int RoadDureSkill6Name();

    public abstract int RoadDureSkill6Level();

    public abstract int RoadDureSkill7Name();

    public abstract int RoadDureSkill7Level();

    public abstract int RoadDureSkill8Name();

    public abstract int RoadDureSkill8Level();

    public abstract int RoadDureSkill9Name();

    public abstract int RoadDureSkill9Level();

    public abstract int RoadDureSkill10Name();

    public abstract int RoadDureSkill10Level();

    public abstract int RoadDureSkill11Name();

    public abstract int RoadDureSkill11Level();

    public abstract int RoadDureSkill12Name();

    public abstract int RoadDureSkill12Level();

    public abstract int RoadDureSkill13Name();

    public abstract int RoadDureSkill13Level();

    public abstract int RoadDureSkill14Name();

    public abstract int RoadDureSkill14Level();

    public abstract int RoadDureSkill15Name();

    public abstract int RoadDureSkill15Level();

    public abstract int RoadDureSkill16Name();

    public abstract int RoadDureSkill16Level();

    public abstract int PartySize();

    public abstract int PartySizeMax();

    public abstract uint GSRP();

    public abstract uint GRP();

    public abstract int HunterHP();

    public abstract int HunterStamina();

    public abstract int QuestItemsUsed();

    public abstract int AreaHitsTakenBlocked();

    public abstract int PartnyaBagItem1ID();

    public abstract int PartnyaBagItem1Qty();

    public abstract int PartnyaBagItem2ID();

    public abstract int PartnyaBagItem2Qty();

    public abstract int PartnyaBagItem3ID();

    public abstract int PartnyaBagItem3Qty();

    public abstract int PartnyaBagItem4ID();

    public abstract int PartnyaBagItem4Qty();

    public abstract int PartnyaBagItem5ID();

    public abstract int PartnyaBagItem5Qty();

    public abstract int PartnyaBagItem6ID();

    public abstract int PartnyaBagItem6Qty();

    public abstract int PartnyaBagItem7ID();

    public abstract int PartnyaBagItem7Qty();

    public abstract int PartnyaBagItem8ID();

    public abstract int PartnyaBagItem8Qty();

    public abstract int PartnyaBagItem9ID();

    public abstract int PartnyaBagItem9Qty();

    public abstract int PartnyaBagItem10ID();

    public abstract int PartnyaBagItem10Qty();

    public bool HasMonster1 => CaravanOverride() ? ShowHPBar(CaravanMonster1ID(), Monster1HPInt()) : ShowHPBar(LargeMonster1ID(), Monster1HPInt());

    public bool HasMonster2 => CaravanOverride() ? CaravanMonster2ID() > 0 && Monster2HPInt() != 0 && GetNotRoad() || Configuring : LargeMonster2ID() > 0 && Monster2HPInt() != 0 && GetNotRoad() || Configuring; // road check since the 2nd choice is used as the monster #1

    public bool HasMonster3 => ShowHPBar(LargeMonster3ID(), Monster3HPInt());

    public bool HasMonster4 => ShowHPBar(LargeMonster4ID(), Monster4HPInt());

    public static string FullCurrentProgramVersion
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "Monster Hunter Frontier Z Overlay {0}", App.CurrentProgramVersion);
        }
    }

    public string GetFullCurrentProgramVersion()
    {
        return string.Format(CultureInfo.InvariantCulture, "Monster Hunter Frontier Z Overlay {0}", App.CurrentProgramVersion);
    }

    public static string SimplifiedCurrentProgramVersion
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "MHF-Z Overlay {0}", App.CurrentProgramVersion);
        }
    }

    public static bool ShowHitsPerSecond()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.HitsPerSecondShown;
    }

    public static bool ShowTotalHitsTakenBlockedPerSecond()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.TotalHitsTakenBlockedPerSecondShown;
    }

    public string HitCount
    {
        get
        {
            var hitsPerSecond = string.Empty;

            if (ShowHitsPerSecond())
            {
                hitsPerSecond = string.Format(CultureInfo.InvariantCulture, " ({0:0.00}/s)", HitsPerSecond);
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", HitCountInt(), hitsPerSecond);
        }
    }

    private bool _configuring = false;

    /// <summary>
    /// Shows the monster ehp.
    /// </summary>
    /// <returns></returns>
    public static bool ShowMonsterEHP()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableEHPNumbers;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="AddressModel"/> is configuring.
    /// </summary>
    /// <value>
    ///   <c>true</c> if configuring; otherwise, <c>false</c>.
    /// </value>
    public bool Configuring { get { return _configuring; } set { _configuring = value; ReloadData(); } }

    public static bool IsAlwaysShowingMonsterInfo()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.AlwaysShowMonsterInfo;
    }

    /// <summary>
    /// Shows the hp bar?
    /// </summary>
    /// <param name="monsterId">The monster identifier.</param>
    /// <param name="monsterHp">The monster hp.</param>
    /// <returns></returns>
    public bool ShowHPBar(int monsterId, int monsterHp)
    {
        return monsterId > 0 && monsterHp != 0 || Configuring || IsAlwaysShowingMonsterInfo();
    }

    public bool ShowOverlayStatText
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            if (s.OverlayStatIconShown)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool ShowOverlayStatIcon
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            return s.OverlayStatIconShown;
        }
    }

    public bool? roadOverride()
    {
        // should work
        if (QuestID() != 23527 && QuestID() != 23628)
        {
            return true;
        }
        else if (QuestID() == 23527 || QuestID() == 23628)
        {
            return false;
        }

        return null;
    }

    /// <summary>
    /// Gets the name of the quest.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetQuestNameFromID(int id)
    {
        if (!DiscordService.ShowDiscordQuestNames())
        {
            return string.Empty;
        }

        string? QuestValue1;
        EZlion.Mapper.Quest.IDName.TryGetValue(id, out QuestValue1);  //returns true

        return QuestValue1 + string.Empty;
    }

    public string QuestTimeMode
    {
        get
        {
            switch (GetTimerMode())
            {
                case "Time Left":
                    return "Time Left";
                case "Time Elapsed":
                    return "Elapsed";
                default:
                    return "Quest Time";
            }
        }
    }

    // TODO need to create another variable for discordManager. Ideally discordManager state only affects it.
    public int PreviousHubAreaID;

    public bool closedGame;

    public bool isInLauncherBool;

    readonly Mem m = new();

    // TODO convert to bool and remove isInLauncherBool?
    public string isInLauncher()
    {
        // TODO: test
        if (QuestID() != 0 ||
        AreaID() != 0 && Location.IDName.ContainsKey(AreaID()))
        {
            return "No";
        }

        var pidToSearch = m.GetProcIdFromName("mhf");

        // Init a condition indicating that you want to search by process id.
        var condition = new PropertyCondition(AutomationElementIdentifiers.ProcessIdProperty,
            pidToSearch);

        AutomationElement? element = null;

        // Find the automation element matching the criteria
        // TODO: does this even fix anything?
        try
        {
            element = AutomationElement.RootElement.FindFirst(
TreeScope.Children, condition);
        }
        catch (Exception ex)
        {
            LoggerInstance.Warn(ex, "Could not find AutomationElement");
        }

        if (element == null || pidToSearch == 0)
        {
            return "NULL"; // TODO this looks ugly
        }

        // get the classname
        var className = element.Current.ClassName;

        if (className == "MHFLAUNCH")
        {
            return "Yes";

        }
        else
        {
            return "No";
        }
    }

    public string GetOverlayModeForRPC()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        if (s.ShowDiscordRPCOverlayMode)
        {
            return GetOverlayMode();
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Gets the overlay mode.
    /// </summary>
    /// <returns></returns>
    public string GetOverlayMode()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        if (Configuring)
        {
            return "(Configuring) ";
        }
        else if (closedGame)
        {
            return "(Closed Game) ";
        }
        else if (isInLauncherBool || isInLauncher() == "Yes") //works?
        {
            return "(Launcher) ";
        }
        else if (isInLauncher() == "NULL")
        {
            return "(No game detected) ";
        }
        else if (QuestID() == 0 && AreaID() == 0 && BlademasterWeaponID() == 0 && GunnerWeaponID() == 0)
        {
            return "(Main Menu) ";
        }
        else if (QuestID() == 0 && AreaID() == 200 && BlademasterWeaponID() == 0 && GunnerWeaponID() == 0)
        {
            return "(World Select) ";
        }
        else if (
            !(
                QuestID() != 0
                && TimeDefInt() > TimeInt()
                && int.Parse(ATK, CultureInfo.InvariantCulture) > 0
                || IsRoad() || IsDure()
            )
            || s.EnableDamageNumbers
            || s.EnableSharpness
            || s.PartThresholdShown
            || s.HitCountShown
            || s.PlayerAtkShown
            || s.MonsterAtkMultShown
            || s.MonsterDefrateShown
            || s.MonsterSizeShown
            || s.MonsterPoisonShown
            || s.MonsterParaShown
            || s.MonsterSleepShown
            || s.MonsterBlastShown
            || s.MonsterStunShown
            || s.DamagePerSecondShown
            || s.TotalHitsTakenBlockedShown
            || s.PlayerAPMGraphShown
            || s.PlayerAttackGraphShown
            || s.PlayerDPSGraphShown
            || s.PlayerHitsPerSecondGraphShown
            || s.EnableQuestPaceColor
            || s.Monster1HealthBarShown
            || s.Monster2HealthBarShown
            || s.Monster3HealthBarShown
            || s.Monster4HealthBarShown
            || s.EnableMap
            || s.PersonalBestTimePercentShown
            || s.EnablePersonalBestPaceColor) // TODO monster 1 overview? and update README
        {
            return string.Empty;
        }
        else if (s.TimerInfoShown && s.EnableInputLogging && s.EnableQuestLogging && PartySize() == 1 && s.OverlayModeWatermarkShown)
        {
            if (DivaSkillUsesLeft() == 0 && StyleRank1() != 15 && StyleRank2() != 15)
            {
                return "(Time Attack) ";
            }
            else if (StyleRank1() == 15 || StyleRank2() == 15)
            {
                return "(Freestyle w/ Secret Tech) ";
            }
            else
            {
                return "(Freestyle No Secret Tech) ";
            }
        }
        else
        {
            return "(Zen) ";
        }

    }

    private bool QuestNameContainsAlternativeTitle()
    {
        return GetQuestNameFromID(QuestID()).Contains("Daily Limited Quest≫") ||
            GetQuestNameFromID(QuestID()).Contains("Daily Quest≫") ||
            GetQuestNameFromID(QuestID()).Contains("Guild Quest≫") ||
            GetQuestNameFromID(QuestID()).Contains("Interception Base≫") ||
            GetQuestNameFromID(QuestID()).Contains("Interception Quest≫") ||
            GetQuestNameFromID(QuestID()).Contains("Interception Urgent Quest≫") ||
            GetQuestNameFromID(QuestID()).Contains("Great Slaying Quest≫") ||
            GetQuestNameFromID(QuestID()).Contains("G Rank Great Slaying≫") ||
            GetQuestNameFromID(QuestID()).Contains("New Weapon Type Acquisition≫");
    }

    private bool PreviousHubAreaIDIsAlternative()
    {
        switch (PreviousHubAreaID)
        {
            default:
                return false;
            case 210://Private Bar
            case 260://Pallone Caravan
            case 282://Cities Map
            case 202://Guild Halls
            case 203:
            case 204:
                return true;
        }
    }

    private bool IsAlternativeQuestName()
    {
        if (QuestNameContainsAlternativeTitle() || PreviousHubAreaIDIsAlternative())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CaravanOverride()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        switch (s.EnableCaravanOverride)
        {
            case "Enabled":
                return true;
            case "Disabled":
                return false;
            case "Automatic":
                return IsAlternativeQuestName();
            default:
                return false;
        }
    }

    public bool GetNotRoad()
    {
        var b = roadOverride();
        if (b != null)
        {
            return b.Value;
        }

        return IsNotRoad();
    }

    // assumption: it follows ferias' monster part order top to bottom, presumably (e.g. head is at the top, so part 0 is head, and so on)
    // grouping by skeleton too

    ///<summary>
    /// Monster parts labels
    ///<para>int number: The part number from 1 to 10</para>
    ///<para>int monsterID: the monsterID</para>
    ///</summary>
    public string GetPartName(int number, int monsterID)
    {
        //keep in mind this has the null
        if (roadOverride() == false)
        {
            monsterID = RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID();
        }
        else if (CaravanOverride())
        {
            monsterID = CaravanMonster1ID();
        }

        if (getDureName() != "None")
        {
            monsterID = 132;
        }

        if (getRaviName() != "None")
        {
            switch (getRaviName())
            {
                case "Raviente":
                case "Violent Raviente":
                    monsterID = 93;
                    break;
                case "Berserk Raviente Practice":
                case "Berserk Raviente":
                case "Extreme Raviente":
                    monsterID = 149;
                    break;
            }
        }

        if (monsterID > 176)
        {
            return "Error: ";
        }
        else
        {
            if (number <= 0 && number >= 11)
            {
                return "None: ";
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}: ", FindPartName(number, monsterID));
            }
        }
    }

    /// <summary>
    /// https://stackoverflow.com/a/35775810/18859245
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private static string FindPartName(int number, int monsterID)
    {
        var partMonsterGroup = new List<int> { 0 };

        foreach (var kvp in MonsterPart.IDName)
        {
            var monsterIDs = kvp.Key;

            if (monsterIDs.Contains(monsterID))
            {
                partMonsterGroup = kvp.Key;
                break;
            }
        }

        return DeterminePartName(partMonsterGroup, number - 1);
    }

    private static string DeterminePartName(List<int> key, int slot)
    {
        var keyFound = MonsterPart.IDName.ContainsKey(key);

        if (!keyFound)
        {
            return "None";
        }
        else
        {
            if (slot > MonsterPart.IDName[key].Count)
            {
                return "None";
            }
            else
            {
                return MonsterPart.IDName[key][slot];
            }
        }
    }

    public string Monster1Part1Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part1();
            return GetPartName(1, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part2Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part2();
            return GetPartName(2, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part3Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part3();
            return GetPartName(3, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part4Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part4();
            return GetPartName(4, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part5Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part5();
            return GetPartName(5, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part6Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part6();
            return GetPartName(6, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part7Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part7();
            return GetPartName(7, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part8Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part8();
            return GetPartName(8, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part9Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part9();
            return GetPartName(9, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster1Part10Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster1Part10();
            return GetPartName(10, LargeMonster1ID()) + currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part1Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part1();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part2Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part2();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part3Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part3();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part4Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part4();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part5Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part5();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part6Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part6();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part7Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part7();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part8Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part8();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part9Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part9();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster2Part10Number
    {
        get
        {
            if (QuestID() == 0)
            {
                return Messages.MonsterPartNotLoaded;
            }

            var currentPartHP = Monster2Part10();
            return currentPartHP.ToString(CultureInfo.InvariantCulture);
        }
    }



    /// <summary>
    /// Shows the sharpness percentage.
    /// </summary>
    /// <returns></returns>
    public static bool ShowSharpnessPercentage()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableSharpnessPercentage;
    }

    /// <summary>
    /// Shows the time left percentage.
    /// </summary>
    /// <returns></returns>
    public static bool ShowTimeLeftPercentage()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableTimeLeftPercentage;
    }

    /// <summary>
    /// Gets the timer mode.
    /// </summary>
    /// <returns></returns>
    public static string GetTimerMode()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        if (s.TimerMode == "Time Left")
        {
            return "Time Left";
        }
        else if (s.TimerMode == "Time Elapsed")
        {
            return "Time Elapsed";
        }
        else
        {
            return "Time Left";
        }
    }

    private int MaxSharpness;

    private string TimeLeftPercent = string.Empty;

    public string TimeLeftPercentNumber
    {
        get
        {
            if (TimeDefInt() < TimeInt())
            {
                return "0";
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, " ({0:0}%)", (float)TimeInt() / TimeDefInt() * 100.0);
            }
        }
    }

    public string SharpnessPercentNumber
    {
        get
        {
            if (ShowSharpnessPercentage())
            {
                if (MaxSharpness < Sharpness())
                {
                    MaxSharpness = Sharpness();
                    return string.Format(CultureInfo.InvariantCulture, " ({0:0}%)", (float)Sharpness() / MaxSharpness * 100.0);
                }
                else if (Sharpness() <= 0)
                {
                    return " (0%)";
                }
                else // MaxSharpness > CurrentSharpness && CurrentSharpness > 0
                {
                    return string.Format(CultureInfo.InvariantCulture, " ({0:0}%)", (float)Sharpness() / MaxSharpness * 100.0);
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }

    ///<summary>
    /// Quest time in the format of mm:ss.fff. This should only be used for display purposes.
    ///</summary>
    public string Time
    {
        get
        {
            int time;

            if (GetTimerMode() == "Time Elapsed")
            {
                time = TimeDefInt() - TimeInt();
            }
            else if (GetTimerMode() == "Time Left")
            {
                time = TimeInt();
            }
            else // default to Time Left mode
            {
                time = TimeInt();
            }

            if (time > 0)
            {

                if (ShowTimeLeftPercentage())
                {
                    TimeLeftPercent = TimeLeftPercentNumber;
                }
                else
                {
                    TimeLeftPercent = string.Empty;
                }

                if (time / Numbers.FramesPerSecond / 60 < 10)
                {
                    if (time / Numbers.FramesPerSecond % 60 < 10)
                    {
                        return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}.{2:000}", time / Numbers.FramesPerSecond / 60, time / Numbers.FramesPerSecond % 60, (int)Math.Round((float)(time % Numbers.FramesPerSecond * 100) / 3)) + TimeLeftPercent;//should work fine
                    }
                    else
                    {
                        return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1}.{2:000}", time / Numbers.FramesPerSecond / 60, time / Numbers.FramesPerSecond % 60, (int)Math.Round((float)(time % Numbers.FramesPerSecond * 100) / 3)) + TimeLeftPercent;
                    }
                }
                else
                {
                    if (time / Numbers.FramesPerSecond % 60 < 10)
                    {
                        return string.Format(CultureInfo.InvariantCulture, "{0}:{1:00}.{2:000}", time / Numbers.FramesPerSecond / 60, time / Numbers.FramesPerSecond % 60, (int)Math.Round((float)(time % Numbers.FramesPerSecond * 100) / 3)) + TimeLeftPercent;
                    }
                    else
                    {
                        return string.Format(CultureInfo.InvariantCulture, "{0}:{1}.{2:000}", time / Numbers.FramesPerSecond / 60, time / Numbers.FramesPerSecond % 60, (int)Math.Round((float)(time % Numbers.FramesPerSecond * 100) / 3)) + TimeLeftPercent;
                    }
                }
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}.{2:000}", time / Numbers.FramesPerSecond / 60, time / Numbers.FramesPerSecond % 60, (int)Math.Round((float)(time % Numbers.FramesPerSecond * 100) / 3)) + TimeLeftPercent;
            }
        }
    }

    // per quest
    public int HighestAtk { get; set; }

    public double HighestDPS { get; set; }

    /// <summary>
    /// Shows the color of the highest atk.
    /// </summary>
    /// <returns></returns>
    public static bool ShowHighestAtkColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableHighestAtkColor;
    }

    public static bool ShowHighestDPSColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableHighestDPSColor;
    }

    public static bool ShowAverageHitsPerSecondColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableAverageHitsPerSecondColor;
    }

    public static bool ShowAverageAPMColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableAverageActionsPerMinuteColor;
    }

    public static bool ShowHitsTakenBlockedColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableTotalHitsTakenBlockedColor;
    }

    public static bool ShowQuestPaceColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableQuestPaceColor;
    }

    public static bool ShowPersonalBestPaceColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnablePersonalBestPaceColor;
    }

    public static bool ShowHighestMonsterAttackMultiplierColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableHighestMonsterAttackMultiplierColor;
    }

    public static bool ShowLowestMonsterDefrateColor()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableLowestMonsterDefrateColor;
    }

    public string isHighestAtk
    {
        get
        {
            if (WeaponRaw() == HighestAtk && ShowHighestAtkColor())
            {
                PlayerAttackIcon = "../../Assets/Icons/png/attack_up_red.png";
                return "#f38ba8";
            }
            else
            {
                PlayerAttackIcon = "../../Assets/Icons/png/attack_up.png";
                return "#f5e0dc";
            }
        }
    }

    public string isHighestDPS
    {
        get
        {
            if (DPS + 10 >= HighestDPS && ShowHighestDPSColor())
            {
                DPSIcon = "../../Assets/Icons/png/burst_red.png";
                return "#f38ba8";
            }
            else
            {
                DPSIcon = "../../Assets/Icons/png/burst.png";
                return "#f5e0dc";
            }
        }
    }

    public string isAverageHitsPerSecond
    {
        get
        {
            if (HitsPerSecond >= GetAverageHitsPerSecond(WeaponType()) && ShowAverageHitsPerSecondColor())
            {
                HitCountIcon = "../../Assets/Icons/png/red_soul.png";
                return "#f38ba8";
            }
            else
            {
                HitCountIcon = "../../Assets/Icons/png/blue_soul.png";
                return "#f5e0dc";
            }
        }
    }

    public string isAverageAPM
    {
        get
        {
            // get the average hits of a weapon per second, multiply by 60 to get the minutes, multiply by 2 to account for other actions
            if (APM >= GetAverageHitsPerSecond(WeaponType()) * 60 * 2 && ShowAverageAPMColor())
            {
                APMIcon = "../../Assets/Icons/png/flame_hc.png";
                return "#f38ba8";
            }
            else
            {
                APMIcon = "../../Assets/Icons/png/flame_ul.png";
                return "#f5e0dc";
            }
        }
    }

    public string isPlayerHit
    {
        get
        {
            var weaponFound = WeaponCanUseReflect.WeaponTypeID.ContainsKey(WeaponType());
            if (!weaponFound)
            {
                return "#f5e0dc";
            }

            var hasReflect = false;

            if (WeaponCanUseReflect.WeaponTypeID[WeaponType()])
            {
                hasReflect = true;
            }

            // for speedrunners to get hit by 1 small bomb at least. and 2 perfect guards to get max obscurity.
            if (!hasReflect && TotalHitsTakenBlocked <= 1 || hasReflect && TotalHitsTakenBlocked <= 3 && ShowHitsTakenBlockedColor())
            {
                PlayerHitsTakenBlockedIcon = "../../Assets/Icons/png/defense_up.png";
                return "#f38ba8";
            }
            else
            {
                PlayerHitsTakenBlockedIcon = "../../Assets/Icons/png/defense_down.png";
                return "#f5e0dc";
            }
        }
    }

    public double GetAverageHitsPerSecond(int weaponTypeID)
    {
        var weaponFound = WeaponCanUseReflect.WeaponTypeID.ContainsKey(weaponTypeID);
        if (!weaponFound)
        {
            return 0;
        }

        var averageFound = WeaponAverageHitsPerSecond.WeaponAverageHitsPerSecondID.ContainsKey(weaponTypeID);
        if (!averageFound)
        {
            return 0;
        }

        var averageMultiplier = 0.5;

        if (!WeaponCanUseReflect.WeaponTypeID[weaponTypeID])
        {
            averageMultiplier = 0.4;
        }

        return WeaponAverageHitsPerSecond.WeaponAverageHitsPerSecondID[weaponTypeID] * averageMultiplier;
    }

    public string isOnPace
    {
        get
        {
            if (TimeDefInt() == 0 || int.Parse(Monster1MaxHP, CultureInfo.InvariantCulture) <= 1)
            {
                QuestTimeIcon = "../../Assets/Icons/png/sand_clock.png";
                return "#f5e0dc";
            }

            if (TimeDefInt() < TimeInt())
            {
                QuestTimeIcon = "../../Assets/Icons/png/sand_clock.png";
                return "#f5e0dc";
            }

            var timePercent = (float)TimeInt() / TimeDefInt() * 100.0;
            var monster1HPPercent = (float)Monster1HPInt() / int.Parse(Monster1MaxHP, CultureInfo.InvariantCulture) * 100.0;

            if (timePercent >= monster1HPPercent && ShowQuestPaceColor())
            {
                if (GetTimerMode() == "Time Left")
                {
                    QuestTimeIcon = "../../Assets/Icons/png/sand_clock2_red.png";
                }
                else
                {
                    QuestTimeIcon = "../../Assets/Icons/png/clock_red.png";
                }

                return "#f38ba8";

            }
            else
            {
                if (GetTimerMode() == "Time Left")
                {
                    QuestTimeIcon = "../../Assets/Icons/png/sand_clock2.png";
                }
                else
                {
                    QuestTimeIcon = "../../Assets/Icons/png/clock.png";
                }

                return "#f5e0dc";
            }
        }
    }

    public string isOnBestPace
    {
        get
        {
            if (TimeDefInt() == 0 || int.Parse(Monster1MaxHP) <= 1)
            {
                PersonalBestIcon = "../../Assets/Icons/png/quest_clock.png";
                return "#f5e0dc";
            }

            if (TimeDefInt() < TimeInt())
            {
                PersonalBestIcon = "../../Assets/Icons/png/quest_clock.png";
                return "#f5e0dc";
            }

            var timePercent = int.Parse(PersonalBestTimePercent.Replace("%", string.Empty));
            var monster1HPPercent = (float)Monster1HPInt() / int.Parse(Monster1MaxHP, CultureInfo.InvariantCulture) * 100.0;

            if (timePercent >= monster1HPPercent && ShowPersonalBestPaceColor())
            {
                PersonalBestIcon = "../../Assets/Icons/png/quest_clock_red.png";
                return "#f38ba8";
            }
            else
            {
                PersonalBestIcon = "../../Assets/Icons/png/quest_clock.png";
                return "#f5e0dc";
            }
        }
    }

    public string PersonalBestLoaded = Messages.TimerNotLoaded;

    public string PersonalBestTimePercent
    {
        get
        {
            if (PersonalBestLoaded != Messages.TimerNotLoaded && ShowPersonalBestPaceColor())
            {
                const int framesPerSecond = 30;
                var personalBestInFrames = (int)(framesPerSecond * TimeSpan.ParseExact(PersonalBestLoaded, "mm':'ss'.'fff", CultureInfo.InvariantCulture).TotalSeconds);
                var personalBestTimeFramesElapsed = 0;
                if (GetTimerMode() == "Time Left")
                {
                    personalBestTimeFramesElapsed = TimeDefInt() - personalBestInFrames;
                }
                else
                {
                    personalBestTimeFramesElapsed = personalBestInFrames;
                }

                var elapsedPersonalBestTimePercent = CalculatePersonalBestInFramesPercent(personalBestTimeFramesElapsed);

                return string.Format(CultureInfo.InvariantCulture, "{0:0}%", elapsedPersonalBestTimePercent);
            }
            else
            {
                return "0%";
            }
        }
    }

    public double CalculatePersonalBestInFramesPercent(double personalBestInFramesElapsed)
    {
        if (personalBestInFramesElapsed <= 0)
        {
            return 0;
        }
        else
        {
            return 100 - (TimeDefInt() - TimeInt()) / personalBestInFramesElapsed * 100.0;
        }
    }

    private double HighestAttackMult { get; set; }

    private decimal LowestMonsterDefrate { get; set; } = 1_000;

    public string isHighestMonsterAttackMultiplier
    {
        get
        {
            if (AtkMult == HighestAttackMult.ToString(CultureInfo.InvariantCulture) && ShowHighestMonsterAttackMultiplierColor())
            {
                MonsterAttackMultiplierIcon = "../../Assets/Icons/png/dure_attack_red.png";
                return "#f38ba8";
            }
            else
            {
                MonsterAttackMultiplierIcon = "../../Assets/Icons/png/dure_attack.png";
                return "#f5e0dc";
            }
        }
    }

    public string isLowestMonsterDefrate
    {
        get
        {
            if (DefMult == LowestMonsterDefrate.ToString(CultureInfo.InvariantCulture) && ShowLowestMonsterDefrateColor())
            {
                MonsterDefrateIcon = "../../Assets/Icons/png/dure_defense_red.png";
                return "#f38ba8";
            }
            else
            {
                MonsterDefrateIcon = "../../Assets/Icons/png/dure_defense.png";
                return "#f5e0dc";
            }
        }
    }

    public string MonsterAttackMultiplierIcon { get; set; } = "../../Assets/Icons/png/dure_attack.png";

    public string MonsterDefrateIcon { get; set; } = "../../Assets/Icons/png/dure_defense.png";

    public string DPSIcon { get; set; } = "../../Assets/Icons/png/burst.png";

    public string QuestTimeIcon { get; set; } = "../../Assets/Icons/png/clock.png";

    public string PlayerAttackIcon { get; set; } = "../../Assets/Icons/png/attack_up.png";

    public string PlayerHitsTakenBlockedIcon { get; set; } = "../../Assets/Icons/png/defense_up.png";

    public string HitCountIcon { get; set; } = "../../Assets/Icons/png/blue_soul.png";

    public string APMIcon { get; set; } = "../../Assets/Icons/png/flame_ul.png";

    public string PersonalBestIcon { get; set; } = "../../Assets/Icons/png/quest_clock.png";

    ///<summary>
    ///<para>Player true raw</para>
    ///<br>Attack addition is added as either Attack A or Attack B and is before or after Hunting Horn buffs respectively.</br>
    ///<br>Values that are known to reside in Attack B are Rush, Stylish Assault, Flash Conversion, Obscurity, Incitement, Rush, Vigorous Up and Partnyaa Attack Buffs.</br>
    ///<para>Final True  = ((Weapon True + Attack A) * HH Buff + Attack B) * Multipliers + Additional</para>
    ///</summary>
    public string ATK
    {
        get
        {
            var weaponRaw = WeaponRaw();

            if (QuestID() == 0) //should work fine
            {
                HighestAtk = 0;
                HighestDPS = 0;
                HighestAttackMult = 0; // i get stackoverflow otherwise
                LowestMonsterDefrate = 1_000; // should be enough

            }

            if (weaponRaw > HighestAtk)
            {
                HighestAtk = weaponRaw;
            }

            if (DPS > HighestDPS)
            {
                HighestDPS = DPS;
            }

            if (double.Parse(AtkMult, CultureInfo.InvariantCulture) > HighestAttackMult)
            {
                HighestAttackMult = double.Parse(AtkMult, CultureInfo.InvariantCulture);
            }

            if (decimal.Parse(DefMult) < LowestMonsterDefrate && decimal.Parse(DefMult, CultureInfo.InvariantCulture) != 0)
            {
                LowestMonsterDefrate = decimal.Parse(DefMult, CultureInfo.InvariantCulture);
            }

            return weaponRaw.ToString(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Gets the color of the sharpness.
    /// </summary>
    /// <value>
    /// The color of the sharpness.
    /// </value>
    public string SharpnessColor
    {
        get
        {
            // see palettes.md
            var currentSharpnessLevel = SharpnessLevel();
            return currentSharpnessLevel switch
            {
                0 => "#c50f3a",// Red
                1 => "#e85218",// Orange
                2 => "#f3c832",// Yellow
                3 => "#5ed300",// Green
                4 => "#3068ee",// Blue
                5 => "#f0f0f0",// White
                6 => "#de7aff",// Purple
                7 => "#86f4f4",// Cyan
                _ => "#ffffff",
            };
        }
    }

    /// <summary>
    /// Gets the sharpness number.
    /// </summary>
    /// <value>
    /// The sharpness number.
    /// </value>
    public string SharpnessNumber
    {
        get
        {
            var currentSharpness = Sharpness();
            if (currentSharpness > 0)
            {
                return currentSharpness.ToString(CultureInfo.InvariantCulture) + SharpnessPercentNumber;
            }

            return "0" + SharpnessPercentNumber;
        }
    }

    /// <summary>
    /// Gets the current weapon multiplier.
    /// </summary>
    /// <value>
    /// The current weapon multiplier.
    /// </value>
    public float CurrentWeaponMultiplier
    {
        get
        {
            var weaponType = WeaponType();
            return GetMultFromWeaponType(weaponType);
        }
    }

    /// <summary>
    /// Gets the name of the current weapon.
    /// </summary>
    /// <value>
    /// The name of the current weapon.
    /// </value>
    public string CurrentWeaponName
    {
        get
        {
            var weaponType = WeaponType();
            return GetWeaponNameFromType(weaponType);
        }
    }

    /// <summary>
    /// Gets the size.
    /// </summary>
    /// <value>
    /// The size.
    /// </value>
    public string Size
    {
        get
        {
            return SelectedMonster switch
            {
                0 => Monster1Size(),
                1 => Monster2Size(),
                _ => Monster1Size(),
            };
        }
    }

    /// <summary>
    /// Gets the atk mult.
    /// </summary>
    /// <value>
    /// The atk mult.
    /// </value>
    public string AtkMult
    {
        get
        {
            return SelectedMonster switch
            {
                0 => Monster1AtkMult(),
                1 => Monster2AtkMult(),
                _ => Monster1AtkMult(),
            };
        }
    }

    /// <summary>
    /// Gets the defrate multiplier.
    /// </summary>
    /// <value>
    /// The defrate multiplier.
    /// </value>
    public string DefMult
    {
        get
        {
            switch (SelectedMonster)
            {
                case 0:
                    return Monster1DefMult().ToString(CultureInfo.InvariantCulture);
                case 1:
                    return Monster2DefMult().ToString(CultureInfo.InvariantCulture);
                default:
                    return Monster1DefMult().ToString(CultureInfo.InvariantCulture);
            }
        }
    }

    /// <summary>
    /// Gets the size.
    /// </summary>
    /// <value>
    /// The size.
    /// </value>
    public double Monster1SizeMultForDictionary()
    {
        return SelectedMonster switch
        {
            0 => double.Parse(Monster1Size().Replace("%", string.Empty), CultureInfo.InvariantCulture),
            1 => double.Parse(Monster2Size().Replace("%", string.Empty), CultureInfo.InvariantCulture),
            _ => double.Parse(Monster1Size().Replace("%", string.Empty), CultureInfo.InvariantCulture),
        };
    }

    /// <summary>
    /// Gets the atk mult.
    /// </summary>
    /// <value>
    /// The atk mult.
    /// </value>
    public double Monster1AttackMultForDictionary()
    {
        return SelectedMonster switch
        {
            0 => double.Parse(Monster1AtkMult(), CultureInfo.InvariantCulture),
            1 => double.Parse(Monster2AtkMult(), CultureInfo.InvariantCulture),
            _ => double.Parse(Monster1AtkMult(), CultureInfo.InvariantCulture),
        };
    }

    /// <summary>
    /// Gets the defrate multiplier.
    /// </summary>
    /// <value>
    /// The defrate multiplier.
    /// </value>
    public double Monster1DefMultForDictionary()
    {
        switch (SelectedMonster)
        {
            case 0:
                return double.Parse(Monster1DefMult().ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            case 1:
                return double.Parse(Monster2DefMult().ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            default:
                return double.Parse(Monster1DefMult().ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }
    }

    private decimal previousMonsterDefrate = decimal.Zero;

    /// <summary>
    /// Displays the monster ehp.
    /// </summary>
    /// <param name="defrate">The defrate.</param>
    /// <param name="monsterhp">The monsterhp.</param>
    /// <param name="monsterdefrate">The monsterdefrate.</param>
    /// <returns></returns>
    public int DisplayMonsterEHP(decimal monsterDefrate, int monsterHP)
    {
        if (QuestID() == 0)
        {
            previousMonsterDefrate = decimal.Zero;
            return 0;
        }

        var s = (Settings)Application.Current.TryFindResource("Settings");
        if (s.EnableMonsterEHPDisplayCorrector)
        {
            if (s.MonsterEHPDisplayCorrectorDefrateMinimumThreshold >= s.MonsterEHPDisplayCorrectorDefrateMaximumThreshold)
            {
                return monsterHP;
            }

            if (monsterDefrate > s.MonsterEHPDisplayCorrectorDefrateMinimumThreshold && monsterDefrate < s.MonsterEHPDisplayCorrectorDefrateMaximumThreshold)
            {
                previousMonsterDefrate = monsterDefrate;
                var result = Convert.ToDecimal(monsterHP / previousMonsterDefrate);
                if (result <= int.MaxValue && result >= int.MinValue)
                {
                    return Convert.ToInt32(result);

                }
                else
                {
                    return monsterHP;
                }
            }
            else
            {
                if (previousMonsterDefrate > 0)
                {
                    var result = Convert.ToDecimal(monsterHP / previousMonsterDefrate);

                    if (result <= int.MaxValue && result >= int.MinValue)
                    {
                        return Convert.ToInt32(result);

                    }
                    else
                    {
                        return monsterHP;
                    }
                }
                else
                {
                    return monsterHP;
                }
            }
        }
        else
        {
            if (monsterDefrate > 0)
            {
                var result = Convert.ToDecimal(monsterHP / monsterDefrate);

                if (result <= int.MaxValue && result >= int.MinValue)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;

                    // Handle the case where the result is too large or too small for an int
                    // Return an appropriate value or throw an exception if necessary
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// Reloads the maximum ehp.
    /// </summary>
    public void ReloadMaxEHP()
    {
        if (savedMonster1MaxHP < Monster1HPInt())
        {
            savedMonster1MaxHP = (int)(Monster1HPInt() / Monster1DefMult());
        }

        if (savedMonster2MaxHP < Monster2HPInt())
        {
            savedMonster2MaxHP = (int)(Monster2HPInt() / Monster2DefMult());
        }

        if (savedMonster3MaxHP < Monster3HPInt())
        {
            savedMonster3MaxHP = (int)(Monster3HPInt() / float.Parse("1", CultureInfo.InvariantCulture.NumberFormat));
        }

        if (savedMonster4MaxHP < Monster4HPInt())
        {
            savedMonster4MaxHP = (int)(Monster4HPInt() / float.Parse("1", CultureInfo.InvariantCulture.NumberFormat));
        }
    }

    /// <summary>
    /// Gets the current poison.
    /// </summary>
    public int PoisonCurrent
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1Poison(),
                1 => Monster2Poison(),
                _ => Monster1Poison(),
            };
        }
    }

    /// <summary>
    /// Gets the poison maximum.
    /// </summary>
    /// <value>
    /// The poison maximum.
    /// </value>
    public int PoisonMax
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1PoisonNeed(),
                1 => Monster2PoisonNeed(),
                _ => Monster1PoisonNeed(),
            };
        }
    }

    /// <summary>
    /// Gets the current sleep.
    /// </summary>
    public int SleepCurrent
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1Sleep(),
                1 => Monster2Sleep(),
                _ => Monster1Sleep(),
            };
        }
    }

    /// <summary>
    /// Gets the sleep maximum.
    /// </summary>
    /// <value>
    /// The sleep maximum.
    /// </value>
    public int SleepMax
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1SleepNeed(),
                1 => Monster2SleepNeed(),
                _ => Monster1SleepNeed(),
            };
        }
    }

    /// <summary>
    /// Gets the current paralysis.
    /// </summary>
    public int ParaCurrent
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1Para(),
                1 => Monster2Para(),
                _ => Monster1Para(),
            };
        }
    }

    /// <summary>
    /// Gets the para maximum.
    /// </summary>
    /// <value>
    /// The para maximum.
    /// </value>
    public int ParaMax
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1ParaNeed(),
                1 => Monster2ParaNeed(),
                _ => Monster1ParaNeed(),
            };
        }
    }

    /// <summary>
    /// Gets the current blast.
    /// </summary>
    public int BlastCurrent
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1Blast(),
                1 => Monster2Blast(),
                _ => Monster1Blast(),
            };
        }
    }

    /// <summary>
    /// Gets the blast maximum.
    /// </summary>
    /// <value>
    /// The blast maximum.
    /// </value>
    public int BlastMax
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1BlastNeed(),
                1 => Monster2BlastNeed(),
                _ => Monster1BlastNeed(),
            };
        }
    }

    /// <summary>
    /// Gets the current stun.
    /// </summary>
    public int StunCurrent
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1Stun(),
                1 => Monster2Stun(),
                _ => Monster1Stun(),
            };
        }
    }

    /// <summary>
    /// Gets the stun maximum.
    /// </summary>
    /// <value>
    /// The stun maximum.
    /// </value>
    public int StunMax
    {
        get
        {
            if (Configuring)
            {
                return 100;
            }

            return SelectedMonster switch
            {
                0 => Monster1StunNeed(),
                1 => Monster2StunNeed(),
                _ => Monster1StunNeed(),
            };
        }
    }

    /// <summary>
    /// Gets the current poison.
    /// </summary>
    public int Monster1PoisonForDictionary()
    {
        return SelectedMonster switch
        {
            0 => Monster1Poison(),
            1 => Monster2Poison(),
            _ => Monster1Poison(),
        };
    }

    /// <summary>
    /// Gets the current sleep.
    /// </summary>
    public int Monster1SleepForDictionary()
    {
        return SelectedMonster switch
        {
            0 => Monster1Sleep(),
            1 => Monster2Sleep(),
            _ => Monster1Sleep(),
        };
    }

    /// <summary>
    /// Gets the current paralysis.
    /// </summary>
    public int Monster1ParalysisForDictionary()
    {
        return SelectedMonster switch
        {
            0 => Monster1Para(),
            1 => Monster2Para(),
            _ => Monster1Para(),
        };
    }

    /// <summary>
    /// Gets the current blast.
    /// </summary>
    public int Monster1BlastForDictionary()
    {
        return SelectedMonster switch
        {
            0 => Monster1Blast(),
            1 => Monster2Blast(),
            _ => Monster1Blast(),
        };
    }

    /// <summary>
    /// Gets the current stun.
    /// </summary>
    public int Monster1StunForDictionary()
    {
        return SelectedMonster switch
        {
            0 => Monster1Stun(),
            1 => Monster2Stun(),
            _ => Monster1Stun(),
        };
    }



    /// <summary>
    /// Gets the name of Duremudira.
    /// </summary>
    /// <returns></returns>
    public string getDureName()
    {
        if (QuestID() == 21731 || QuestID() == 21749)
        {
            return "1st District Duremudira";
        }
        else if (QuestID() == 21746 || QuestID() == 21750)
        {
            return "2nd District Duremudira";
        }
        else if (QuestID() == 21747 || QuestID() == 21734)
        {
            return "3rd District Duremudira";
        }
        else if (QuestID() == 21748)
        {
            return "4th District Duremudira";
        }
        else if (QuestID() == 23648 || QuestID() == 23649)
        {
            return "Arrogant Duremudira";
        }
        else
        {
            return "None";
        }
    }

    // quest ids
    // ravi 62105 TODO: same ids in all phases?
    // violent 62101
    // berserk
    // berserk practice
    // support 1 55803
    // extreme
    /// <summary>
    /// Gets the name of the ravi.
    /// </summary>
    /// <returns></returns>
    public string getRaviName()
    {
        // quest ids:
        // mp road: 23527
        // solo road: 23628
        // 1st district dure: 21731
        // 2nd district dure: 21746
        // 1st district dure sky corridor: 21749
        // 2nd district dure sky corridor: 21750
        // arrogant dure repel: 23648
        // arrogant dure slay: 23649
        // urgent tower: 21751
        // 4th district dure: 21748
        // 3rd district dure: 21747
        // 3rd district dure 2: 21734
        // UNUSED sky corridor: 21730
        // sky corridor prologue: 21729
        // raviente 62105
        // raviente carve 62108
        // violent raviente 62101
        // violent carve 62104
        // berserk slay practice 55796
        // berserk support practice 1 55802
        // berserk support practice 2 55803
        // berserk support practice 3 55804
        // berserk support practice 4 55805
        // berserk support practice 5 55806
        // berserk practice carve 55807
        // berserk slay  54751
        // berserk support 1 54756
        // berserk support 2 54757
        // berserk support 3 54758
        // berserk support 4 54759
        // berserk support 5 54760
        // berserk carve 54761
        // extreme slay (musou table 54) 55596
        // extreme support 1 55602
        // extreme support 2 55603
        // extreme support 3 55604
        // extreme support 4 55605
        // extreme support 5 55606
        // extreme carve 55607
        if (QuestID() == 62105 || QuestID() == 62108)
        {
            return "Raviente";
        }
        else if (QuestID() == 62101 || QuestID() == 62104)
        {
            return "Violent Raviente";
        }
        else if (QuestID() == 55796 || QuestID() == 55802 || QuestID() == 55803 || QuestID() == 55804 || QuestID() == 55805 || QuestID() == 55806 || QuestID() == 55807)
        {
            return "Berserk Raviente Practice";
        }
        else if (QuestID() == 54751 || QuestID() == 54756 || QuestID() == 54757 || QuestID() == 54758 || QuestID() == 54759 || QuestID() == 54760 || QuestID() == 54761)
        {
            return "Berserk Raviente";
        }
        else if (QuestID() == 55596 || QuestID() == 55602 || QuestID() == 55603 || QuestID() == 55604 || QuestID() == 55605 || QuestID() == 55606 || QuestID() == 55607)
        {
            return "Extreme Raviente";
        }
        else
        {
            return "None";
        }
    }

    public string Monster1Name => getDureName() != "None" ? getDureName() : getRaviName() != "None" ? getRaviName() : getMonsterName(GetNotRoad() || RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID()); // monster 1 is used for the first display and road uses 2nd choice to store 2nd monster

    public string Monster2Name => CaravanOverride() ? getMonsterName(CaravanMonster2ID(), false) : getMonsterName(LargeMonster2ID(), false);

    public string Monster3Name => getMonsterName(LargeMonster3ID(), false);

    public string Monster4Name => getMonsterName(LargeMonster4ID(), false);

    /// <summary>
    /// Gets the name of the rank.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetRankName(int id)
    {
        switch (id)
        {
            case 0:
                return string.Empty;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                return "Low Rank ";
            case 11:
                return "Low/High Rank ";
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
                return "High Rank ";
            case 26:
            case 31:
            case 42:
                return "HR5 ";
            case 32:
            case 46://supremacies
                return "Supremacy ";
            case 53://: conquest levels via quest id
                    //shantien
                    //lv1 23585
                    //lv200 23586
                    //lv1000 23587
                    //lv9999 23588
                    //disufiroa
                    //lv1 23589
                    //lv200 23590
                    //lv1000 23591
                    //lv9999 23592
                    //fatalis
                    //lv1 23593
                    //lv200 23594
                    //lv1000 23595
                    //lv9999 23596
                    //crimson fatalis
                    //lv1 23597
                    //lv200 23598
                    //lv1000 23599
                    //lv9999 23601
                    //upper shiten unknown 23605
                    //lower shiten unknown 23604
                    //upper shiten disufiroa 23603
                    //lower shiten disu 23602
                    //thirsty 55532
                    //shifting 55920
                    //starving 55916
                    //golden 55917
                switch (QuestID())
                {
                    default:
                        return "G Rank ";
                    case 23585:
                    case 23589:
                    case 23593:
                    case 23597:
                        return "Lv1 ";
                    case 23586:
                    case 23590:
                    case 23594:
                    case 23598:
                        return "Lv200 ";
                    case 23587:
                    case 23591:
                    case 23595:
                    case 23599:
                        return "Lv1000 ";
                    case 23588:
                    case 23592:
                    case 23596:
                    case 23601:
                        return "Lv9999 ";
                }

            case 54:
                switch (QuestID())
                {
                    default:
                        return string.Empty;
                    case 23604:
                    case 23602:
                        return "Lower Shiten ";
                }

            case 55:
                switch (QuestID())
                {
                    default:
                        return string.Empty;
                    case 23603:
                        return "Upper Shiten ";
                }

            // 10m upper shiten/musou true slay
            case 56:// twinhead rajang / voljang and rajang
            case 57:// twinhead mi ru / white and brown espi / unknown and zeru / rajang and dorag
                return "Twinhead ";
            case 64:
                return "Zenith★1 ";
            case 65:
                return "Zenith★2 ";
            case 66:
                return "Zenith★3 ";
            case 67:
                return "Zenith★4 ";
            case 70://unknown
                return "Upper Shiten ";
            case 71:
            case 72:
            case 73:
                return "Interception ";
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// Gets the real name of the monster.
    /// </summary>
    /// <value>
    /// The real name of the monster.
    /// </value>
    public string RealMonsterName
    {
        get
        {
            // https://stackoverflow.com/questions/4315564/capitalizing-words-in-a-string-using-c-sharp
            int id;

            if (roadOverride() == false)
            {
                id = RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID();
            }
            else if (CaravanOverride())
            {
                id = CaravanMonster1ID();
            }
            else
            {
                id = LargeMonster1ID();
            }

            // dure
            if (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750)
            {
                return "Duremudira";
            }
            else if (QuestID() == 23648 || QuestID() == 23649)
            {
                return "Arrogant Duremudira";
            }

            return DetermineMonsterName(id);
        }

        // quest ids:
        // mp road: 23527
        // solo road: 23628
        // 1st district dure: 21731
        // 2nd district dure: 21746
        // 1st district dure sky corridor: 21749
        // 2nd district dure sky corridor: 21750
        // arrogant dure repel: 23648
        // arrogant dure slay: 23649
        // urgent tower: 21751
        // 4th district dure: 21748
        // 3rd district dure: 21747
        // 3rd district dure 2: 21734
        // UNUSED sky corridor: 21730
        // sky corridor prologue: 21729
    }

    public string DetermineMonsterName(int id)
    {
        var keyFound = MonsterNameCollection.MonsterNameID.TryGetValue(id, out var link);
        if (link == null)
        {
            link = "Loading...";
        }

        if (keyFound)
        {
            return link;
        }
        else
        {
            return GetAlternateMonsterName(id);
        }
    }

    public string GetAlternateMonsterName(int id)
    {
        switch (id)
        {
            case 65:
                return "Teostra";
            case 89:
                if (RankBand() == 54)
                {
                    return "Thirsty Pariapuria";
                }
                else
                {
                    return "Pariapuria";
                }

            case 95:
                return "Doragyurosu";
            case 106:
                return "Odibatorasu";
            case 113:
                if (RankBand() == 55)
                {
                    return "Shifting Mi Ru";
                }
                else
                {
                    return "Mi Ru";
                }

            case 146:
                if (RankBand() >= 54 && RankBand() <= 55)
                {
                    return "Howling Zinogre";
                }
                else
                {
                    return "Zinogre";
                }

            case 154:
                if (RankBand() >= 54 && RankBand() <= 55)
                {
                    return "Ruling Guanzorumu";
                }
                else
                {
                    return "Guanzorumu";
                }

            case 155:
                if (RankBand() == 55)
                {
                    return "Golden Deviljho";
                }
                else
                {
                    return "Starving Deviljho";
                }

            case 166:
                if (RankBand() >= 54 && RankBand() <= 55)
                {
                    return "Burning Freezing Elzelion";
                }
                else
                {
                    return "Elzelion";
                }

            default:
                return "Loading...";
        }
    }

    /// <summary>
    /// Gets the name of the monster.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string getMonsterName(int id, bool isFirstMonster = true)
    {
        if (Configuring)
        {
            return "Blitzkrieg Bogabadorumu";
        }

        if (id == 0)
        {
            return string.Empty;
        }

        MonsterCollection.MonsterID.TryGetValue(id, out var monstername);

        if (monstername != null && monstername != RealMonsterName && isFirstMonster)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", GetRankName(RankBand()), RealMonsterName);
        }
        else
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", GetRankName(RankBand()), monstername);
        }
    }

    public string Monster1HP
    {
        get
        {
            if (Configuring)
            {
                return "0";
            }
            else if (ShowMonsterEHP())
            {
                var monsterDefMultiplier = Monster1DefMult();
                var monsterHP = Monster1HPInt();
                return DisplayMonsterEHP(monsterDefMultiplier, monsterHP).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                var monsterHP = Monster1HPInt();
                return monsterHP.ToString(CultureInfo.InvariantCulture);
            }
        }
    }

    public string Monster1MaxHP
    {
        get
        {
            if (Configuring)
            {
                return "1";
            }

            if (TimeDefInt() == TimeInt())
            {
                savedMonster1MaxHP = Monster1HPInt();
            }

            if (LargeMonster1ID() > 0 && savedMonster1ID == 0)
            {
                savedMonster1MaxHP = Monster1HPInt();
                savedMonster1ID = LargeMonster1ID();
            }

            if (savedMonster1ID > 0)
            {
                savedMonster1ID = LargeMonster1ID();
            }

            if (GetNotRoad() || RoadSelectedMonster() == 0)
            {
                return savedMonster1MaxHP.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return Monster2MaxHP;
            }
        }
    }

    public string Monster2HP => Configuring ?
        "0" :
        ShowMonsterEHP() ?
            DisplayMonsterEHP(Monster2DefMult(), Monster2HPInt()).ToString(CultureInfo.InvariantCulture) :
            Monster2HPInt().ToString(CultureInfo.InvariantCulture);

    public string Monster2MaxHP
    {
        get
        {
            if (Configuring)
            {
                return "1";
            }

            if (TimeDefInt() == TimeInt())
            {
                savedMonster2MaxHP = Monster2HPInt();
            }

            if (RoadSelectedMonster() > 0 && savedMonster2ID == 0)
            {
                savedMonster2MaxHP = Monster2HPInt();
                savedMonster2ID = RoadSelectedMonster();
            }

            if (savedMonster2ID > 0)
            {
                savedMonster2ID = RoadSelectedMonster();
            }

            return savedMonster2MaxHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster3HP => Configuring ?
        "0" :
        ShowMonsterEHP() ?
            DisplayMonsterEHP(1, Monster3HPInt()).ToString(CultureInfo.InvariantCulture) :
            Monster3HPInt().ToString(CultureInfo.InvariantCulture);

    public string Monster3MaxHP
    {
        get
        {
            if (Configuring)
            {
                return "1";
            }

            if (TimeDefInt() == TimeInt())
            {
                savedMonster3MaxHP = Monster3HPInt();
            }

            return savedMonster3MaxHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string Monster4HP => Configuring ?
        "0" :
        ShowMonsterEHP() ?
            DisplayMonsterEHP(1, Monster4HPInt()).ToString(CultureInfo.InvariantCulture) :
            Monster4HPInt().ToString(CultureInfo.InvariantCulture);

    public string Monster4MaxHP
    {
        get
        {
            if (Configuring)
            {
                return "1";
            }

            if (TimeDefInt() == TimeInt())
            {
                savedMonster4MaxHP = Monster4HPInt();
            }

            return savedMonster4MaxHP.ToString(CultureInfo.InvariantCulture);
        }
    }

    /*  Multipliers
        Sword and Shield 單手劍 片手剣 1.4x
        Dual Swords 雙劍 双剣 1.4x
        Great Sword 大劍 大剣 4.8x
        Long Sword 太刀 太刀 4.8x
        Hammer 大錘 ハンマー 5.2x
        Hunting Horn 狩獵笛 狩猟笛 5.2x
        Lance 長槍 ランス 2.3x
        Gunlance 銃槍 ガンランス 2.3x
        Tonfa 穿龍棍 穿龍棍 1.8x
        Switch Axe F 斬擊斧Ｆ スラッシュアックスF 5.4x
        Magnet Spike 磁斬鎚 マグネットスパイク 5.4x
        Heavy Bowgun 重銃 ヘビィボウガン 1.2x
        Light Bowgun 輕弩 ライトボウガン 1.2x
        Bow 弓 弓 1.2x
        IDs
        0    Great Sword
        1    Heavy Bowgun
        2    Hammer
        3    Lance
        4    Sword and Shield
        5    Light Bowgun
        6    Dual Swords
        7    Long Sword
        8    Hunting Horn
        9    Gunlance
        10    Bow
        11    Tonfa
        12    Switch Axe F
        13    Magnet Spike
        14    Group
    */

    /// <summary>
    /// Gets the multiplier from weapon type.
    /// </summary>
    /// <param name="weaponType">Type of the weapon.</param>
    /// <returns></returns>
    public static float GetMultFromWeaponType(int weaponType)
    {
        return weaponType switch
        {
            0 or 7 => 4.8f,
            4 or 6 => 1.4f,
            2 or 8 => 5.2f,
            12 or 13 => 5.4f,
            3 or 9 => 2.3f,
            1 or 5 or 10 => 1.2f,
            11 => 1.8f,
            _ => 1f,
        };
    }

    // TODO: EZlion
    /// <summary>
    /// Gets the name of the weapon type.
    /// </summary>
    /// <param name="weaponType">Type of the weapon.</param>
    /// <returns></returns>
    public static string GetWeaponNameFromType(int weaponType)
    {
        return weaponType switch
        {
            0 => "Great Sword",
            1 => "Heavy Bowgun",
            2 => "Hammer",
            3 => "Lance",
            4 => "Sword and Shield",
            5 => "Light Bowgun",
            6 => "Dual Swords",
            7 => "Long Sword",
            8 => "Hunting Horn",
            9 => "Gunlance",
            10 => "Bow",
            11 => "Tonfa",
            12 => "Switch Axe F",
            13 => "Magnet Spike",
            14 => "Group",
            _ => string.Empty,
        };
    }



    /// <summary>
    /// Gets the current monster1 icon.
    /// </summary>
    /// <value>
    /// The current monster1 icon.
    /// </value>
    public string CurrentMonster1Icon
    {
        get
        {
            int id;

            if (roadOverride() == false)
            {
                id = RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID();
            }
            else if (CaravanOverride())
            {
                id = CaravanMonster1ID();
            }
            else
            {
                id = LargeMonster1ID();
            }

            // dure
            if (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750)
            {
                id = 132;
            }
            else if (QuestID() == 23648 || QuestID() == 23649)
            {
                id = 167;
            }

            return DetermineMonsterImage(id);
        }
    }

    public string CurrentMonster1Image
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            var monsterIcon = CurrentMonster1Icon;

            if (s.EnableMonsterRenders)
            {
                var renderFound = MonsterRenderCollection.MonsterRender.ContainsKey(monsterIcon);
                if (renderFound)
                {
                    return MonsterRenderCollection.MonsterRender[monsterIcon];
                }
                else
                {
                    return Messages.UnknownMonsterIcon;
                }
            }
            else
            {
                return monsterIcon;
            }
        }
    }

    public string CurrentMonster2Image
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            var monsterIcon = CurrentMonster2Icon;

            if (s.EnableMonsterRenders)
            {
                var renderFound = MonsterRenderCollection.MonsterRender.ContainsKey(monsterIcon);
                if (renderFound)
                {
                    return MonsterRenderCollection.MonsterRender[monsterIcon];
                }
                else
                {
                    return Messages.UnknownMonsterIcon;
                }
            }
            else
            {
                return monsterIcon;
            }
        }
    }

    public string CurrentMonster3Image
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            var monsterIcon = CurrentMonster3Icon;

            if (s.EnableMonsterRenders)
            {
                var renderFound = MonsterRenderCollection.MonsterRender.ContainsKey(monsterIcon);
                if (renderFound)
                {
                    return MonsterRenderCollection.MonsterRender[monsterIcon];
                }
                else
                {
                    return Messages.UnknownMonsterIcon;
                }
            }
            else
            {
                return monsterIcon;
            }
        }
    }

    public string CurrentMonster4Image
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            var monsterIcon = CurrentMonster4Icon;

            if (s.EnableMonsterRenders)
            {
                var renderFound = MonsterRenderCollection.MonsterRender.ContainsKey(monsterIcon);
                if (renderFound)
                {
                    return MonsterRenderCollection.MonsterRender[monsterIcon];
                }
                else
                {
                    return Messages.UnknownMonsterIcon;
                }
            }
            else
            {
                return monsterIcon;
            }
        }
    }

    public string CurrentMonster2Icon
    {
        get
        {
            int id;

            if (roadOverride() == false)
            {
                id = RoadSelectedMonster() == 0 ? LargeMonster2ID() : LargeMonster1ID();
            }
            else if (CaravanOverride())
            {
                id = CaravanMonster2ID();
            }
            else
            {
                id = LargeMonster2ID();
            }

            //dure
            if (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750)
            {
                id = 132;
            }
            else if (QuestID() == 23648 || QuestID() == 23649)
            {
                id = 167;
            }

            return DetermineMonsterImage(id);
        }
    }

    public string CurrentMonster3Icon
    {
        get
        {
            var id = LargeMonster3ID();

            //dure
            if (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750)
            {
                id = 132;
            }
            else if (QuestID() == 23648 || QuestID() == 23649)
            {
                id = 167;
            }

            return DetermineMonsterImage(id);
        }
    }

    public string CurrentMonster4Icon
    {
        get
        {
            var id = LargeMonster4ID();

            // dure
            if (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750)
            {
                id = 132;
            }
            else if (QuestID() == 23648 || QuestID() == 23649)
            {
                id = 167;
            }

            return DetermineMonsterImage(id);
        }
    }

    public string DetermineMonsterImage(int id)
    {
        var keyFound = MonsterImageCollection.MonsterImageID.TryGetValue(id, out var link);
        if (link == null)
        {
            link = Messages.UnknownMonsterIcon;
        }

        if (keyFound)
        {
            return link;
        }
        else
        {
            return GetAlternateMonsterImage(id);
        }
    }

    public string DetermineMonsterHPBarColor(int n)
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        var barColorSetting = "#1e1e2e";
        var monsterID = 0;

        switch (n)
        {
            case 1:
                barColorSetting = s.Monster1BarColor;
                monsterID = LargeMonster1ID();
                break;
            case 2:
                barColorSetting = s.Monster2BarColor;
                monsterID = LargeMonster2ID();
                break;
            case 3:
                barColorSetting = s.Monster3BarColor;
                monsterID = LargeMonster3ID();
                break;
            case 4:
                barColorSetting = s.Monster4BarColor;
                monsterID = LargeMonster4ID();
                break;
        }

        var keyFound = MonsterColorCollection.MonsterColorID.TryGetValue(monsterID, out var color);
        if (color == null)
        {
            color = barColorSetting;
        }

        if (keyFound && s.EnableMonsterHPBarsAutomaticColor)
        {
            return color;
        }
        else
        {
            return barColorSetting;
        }
    }

    public string DetermineMonsterStrokeColor(int n)
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        var barColor = DetermineMonsterHPBarColor(n);
        var strokeColorSetting = string.Empty;
        var color = string.Empty;

        switch (barColor)
        {
            case "#313244":// Surface0
            case "#45475a":// Surface1
            case "#585b70":// Surface2
            case "#1e1e2e":// Base
            case "#181825":// Mantle
            case "#11111b":// Crust
                color = "#f5e0dc"; // Rosewater
                break;
            default:
                color = "#1e1e2e"; // Base
                break;
        }

        switch (n)
        {
            case 1:
                strokeColorSetting = s.Monster1BarStrokeColor;
                break;
            case 2:
                strokeColorSetting = s.Monster2BarStrokeColor;
                break;
            case 3:
                strokeColorSetting = s.Monster3BarStrokeColor;
                break;
            case 4:
                strokeColorSetting = s.Monster4BarStrokeColor;
                break;
        }

        if (s.EnableMonsterHPBarsAutomaticColor)
        {
            return color;
        }
        else
        {
            return strokeColorSetting;
        }
    }

    public string DetermineMonsterBorderColor(int n)
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        var barColor = DetermineMonsterHPBarColor(n);
        var colorSetting = string.Empty;
        var color = string.Empty;

        switch (barColor)
        {
            case "#313244":// Surface0
            case "#45475a":// Surface1
            case "#585b70":// Surface2
            case "#1e1e2e":// Base
            case "#181825":// Mantle
            case "#11111b":// Crust
                color = "#f5e0dc"; // Rosewater
                break;
            default:
                color = "#11111b"; // Crust
                break;
        }

        switch (n)
        {
            case 1:
                colorSetting = s.Monster1BarBorderColor;
                break;
            case 2:
                colorSetting = s.Monster2BarBorderColor;
                break;
            case 3:
                colorSetting = s.Monster3BarBorderColor;
                break;
            case 4:
                colorSetting = s.Monster4BarBorderColor;
                break;
        }

        if (s.EnableMonsterHPBarsAutomaticColor)
        {
            return color;
        }
        else
        {
            return colorSetting;
        }
    }

    public string DetermineMonsterHPModeText()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        if (s.EnableEHPNumbers)
        {
            return "EHP";
        }
        else
        {
            return "THP";
        }
    }

    public string Monster1HPBarColor => DetermineMonsterHPBarColor(1);

    public string Monster2HPBarColor => DetermineMonsterHPBarColor(2);

    public string Monster3HPBarColor => DetermineMonsterHPBarColor(3);

    public string Monster4HPBarColor => DetermineMonsterHPBarColor(4);

    public string Monster1StrokeColor => DetermineMonsterStrokeColor(1);

    public string Monster2StrokeColor => DetermineMonsterStrokeColor(2);

    public string Monster3StrokeColor => DetermineMonsterStrokeColor(3);

    public string Monster4StrokeColor => DetermineMonsterStrokeColor(4);

    public string Monster1BarBorderColor => DetermineMonsterBorderColor(1);

    public string Monster2BarBorderColor => DetermineMonsterBorderColor(2);

    public string Monster3BarBorderColor => DetermineMonsterBorderColor(3);

    public string Monster4BarBorderColor => DetermineMonsterBorderColor(4);

    public string MonsterHPModeText => DetermineMonsterHPModeText();

    public string CurrentMap
    {
        get
        {
            var id = AreaID();
            return GetGatheringMapFromID(id);
        }
    }

    private static string GetGatheringMapFromID(int id) // TODO: are highlands, tidal island or painted falls icons correct?
    {
        if (id >= 470 && id < 0)
        {
            return Messages.EmptyImage;
        }
        else
        {
            return FindGatheringMap(id);
        }
    }

    private static string FindGatheringMap(int id)
    {
        var AreaGroup = new List<int> { 0 };

        foreach (var kvp in GatheringMapCollection.GatheringMapID)
        {
            var areaIDs = kvp.Key;

            if (areaIDs.Contains(id))
            {
                AreaGroup = kvp.Key;
                break;
            }
        }

        return DetermineGatheringMap(AreaGroup);
    }

    private static string DetermineGatheringMap(List<int> key)
    {
        var gatheringMap = GatheringMapCollection.GatheringMapID.ContainsKey(key);
        if (!gatheringMap)
        {
            return Messages.EmptyImage;
        }
        else
        {
            return GatheringMapCollection.GatheringMapID[key];
        }
    }

    /// <summary>
    /// Gets the weapon class
    /// </summary>
    public string GetWeaponClass(int? weaponClass = null)
    {
        if (weaponClass == null)
        {
            if (CurrentWeaponName == "Light Bowgun" || CurrentWeaponName == "Heavy Bowgun" || CurrentWeaponName == "Bow")
            {
                return "Gunner";
            }
            else
            {
                return "Blademaster";
            }
        }
        else
        {
            if (weaponClass == 1 || weaponClass == 5 || weaponClass == 10)
            {
                return "Gunner";
            }
            else
            {
                return "Blademaster";
            }
        }
    }

    /// <summary>
    /// Gets the text format
    /// </summary>
    public static string GetTextFormat()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.TextFormatExport ?? "None";
    }

    /// <summary>
    /// Gets the gender.
    /// </summary>
    /// <value>
    /// The gender.
    /// </value>
    public static string GetGender()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.GenderExport ?? "Male";
    }



    /// <summary>
    /// Gets the name of the weapon.
    /// </summary>
    /// <value>
    /// The name of the weapon.
    /// </value>
    public string GetRealWeaponName
    {
        get
        {
            var className = GetWeaponClass();
            var lv = GetWeaponNameFromType(WeaponType()).Contains("Bowgun") ? GetGRWeaponLevel(GRWeaponLvBowguns()) : GetGRWeaponLevel(GRWeaponLv());

            if (GetTextFormat() == "Markdown" && (lv == " Lv. 50" || lv == " Lv. 100"))
            {
                lv = string.Format(CultureInfo.InvariantCulture, "**{0}**", lv);
            }

            var style = WeaponStyle() switch
            {
                0 => "Earth Style",
                1 => "Heaven Style",
                2 => "Storm Style",
                3 => "Extreme Style",
                _ => "Earth Style"
            };

            if (className == "Blademaster")
            {
                WeaponBlademaster.IDName.TryGetValue(BlademasterWeaponID(), out var wepname);
                var address = BlademasterWeaponID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();  // gives you hex 4 digit "007B"

                return string.Format(CultureInfo.InvariantCulture, "{0}{1} ({2}) | {3}\n{4} | {5} | {6}", wepname, lv, address, style, GetDecoName(WeaponDeco1ID(), 1), GetDecoName(WeaponDeco2ID(), 2), GetDecoName(WeaponDeco3ID(), 3));

            }
            else if (className == "Gunner")
            {
                WeaponGunner.IDName.TryGetValue(GunnerWeaponID(), out var wepname);
                var address = GunnerWeaponID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();  // gives you hex 4 digit "007B"
                return string.Format(CultureInfo.InvariantCulture, "{0}{1} ({2}) | {3}\n{4} | {5} | {6}", wepname, lv, address, style, GetDecoName(WeaponDeco1ID(), 1), GetDecoName(WeaponDeco2ID(), 2), GetDecoName(WeaponDeco3ID(), 3));
            }
            else
            {
                return "None";
            }
        }
    }

    public string GetRealWeaponNameForRunID(string className, string weaponName, long styleID, long weaponID, string weaponSlot1, string weaponSlot2, string weaponSlot3)
    {
        var style = styleID switch
        {
            0 => "Earth Style",
            1 => "Heaven Style",
            2 => "Storm Style",
            3 => "Extreme Style",
            _ => "Earth Style"
        };

        if (className == "Blademaster")
        {
            WeaponBlademaster.IDName.TryGetValue((int)weaponID, out var wepname);
            var address = weaponID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();  // gives you hex 4 digit "007B"

            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2}\n{3} | {4} | {5}", wepname, address, style, weaponSlot1, weaponSlot2, weaponSlot3);
        }
        else if (className == "Gunner")
        {
            WeaponGunner.IDName.TryGetValue((int)weaponID, out var wepname);
            var address = weaponID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();  // gives you hex 4 digit "007B"
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2}\n{3} | {4} | {5}", wepname, address, style, weaponSlot1, weaponSlot2, weaponSlot3);
        }
        else
        {
            return "None";
        }
    }


    public string GetAmmoPouch
    {
        get
        {
            var Item1 = AmmoPouchItem1ID();
            var Item2 = AmmoPouchItem2ID();
            var Item3 = AmmoPouchItem3ID();
            var Item4 = AmmoPouchItem4ID();
            var Item5 = AmmoPouchItem5ID();
            var Item6 = AmmoPouchItem6ID();
            var Item7 = AmmoPouchItem7ID();
            var Item8 = AmmoPouchItem8ID();
            var Item9 = AmmoPouchItem9ID();
            var Item10 = AmmoPouchItem10ID();

            Item.IDName.TryGetValue(Item1, out var ItemName1);
            Item.IDName.TryGetValue(Item2, out var ItemName2);
            Item.IDName.TryGetValue(Item3, out var ItemName3);
            Item.IDName.TryGetValue(Item4, out var ItemName4);
            Item.IDName.TryGetValue(Item5, out var ItemName5);
            Item.IDName.TryGetValue(Item6, out var ItemName6);
            Item.IDName.TryGetValue(Item7, out var ItemName7);
            Item.IDName.TryGetValue(Item8, out var ItemName8);
            Item.IDName.TryGetValue(Item9, out var ItemName9);
            Item.IDName.TryGetValue(Item10, out var ItemName10);

            //. also the values have to be skipped if item slot is empty
            if (ItemName1 == null || ItemName1 == "None" || ItemName1 == string.Empty || AmmoPouchItem1Qty() == 0)
            {
                ItemName1 = "Empty, ";
            }
            else if (ItemName2 == null || ItemName2 == "None" || ItemName2 == string.Empty || AmmoPouchItem2Qty() == 0)
            {
                ItemName1 += ", ";
            }
            else
            {
                ItemName1 += ", ";
            }

            if (ItemName2 == null || ItemName2 == "None" || ItemName2 == string.Empty || AmmoPouchItem2Qty() == 0)
            {
                ItemName2 = "Empty, ";
            }
            else if (ItemName3 == null || ItemName3 == "None" || ItemName3 == string.Empty || AmmoPouchItem3Qty() == 0)
            {
                ItemName2 += ", ";
            }
            else
            {
                ItemName2 += ", ";
            }

            if (ItemName3 == null || ItemName3 == "None" || ItemName3 == string.Empty || AmmoPouchItem3Qty() == 0)
            {
                ItemName3 = "Empty, ";
            }
            else if (ItemName4 == null || ItemName4 == "None" || ItemName4 == string.Empty || AmmoPouchItem4Qty() == 0)
            {
                ItemName3 += ", ";
            }
            else
            {
                ItemName3 += ", ";
            }

            if (ItemName4 == null || ItemName4 == "None" || ItemName4 == string.Empty || AmmoPouchItem4Qty() == 0)
            {
                ItemName4 = "Empty, ";
            }
            else if (ItemName5 == null || ItemName5 == "None" || ItemName5 == string.Empty || AmmoPouchItem5Qty() == 0)
            {
                ItemName4 += ", ";
            }
            else
            {
                ItemName4 += ", ";
            }

            if (ItemName5 == null || ItemName5 == "None" || ItemName5 == string.Empty || AmmoPouchItem5Qty() == 0)
            {
                ItemName5 = "Empty, \n";
            }
            else if (ItemName6 == null || ItemName6 == "None" || ItemName6 == string.Empty || AmmoPouchItem6Qty() == 0)
            {
                ItemName5 += string.Empty;
            }
            else
            {
                ItemName5 += "\n";
            }

            if (ItemName6 == null || ItemName6 == "None" || ItemName6 == string.Empty || AmmoPouchItem6Qty() == 0)
            {
                ItemName6 = "Empty, ";
            }
            else if (ItemName7 == null || ItemName7 == "None" || ItemName7 == string.Empty || AmmoPouchItem7Qty() == 0)
            {
                ItemName6 += ", ";
            }
            else
            {
                ItemName6 += ", ";
            }

            if (ItemName7 == null || ItemName7 == "None" || ItemName7 == string.Empty || AmmoPouchItem7Qty() == 0)
            {
                ItemName7 = "Empty, ";
            }
            else if (ItemName8 == null || ItemName8 == "None" || ItemName8 == string.Empty || AmmoPouchItem8Qty() == 0)
            {
                ItemName7 += ", ";
            }
            else
            {
                ItemName7 += ", ";
            }

            if (ItemName8 == null || ItemName8 == "None" || ItemName8 == string.Empty || AmmoPouchItem8Qty() == 0)
            {
                ItemName8 = "Empty, ";
            }
            else if (ItemName9 == null || ItemName9 == "None" || ItemName9 == string.Empty || AmmoPouchItem9Qty() == 0)
            {
                ItemName8 += ", ";
            }
            else
            {
                ItemName8 += ", ";
            }

            if (ItemName9 == null || ItemName9 == "None" || ItemName9 == string.Empty || AmmoPouchItem9Qty() == 0)
            {
                ItemName9 = "Empty, ";
            }
            else if (ItemName10 == null || ItemName10 == "None" || ItemName10 == string.Empty || AmmoPouchItem10Qty() == 0)
            {
                ItemName9 += ", ";
            }
            else
            {
                ItemName9 += ", ";
            }

            if (ItemName10 == null || ItemName10 == "None" || ItemName10 == string.Empty || AmmoPouchItem10Qty() == 0)
            {
                ItemName10 = "Empty";
            }
            else
            {
                ItemName10 += string.Empty;
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", ItemName1, ItemName2, ItemName3, ItemName4, ItemName5, ItemName6, ItemName7, ItemName8, ItemName9, ItemName10);
        }
    }

    public string GetItemPouch
    {
        get
        {
            var itemIDs = new int[20]
            {
        PouchItem1ID(),
        PouchItem2ID(),
        PouchItem3ID(),
        PouchItem4ID(),
        PouchItem5ID(),
        PouchItem6ID(),
        PouchItem7ID(),
        PouchItem8ID(),
        PouchItem9ID(),
        PouchItem10ID(),
        PouchItem11ID(),
        PouchItem12ID(),
        PouchItem13ID(),
        PouchItem14ID(),
        PouchItem15ID(),
        PouchItem16ID(),
        PouchItem17ID(),
        PouchItem18ID(),
        PouchItem19ID(),
        PouchItem20ID()
            };

            var itemNames = new string[20];
            for (var i = 0; i < 20; i++)
            {
                if (Item.IDName.TryGetValue(itemIDs[i], out var itemName))
                {
                    if (GetTextFormat() == "Markdown" && IsMetaItem(itemIDs[i]) && itemName != null && itemName != "None" && itemName != string.Empty)
                    {
                        itemName = string.Format(CultureInfo.InvariantCulture, "**{0}**", itemName);
                    }
                }
                else
                {
                    itemName = "None";
                }

                itemNames[i] = itemName == null ? "None" : itemName;
            }

            var itemPouch = string.Empty;
            for (var i = 0; i < 20; i++)
            {
                itemPouch += itemNames[i];
                if ((i + 1) % 5 == 0)
                {
                    itemPouch += "\n";
                }
                else if (i != 19)
                {
                    itemPouch += ", ";
                }
            }

            return itemPouch;
        }
    }

    public string GetItemsForRunID(int[] items)
    {
        var sb = new StringBuilder();
        var counter = 0;
        for (var i = 0; i < items.Length; i++)
        {
            var id = items[i];
            if (Item.IDName.TryGetValue(id, out var value))
            {
                if (value != "None" && value != string.Empty)
                {
                    sb.Append(value);
                    counter++;
                    if (counter % 5 == 0)
                    {
                        sb.Append('\n');
                    }
                    else if (i != items.Length - 1)
                    {
                        sb.Append(", ");
                    }
                }
            }
        }

        if (sb.Length == 0)
        {
            return "None";
        }

        return sb.ToString();
    }

    // TODO: Levels
    public string GetRoadDureSkillsForRunID(int[] skills)
    {
        var name = string.Empty;
        for (var i = 0; i < skills.Length; i++)
        {
            var id = skills[i];
            if (SkillRoadTower.IDName.TryGetValue(id, out var value) && value != "None" && value != string.Empty)
            {
                name += value;
                if (i != skills.Length - 1)
                {
                    name += ", ";
                }

                if (i % 5 == 4)
                {
                    name += "\n";
                }
            }
        }

        return string.IsNullOrEmpty(name) ? "None" : name;
    }

    /// <summary>
    /// Gets the name of the head piece.
    /// </summary>
    /// <value>
    /// The name of the head piece.
    /// </value>
    public string GetArmorHeadName
    {
        get
        {
            ArmorHead.IDName.TryGetValue(ArmorHeadID(), out var piecename);

            if (GetTextFormat() == "Markdown" && piecename != null && IsMetaGear(piecename))
            {
                piecename = string.Format(CultureInfo.InvariantCulture, "**{0}**", piecename);
            }

            var address = ArmorHeadID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(ArmorHeadDeco1ID()), GetDecoName(ArmorHeadDeco2ID()), GetDecoName(ArmorHeadDeco3ID()));
        }
    }

    public string GetArmorHeadNameForRunID(int armorHeadID, int headSlot1ID, int headslot2ID, int headSlot3ID)
    {
        ArmorHead.IDName.TryGetValue(armorHeadID, out var piecename);

        var address = armorHeadID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(headSlot1ID), GetDecoName(headslot2ID), GetDecoName(headSlot3ID));
    }

    /// <summary>
    /// Gets the name of the chest piece.
    /// </summary>
    /// <value>
    /// The name of the chest piece.
    /// </value>
    public string GetArmorChestName
    {
        get
        {
            ArmorChest.IDName.TryGetValue(ArmorChestID(), out var piecename);

            if (GetTextFormat() == "Markdown" && piecename != null && IsMetaGear(piecename))
            {
                piecename = string.Format(CultureInfo.InvariantCulture, "**{0}**", piecename);
            }

            var address = ArmorChestID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(ArmorChestDeco1ID()), GetDecoName(ArmorChestDeco2ID()), GetDecoName(ArmorChestDeco3ID()));
        }
    }

    public string GetArmorChestNameForRunID(int armorID, int slot1ID, int slot2ID, int slot3ID)
    {
        ArmorChest.IDName.TryGetValue(armorID, out var piecename);

        var address = armorID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(slot1ID), GetDecoName(slot2ID), GetDecoName(slot3ID));
    }

    /// <summary>
    /// Gets the name of the arms piece.
    /// </summary>
    /// <value>
    /// The name of the arms piece.
    /// </value>
    public string GetArmorArmName
    {
        get
        {
            ArmorArms.IDName.TryGetValue(ArmorArmsID(), out var piecename);

            if (GetTextFormat() == "Markdown" && piecename != null && IsMetaGear(piecename))
            {
                piecename = string.Format(CultureInfo.InvariantCulture, "**{0}**", piecename);
            }

            var address = ArmorArmsID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(ArmorArmsDeco1ID()), GetDecoName(ArmorArmsDeco2ID()), GetDecoName(ArmorArmsDeco3ID()));
        }
    }

    public string GetArmorArmNameForRunID(int armorID, int slot1ID, int slot2ID, int slot3ID)
    {
        ArmorArms.IDName.TryGetValue(armorID, out var piecename);

        var address = armorID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(slot1ID), GetDecoName(slot2ID), GetDecoName(slot3ID));
    }

    /// <summary>
    /// Gets the name of the waist piece.
    /// </summary>
    /// <value>
    /// The name of the waist piece.
    /// </value>
    public string GetArmorWaistName
    {
        get
        {
            ArmorWaist.IDName.TryGetValue(ArmorWaistID(), out var piecename);

            if (GetTextFormat() == "Markdown" && piecename != null && IsMetaGear(piecename))
            {
                piecename = string.Format(CultureInfo.InvariantCulture, "**{0}**", piecename);
            }

            var address = ArmorWaistID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(ArmorWaistDeco1ID()), GetDecoName(ArmorWaistDeco2ID()), GetDecoName(ArmorWaistDeco3ID()));
        }
    }

    public string GetArmorWaistNameForRunID(int armorID, int slot1ID, int slot2ID, int slot3ID)
    {
        ArmorWaist.IDName.TryGetValue(armorID, out var piecename);

        var address = armorID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(slot1ID), GetDecoName(slot2ID), GetDecoName(slot3ID));
    }

    /// <summary>
    /// Gets the name of the head piece.
    /// </summary>
    /// <value>
    /// The name of the head piece.
    /// </value>
    public string GetArmorLegName
    {
        get
        {
            ArmorLegs.IDName.TryGetValue(ArmorLegsID(), out var piecename);

            if (GetTextFormat() == "Markdown" && piecename != null && IsMetaGear(piecename))
            {
                piecename = string.Format(CultureInfo.InvariantCulture, "**{0}**", piecename);
            }

            var address = ArmorLegsID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(ArmorLegsDeco1ID()), GetDecoName(ArmorLegsDeco2ID()), GetDecoName(ArmorLegsDeco3ID()));
        }
    }

    public string GetArmorLegNameForRunID(int armorID, int slot1ID, int slot2ID, int slot3ID)
    {
        ArmorLegs.IDName.TryGetValue(armorID, out var piecename);

        var address = armorID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1}) | {2} | {3} | {4}", piecename, address, GetDecoName(slot1ID), GetDecoName(slot2ID), GetDecoName(slot3ID));
    }

    /// <summary>
    /// Gets the decos.
    /// </summary>
    /// <value>
    /// The decos.
    /// </value>
    public string GetDecoName(int id, int slot = 0, bool isForImage = false)
    {
        var DecoName = string.Empty;

        var keyFound = Item.IDName.TryGetValue(id, out DecoName);

        if (GetTextFormat() == "Markdown" && IsMetaItem(id) && DecoName != null && DecoName == "None" && DecoName != string.Empty && keyFound)
        {
            DecoName = string.Format(CultureInfo.InvariantCulture, "**{0}**", DecoName);
        }

        if (DecoName == null || DecoName == "None" || DecoName == string.Empty)
        {
            DecoName = "Empty";
        }
        else
        {
            DecoName += string.Empty;
        }

        if (DecoName == "Empty" && slot != 0)
        {
            return GetSigilName(slot);
        }

        string address;
        if (!isForImage)
        {
            address = " (" + id.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant() + ")";
        }
        else
        {
            address = string.Empty;
        }

        return string.Format(CultureInfo.InvariantCulture, "{0}{1}", DecoName, address);
    }

    /// <summary>
    /// Gets the sigils.
    /// </summary>
    /// <value>
    /// The sigils.
    /// </value>
    public string GetSigilName(int slot)
    {
        var sigilSkillList = (Dictionary<int, string>)SkillSigil.IDName;
        var sigilNames = new int[] { Sigil1Name1(), Sigil1Name2(), Sigil1Name3(), Sigil2Name1(), Sigil2Name2(), Sigil2Name3(), Sigil3Name1(), Sigil3Name2(), Sigil3Name3() };
        var sigilValues = new int[] { Sigil1Value1(), Sigil1Value2(), Sigil1Value3(), Sigil2Value1(), Sigil2Value2(), Sigil2Value3(), Sigil3Value1(), Sigil3Value2(), Sigil3Value3() };
        var sigilTypes = new string[9];
        for (var i = 0; i < 9; i++)
        {
            sigilSkillList.TryGetValue(sigilNames[i], out var type);
            sigilTypes[i] = type ?? string.Empty;
        }

        var index = (slot - 1) * 3;
        if (sigilValues[index] == 0 || sigilNames[index] == 0)
        {
            return "Empty";
        }

        var type1 = sigilTypes[index] + ": ";
        var value1 = (sigilValues[index] > 127 ? sigilValues[index] - 256 : sigilValues[index]).ToString();
        var type2 = "Empty, ";
        var value2 = string.Empty;
        var type3 = "Empty";
        var value3 = string.Empty;

        if (sigilValues[index + 1] != 0 && sigilNames[index + 1] != 0)
        {
            type2 = sigilTypes[index + 1] + ": ";
            value2 = (sigilValues[index + 1] > 127 ? sigilValues[index + 1] - 256 : sigilValues[index + 1]).ToString();
        }

        if (sigilValues[index + 2] != 0 && sigilNames[index + 2] != 0)
        {
            type3 = sigilTypes[index + 2] + ": ";
            value3 = (sigilValues[index + 2] > 127 ? sigilValues[index + 2] - 256 : sigilValues[index + 2]).ToString();
        }

        return type1 + (value1 != string.Empty && !value1.Contains('-') ? "+" : string.Empty) + value1 + ", " + type2 + (value2 != string.Empty && !value2.Contains('-') ? "+" : string.Empty) + value2 + ", " + type3 + (value3 != string.Empty && !value3.Contains('-') ? "+" : string.Empty) + value3;
    }


    /// <summary>
    /// Gets the name of the first cuff.
    /// </summary>
    /// <value>
    /// The name of the first cuff.
    /// </value>
    public string GetCuff1Name
    {
        get
        {
            Item.IDName.TryGetValue(Cuff1ID(), out var cuffname);
            var address = Cuff1ID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", cuffname, address);
        }
    }

    public string GetCuffName(int cuffID)
    {
        Item.IDName.TryGetValue(cuffID, out var cuffname);
        var address = cuffID.ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", cuffname, address);
    }

    /// <summary>
    /// Gets the name of the second cuff.
    /// </summary>
    /// <value>
    /// The name of the second cuff.
    /// </value>
    public string GetCuff2Name
    {
        get
        {
            Item.IDName.TryGetValue(Cuff2ID(), out var cuffname);
            var address = Cuff2ID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", cuffname, address);
        }
    }

    public string GetCuffs
    {
        get
        {
            var cuff1 = GetCuff1Name;
            var cuff2 = GetCuff2Name;

            if (IsMetaItem(Cuff1ID()))
            {
                cuff1 = string.Format(CultureInfo.InvariantCulture, "**{0}**", cuff1);
            }

            if (IsMetaItem(Cuff2ID()))
            {
                cuff2 = string.Format(CultureInfo.InvariantCulture, "**{0}**", cuff2);
            }

            return string.Format(CultureInfo.InvariantCulture, "{0} | {1}", cuff1, cuff2);
        }
    }

    public string GetCuffsForRunID(long cuff1ID, long cuff2ID)
    {
        var cuff1 = GetCuffName((int)cuff1ID);
        var cuff2 = GetCuffName((int)cuff2ID);

        return string.Format(CultureInfo.InvariantCulture, "{0} | {1}", cuff1, cuff2);
    }

    /// <summary>
    /// Determines whether [is maximum caravan skill] [the specified identifier].
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    ///   <c>true</c> if [is maximum caravan skill] [the specified identifier]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMaxCaravanSkill(int id)
    {

        switch (id)
        {
            default:
                return false;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 12:
            case 15:
            case 16:
            case 21:
            case 24:
            case 27:
            case 39:
            case 42:
            case 76:
            case 77:
            case 78:
            case 79:
            case 80:
            case 83:
            case 86:
            case 89:
            case 92:
            case 96:
            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
            case 105:
            case 106:
            case 107:
            case 108:
                return true;
        }
    }

    /// <summary>
    /// Gets the caravan skills.
    /// </summary>
    /// <returns></returns>
    public string GetCaravanSkills
    {
        get
        {
            var id1 = CaravanSkill1();
            var id2 = CaravanSkill2();
            var id3 = CaravanSkill3();

            SkillCaravan.IDName.TryGetValue(id1, out var caravanSkillName1);
            SkillCaravan.IDName.TryGetValue(id2, out var caravanSkillName2);
            SkillCaravan.IDName.TryGetValue(id3, out var caravanSkillName3);

            if (GetTextFormat() == "Markdown")
            {
                if (IsMaxCaravanSkill(id1))
                {
                    caravanSkillName1 = string.Format(CultureInfo.InvariantCulture, "**{0}**", caravanSkillName1);
                }

                if (IsMaxCaravanSkill(id2))
                {
                    caravanSkillName2 = string.Format(CultureInfo.InvariantCulture, "**{0}**", caravanSkillName2);
                }

                if (IsMaxCaravanSkill(id3))
                {
                    caravanSkillName3 = string.Format(CultureInfo.InvariantCulture, "**{0}**", caravanSkillName3);
                }
            }

            if (caravanSkillName1 == string.Empty || caravanSkillName1 == "None")
            {
                return "None";
            }
            else if (caravanSkillName2 == string.Empty || caravanSkillName2 == "None")
            {
                return caravanSkillName1 + string.Empty;
            }
            else if (caravanSkillName3 == string.Empty || caravanSkillName3 == "None")
            {
                return caravanSkillName1 + ", " + caravanSkillName2;
            }
            else
            {
                return caravanSkillName1 + ", " + caravanSkillName2 + ", " + caravanSkillName3;
            }
        }
    }

    public string GetCaravanSkillsForRunID(int skill1, int skill2, int skill3)
    {
        var SkillName = string.Empty;
        var skills = new int[] { skill1, skill2, skill3 };
        for (var i = 0; i < skills.Length; i++)
        {
            var skillId = skills[i];
            if (SkillCaravan.IDName.TryGetValue(skillId, out var skillName)
                && skillName != "None" && skillName != string.Empty)
            {
                SkillName += skillName;
                if (i != skills.Length - 1)
                {
                    SkillName += ", ";
                }

                if (i % 5 == 4)
                {
                    SkillName += "\n";
                }
            }
        }

        return string.IsNullOrEmpty(SkillName) ? "None" : SkillName;
    }

    /// <summary>
    /// Determines whether [is maximum zenith skill].
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    ///   <c>true</c> if [is maximum zenith skill] [the specified identifier]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMaxZenithSkill(int id)
    {
        switch (id)
        {
            default:
                return false;
            case 7:
            case 9:
            case 11:
            case 12:
            case 14:
            case 15:
            case 19:
            case 23:
            case 25:
            case 27:
            case 29:
            case 31:
            case 32:
            case 34:
            case 35:
            case 37:
            case 39:
            case 41:
            case 43:
            case 46:
            case 47:
            case 48:
            case 49:
            case 50:
            case 51:
            case 52:
                return true;
        }
    }

    /// <summary>
    /// Determines whether [is maximum skill level].
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    ///   <c>true</c> if [is maximum skill level] [the specified identifier]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMaxSkillLevel(int id)
    {
        switch (id)
        {
            default:
                return false;
            case 2:
            case 5:
            case 8:
            case 11:
            case 13:
            case 16:
            case 19:
            //case 23:
            case 28:
            case 33:
            case 38:
            case 41:
            case 44:
            case 45:
            case 48:
            case 51:
            case 53:
            case 54:
            case 55:
            case 56:
            case 58:
            case 61:
            case 64:
            case 67:
            case 70:
            case 71:
            case 72:
            case 74:
            case 78:

            // case 81:
            // case 84:
            case 89:
            case 94:
            case 97:
            case 100:
            case 106:
            case 112:
            case 118:
            case 124:
            case 130:
            case 135:
            case 140:
            case 147:
            case 150:
            case 153:
            case 155:
            case 159:
            case 162:
            case 164:
            case 165:
            case 169:
            case 172:
            case 173:
            case 178:
            case 180:
            case 182:
            case 184:
            case 186:
            case 189:
            case 190:
            case 192:
            case 194:
            case 199:
            case 201:
            case 203:// Gunnery?
            case 212:
            case 218:

            // case 219:
            case 222:
            case 223:
            case 228:
            case 232:
            case 235:
            case 238:
            case 240:
            case 242:
            case 244:
            case 246:
            case 248:
            case 250:
            case 252:
            case 256:
            case 259:
            case 262:
            case 265:
            case 268:
            case 271:
            case 274:
            case 277:
            case 280:
            case 283:
            case 285:
            case 287:
            case 288:
            case 289:
            case 290:
            case 292:
            case 293:
            case 295:
            case 296:
            case 297:
            case 299:
            case 300:
            case 301:
            case 302:
            case 305:
            case 309:
            case 313:
            case 317:
            case 321:
            case 325:
            case 329:
            case 333:
            case 337:
            case 341:
            case 345:
            case 349:
            case 350:
            case 352:
            case 353:
            case 354:
            case 356:
            case 359:
            case 360:
            case 362:
            case 365:
            case 366:
            case 368:
            case 384:
            case 388:
            case 390:
            case 392:
            case 393:
            case 394:
            case 395:
            case 396:
            case 397:
            case 398:
            case 401:
            case 404:
            case 407:
            case 414:
            case 417:
            case 420:
            case 423:
            case 425:
            case 426:
            case 431:
            case 432:
            case 437:
            case 438:
            case 443:
            case 446:
            case 449:
            case 452:
            case 453:
            case 456:
            case 457:
            case 458:// Red Soul
            case 461:
            case 463:
            case 464:
            case 465:
            case 466:
            case 471:
            case 473:
            case 474:
            case 475:
            case 476:
            case 477:
            case 480:
            case 481:
            case 482:
            case 485:
            case 486:
            case 488:
            case 489:
            case 491:
            case 494:
            case 495:
            case 497:
            case 498:
            case 499:
            case 501:
            case 502:
            case 503:
            case 504:
            case 505:
            case 506:
            case 512:
            case 513:
            case 514:
            case 515:
            case 516:
            case 517:

            // case 518://dupe?
            case 519:
            case 520:
            case 521:
            case 522:
            case 523:
            case 524:
            case 525:
            case 526:
            case 528:
            case 529:
            case 530:
                return true;
        }
    }

    /// <summary>
    /// Determines whether [is maximum road dure skill level] [the specified identifier].
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="level">The level.</param>
    /// <returns>
    ///   <c>true</c> if [is maximum road dure skill level] [the specified identifier]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMaxRoadDureSkillLevel(int id, string level)
    {
        if (level == "0")
        {
            return false;
        }

        switch (id)
        {
            default:
                return false;
            case 1:
            case 2:
            case 19:
            case 20:
                return level == "5" ? true : false;
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 34:
            case 29:
            case 31:
            case 14:
            case 15:
            case 18:
            case 22:
                return level == "3" ? true : false;
            case 5:
            case 28:
            case 33:
            case 32:
                return level == "2" ? true : false;
            case 35:
            case 30:
                return level == "1" ? true : false;
        }
    }

    /// <summary>
    /// Gets the zenith skills.
    /// </summary>
    /// <value>
    /// The zenith skills.
    /// </value>
    public string GetZenithSkills
    {
        get
        {
            var skills = new string[7];
            for (var i = 0; i < 7; i++)
            {
                var skillNum = i + 1;
                var skillID = (int)this.GetType().GetMethod($"ZenithSkill{skillNum}").Invoke(this, null);
                if (SkillZenith.IDName.TryGetValue(skillID, out var skillName))
                {
                    if (IsMaxZenithSkill(skillID) && skillName != "None" && !string.IsNullOrEmpty(skillName))
                    {
                        skillName = "**" + skillName + "**";
                    }

                    skills[i] = skillName;
                }
            }

            var result = string.Join(", ", skills.Where(s => s != "None" && s != string.Empty).Take(5));
            for (var i = 5; i < 7; i++)
            {
                if (skills[i] != "None" && skills[i] != string.Empty)
                {
                    result += "\n" + skills[i];
                    if (i < 6 && skills[i + 1] != "None" && skills[i + 1] != string.Empty)
                    {
                        result += ", ";
                    }
                }
            }

            return result;
        }
    }


    public string GetZenithSkillsForRunID(int skill1, int skill2, int skill3, int skill4, int skill5, int skill6, int skill7)
    {
        var SkillName = string.Empty;
        var skills = new int[] { skill1, skill2, skill3, skill4, skill5, skill6, skill7 };
        for (var i = 0; i < skills.Length; i++)
        {
            var skillId = skills[i];
            if (SkillZenith.IDName.TryGetValue(skillId, out var skillName))
            {
                if (skillName != "None" && skillName != string.Empty)
                {
                    SkillName += skillName;
                    if (i != skills.Length - 1)
                    {
                        SkillName += ", ";
                    }

                    if (i % 5 == 4)
                    {
                        SkillName += "\n";
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(SkillName))
        {
            return "None";
        }

        return SkillName;
    }

    /// <summary>
    /// Gets the gou boost mode.
    /// </summary>
    /// <returns></returns>
    public static bool GetGouBoostMode()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.GouBoostExport;
    }

    /// <summary>
    /// Gets the armor skills.
    /// </summary>
    /// <value>
    /// The armor skills.
    /// </value>
    public string GetArmorSkills
    {
        get
        {
            var skills = new int[19];
            for (var i = 0; i < 19; i++)
            {
                skills[i] = GetGouBoostMode()
                    ? GetGouBoostSkill((int)this.GetType().GetMethod("ArmorSkill" + (i + 1)).Invoke(this, null))
                    : (int)this.GetType().GetMethod("ArmorSkill" + (i + 1)).Invoke(this, null);
            }

            var skillNames = new string[19];
            for (var i = 0; i < 19; i++)
            {
                SkillArmor.IDName.TryGetValue(skills[i], out var skillName);
                skillNames[i] = skillName ?? string.Empty;
            }

            var result = string.Empty;
            for (var i = 0; i < 19; i++)
            {
                if (GetTextFormat() == "Markdown" && IsMaxSkillLevel(skills[i]) && skillNames[i] != string.Empty && skillNames[i] != "None")
                {
                    skillNames[i] = string.Format(CultureInfo.InvariantCulture, "**{0}**", skillNames[i]);
                }

                result += skillNames[i] + (i == 18 ? string.Empty : i % 5 == 4 ? "\n" : ", ");
            }

            return result;
        }
    }


    public string GetArmorSkillsForRunID(int skill1, int skill2, int skill3, int skill4, int skill5, int skill6, int skill7, int skill8, int skill9, int skill10, int skill11, int skill12, int skill13, int skill14, int skill15, int skill16, int skill17, int skill18, int skill19)
    {
        var SkillName = string.Empty;
        var skills = new int[] { skill1, skill2, skill3, skill4, skill5, skill6, skill7, skill8, skill9, skill10, skill11, skill12, skill13, skill14, skill15, skill16, skill17, skill18, skill19 };
        for (var i = 0; i < skills.Length; i++)
        {
            var skillId = skills[i];
            if (SkillArmor.IDName.TryGetValue(skillId, out var skillName))
            {
                if (skillName != "None" && skillName != string.Empty)
                {
                    SkillName += skillName;
                    if (i != skills.Length - 1)
                    {
                        SkillName += ", ";
                    }

                    if (i % 5 == 4)
                    {
                        SkillName += "\n";
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(SkillName))
        {
            return "None";
        }

        return SkillName;
    }


    /// <summary>
    /// Gets the diva skill name from identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetDivaSkillNameFromID(int id)
    {
        SkillDiva.IDName.TryGetValue(id, out var divaskillaname);
        return divaskillaname + string.Empty;
    }

    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetItemName(int id)
    {
        Item.IDName.TryGetValue(id, out string? itemValue1);  //returns true

        return itemValue1 + string.Empty;
    }

    /// <summary>
    /// Gets the armor skill.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetArmorSkill(int id)
    {
        SkillArmor.IDName.TryGetValue(id, out var skillname);
        if (skillname == string.Empty || skillname == null)
        {
            return "None";
        }
        else
        {
            return skillname + string.Empty;
        }
    }

    /// <summary>
    /// Gets the armor skill.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetArmorSkillWithNull(int id)
    {
        SkillArmor.IDName.TryGetValue(id, out var skillname);
        if (skillname == string.Empty)
        {
            return "None";
        }
        else
        {
            return skillname + string.Empty;
        }
    }

    /// <summary>
    /// Gets the goushou boost skill
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static int GetGouBoostSkill(int id)
    {
        return id switch
        {
            1 => 2,
            4 => 5,
            7 => 8,
            10 => 11,
            21 => 22,
            22 => 23,
            23 => 291,
            27 => 28,
            31 => 293,
            35 => 36,
            36 => 37,
            37 => 284,
            38 => 398,
            40 => 41,
            46 => 47,
            47 => 48,
            52 => 53,
            53 => 482,
            73 => 74,
            79 => 80,
            80 => 81,
            81 => 286,
            82 => 83,
            83 => 84,
            84 => 294,
            88 => 89,
            92 => 93,
            93 => 288,
            95 => 96,
            96 => 384,
            104 => 105,
            105 => 106,
            110 => 111,
            111 => 112,
            116 => 117,
            117 => 118,
            122 => 123,
            123 => 124,
            128 => 129,
            129 => 130,
            134 => 135,
            139 => 140,
            144 => 145,
            145 => 146,
            146 => 349,
            149 => 150,
            154 => 155,
            158 => 159,
            163 => 164,
            167 => 168,
            168 => 169,
            177 => 178,
            184 => 289,
            187 => 290,
            193 => 194,
            197 => 350,
            198 => 199,
            200 => 201,
            203 => 298,
            219 => 359,
            220 => 360,
            230 => 231,
            231 => 232,
            234 => 300,
            235 => 301,
            241 => 242,
            243 => 244,
            245 => 246,
            247 => 248,
            249 => 250,
            254 => 255,
            255 => 256,
            257 => 258,
            258 => 259,
            260 => 261,
            261 => 262,
            263 => 264,
            264 => 265,
            266 => 267,
            267 => 268,
            269 => 270,
            270 => 271,
            272 => 273,
            273 => 274,
            275 => 276,
            276 => 277,
            278 => 279,
            279 => 280,
            281 => 282,
            282 => 283,
            298 => 299,
            303 => 302,// TODO: test
            351 => 352,
            357 => 356,
            361 => 362,
            369 => 370,
            370 => 491,
            477 => 476,
            _ => id,
        };
    }

    /// <summary>
    /// Gets the automatic skills.
    /// </summary>
    /// <value>
    /// The automatic skills.
    /// </value>
    public string GetAutomaticSkills
    {
        get
        {
            SkillArmor.IDName.TryGetValue(AutomaticSkillWeapon(), out var SkillName1);
            SkillArmor.IDName.TryGetValue(AutomaticSkillHead(), out var SkillName2);
            SkillArmor.IDName.TryGetValue(AutomaticSkillChest(), out var SkillName3);
            SkillArmor.IDName.TryGetValue(AutomaticSkillArms(), out var SkillName4);
            SkillArmor.IDName.TryGetValue(AutomaticSkillWaist(), out var SkillName5);
            SkillArmor.IDName.TryGetValue(AutomaticSkillLegs(), out var SkillName6);

            if (SkillName1 == null || SkillName1 == "None" || SkillName1 == string.Empty)
            {
                SkillName1 = string.Empty;
            }
            else if (SkillName2 == null || SkillName2 == "None" || SkillName2 == string.Empty)
            {
                SkillName1 += string.Empty;
            }
            else
            {
                SkillName1 += ", ";
            }

            if (SkillName2 == null || SkillName2 == "None" || SkillName2 == string.Empty)
            {
                SkillName2 = string.Empty;
            }
            else if (SkillName3 == null || SkillName3 == "None" || SkillName3 == string.Empty)
            {
                SkillName2 += string.Empty;
            }
            else
            {
                SkillName2 += ", ";
            }

            if (SkillName3 == null || SkillName3 == "None" || SkillName3 == string.Empty)
            {
                SkillName3 = string.Empty;
            }
            else if (SkillName4 == null || SkillName4 == "None" || SkillName4 == string.Empty)
            {
                SkillName3 += string.Empty;
            }
            else
            {
                SkillName3 += ", ";
            }

            if (SkillName4 == null || SkillName4 == "None" || SkillName4 == string.Empty)
            {
                SkillName4 = string.Empty;
            }
            else if (SkillName5 == null || SkillName5 == "None" || SkillName5 == string.Empty)
            {
                SkillName4 += string.Empty;
            }
            else
            {
                SkillName4 += ", ";
            }

            if (SkillName5 == null || SkillName5 == "None" || SkillName5 == string.Empty)
            {
                SkillName5 = string.Empty;
            }
            else if (SkillName6 == null || SkillName6 == "None" || SkillName6 == string.Empty)
            {
                SkillName5 += string.Empty;
            }
            else
            {
                SkillName5 += "\n";
            }

            if (SkillName6 == null || SkillName6 == "None" || SkillName6 == string.Empty)
            {
                SkillName6 = string.Empty;
            }
            else
            {
                SkillName6 += string.Empty;
            }

            if (SkillName1 == string.Empty)
            {
                SkillName1 = "None";
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}", SkillName1, SkillName2, SkillName3, SkillName4, SkillName5, SkillName6);
        }
    }

    public string GetAutomaticSkillsForRunID(int skill1, int skill2, int skill3, int skill4, int skill5, int skill6)
    {
        var SkillName = string.Empty;
        var skills = new int[] { skill1, skill2, skill3, skill4, skill5, skill6 };
        for (var i = 0; i < skills.Length; i++)
        {
            var skillId = skills[i];
            if (SkillArmor.IDName.TryGetValue(skillId, out var skillName))
            {
                if (skillName != "None" && skillName != string.Empty)
                {
                    SkillName += skillName;
                    if (i != skills.Length - 1)
                    {
                        SkillName += ", ";
                    }

                    if (i % 5 == 4)
                    {
                        SkillName += "\n";
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(SkillName))
        {
            return "None";
        }

        return SkillName;
    }

    /// <summary>
    /// Gets the total GSR weapon unlocks.
    /// </summary>
    /// <returns></returns>
    public static string GetTotalGSRWeaponUnlocks()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.GSRUnlocksExport ?? "11";
    }

    /// <summary>
    /// Is the gsr x11+ R999.
    /// </summary>
    /// <returns></returns>
    public static bool Is11GSR999()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.Enable11GSR999;
    }

    public string MarkdownSavedGearStats { get; set; } = string.Empty;

    public string SavedGearStats { get; set; } = string.Empty;

    /// <summary>
    /// Determines whether [is fixed GSR skill value] [the specified skill name].
    /// </summary>
    /// <param name="skillName">Name of the skill.</param>
    /// <returns>
    ///   <c>true</c> if [is fixed GSR skill value] [the specified skill name]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFixedGSRSkillValue(string skillName)
    {
        return skillName switch
        {
            "Passive Master" or "Soul Revival" or "Secret Tech" or "Max Sharpen" or "Sharpening Up" or "Affinity+26" or "Affinity+24" or "Affinity+22" or "Affinity+20" or "Nothing" => true,
            _ => false,
        };
    }

    /// <summary>
    /// Determines whether [is maximum GSR skill value] [the specified skill name].
    /// </summary>
    /// <param name="skillName">Name of the skill.</param>
    /// <returns>
    ///   <c>true</c> if [is maximum GSR skill value] [the specified skill name]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMaxGSRSkillValue(string skillName)
    {
        return skillName switch
        {
            "Passive Master" or "Soul Revival" or "Secret Tech" or "Max Sharpen" or "Affinity+26" or "Conquest Def+330" or "Conquest Atk+115" or "Def+180" or "Fire Res+35" or "Water Res+35" or "Thunder Res+35" or "Ice Res+35" or "Dragon Res+35" or "All Res+20" => true,
            _ => false,
        };
    }

    /// <summary>
    /// Gets the GSR skills.
    /// </summary>
    /// <value>
    /// The GSR skills.
    /// </value>
    public string GetGSRSkills
    {
        get
        {
            var Skill1 = StyleRank1();
            var Skill2 = StyleRank2();

            SkillStyleRank.IDName.TryGetValue(Skill1, out var SkillName1);
            SkillStyleRank.IDName.TryGetValue(Skill2, out var SkillName2);

            SkillName1 += string.Empty;
            SkillName2 += string.Empty;

            if (SkillName1 == string.Empty)
            {
                SkillName1 = "Nothing";
            }

            if (SkillName2 == string.Empty)
            {
                SkillName2 = "Nothing";
            }

            if (!IsFixedGSRSkillValue(SkillName1))
            {
                SkillName1 = GetGSRBonus(SkillName1);
            }

            if (!IsFixedGSRSkillValue(SkillName2))
            {
                SkillName2 = GetGSRBonus(SkillName2);
            }

            // pls
            if (GetTextFormat() == "Markdown")
            {
                if (IsMaxGSRSkillValue(SkillName1) && (SkillName1 != null || SkillName1 != "Nothing" || SkillName1 != string.Empty))
                {
                    SkillName1 = string.Format(CultureInfo.InvariantCulture, "**{0}**", SkillName1);
                }

                if (IsMaxGSRSkillValue(SkillName2) && (SkillName2 != null || SkillName2 != "Nothing" || SkillName2 != string.Empty))
                {
                    SkillName2 = string.Format(CultureInfo.InvariantCulture, "**{0}**", SkillName2);
                }
            }

            if (SkillName1 == null || SkillName1 == "Nothing" || SkillName1 == string.Empty)
            {
                SkillName1 = string.Empty;
            }
            else if (SkillName2 == null || SkillName2 == "Nothing" || SkillName2 == string.Empty)
            {
                SkillName1 += string.Empty;
            }
            else
            {
                SkillName1 += ", ";
            }

            if (SkillName2 == null || SkillName2 == "Nothing" || SkillName2 == string.Empty)
            {
                SkillName2 = string.Empty;
            }
            else
            {
                SkillName2 += string.Empty;
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", SkillName1, SkillName2);
        }
    }

    public string GetGSRSkillsForRunID(int skill1, int skill2)
    {
        var SkillName = string.Empty;
        var skills = new int[] { skill1, skill2 };
        for (var i = 0; i < skills.Length; i++)
        {
            var skillId = skills[i];
            if (SkillStyleRank.IDName.TryGetValue(skillId, out var skillName))
            {
                if (skillName != "None" && skillName != string.Empty)
                {
                    SkillName += skillName;
                    if (i != skills.Length - 1)
                    {
                        SkillName += ", ";
                    }

                    if (i % 5 == 4)
                    {
                        SkillName += "\n";
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(SkillName))
        {
            return "None";
        }

        return SkillName;
    }

    /// <summary>
    /// Gets the GSR bonus. Values from Ferias guy
    /// </summary>
    /// <param name="skillName">Name of the skill.</param>
    /// <returns></returns>
    public string GetGSRBonus(string skillName)
    {
        //question: does maxing all gsr give 1 more point of all res?
        //also does it increase before grank slowly for the other res?

        //defense here (needs testing)
        if (IsFixedGSRSkillValue(skillName))
        {
            return skillName;
        }

        var Def = 0;
        var FireRes = 0;
        var WaterRes = 0;
        var IceRes = 0;
        var ThunderRes = 0;
        var DragonRes = 0;
        var AllRes = 0;
        var ConquestAtk = 0;
        var ConquestDef = 0;

        var skillNameNumber = int.Parse(skillName.Substring(skillName.IndexOf("+", StringComparison.InvariantCulture)), CultureInfo.InvariantCulture);
        var skillNameType = skillName.Substring(0, skillName.IndexOf("+", StringComparison.InvariantCulture));

        if (GSR() >= 10)
        {
            Def += 1;
        }

        if (GSR() >= 20)
        {
            FireRes += 2;
        }

        if (GSR() >= 30)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 40)
        {
            WaterRes += 2;
        }

        if (GSR() >= 50)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 60)
        {
            Def += 1;
        }

        if (GSR() >= 70)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 80)
        {
            ThunderRes += 2;
        }

        if (GSR() >= 90)
        {
            Def += 1;
        }

        if (GSR() >= 100)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 110)
        {
            Def += 1;
        }

        if (GSR() >= 120)
        {
            IceRes += 2;
        }

        if (GSR() >= 130)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 140)
        {
            DragonRes += 2;
        }

        if (GSR() >= 150)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 160)
        {
            Def += 1;
        }

        if (GSR() >= 170)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 180)
        {
            AllRes += 1;
        }

        if (GSR() >= 190)
        {
            Def += 1;
        }

        if (GSR() >= 200)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 210)
        {
            Def += 1;
        }

        if (GSR() >= 220)
        {
            FireRes += 2;
        }

        if (GSR() >= 230)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 240)
        {
            WaterRes += 2;
        }

        if (GSR() >= 250)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 260)
        {
            Def += 1;
        }

        if (GSR() >= 270)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 280)
        {
            ThunderRes += 2;
        }

        if (GSR() >= 290)
        {
            Def += 1;
        }

        if (GSR() >= 300)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 310)
        {
            Def += 1;
        }

        if (GSR() >= 320)
        {
            IceRes += 2;
        }

        if (GSR() >= 330)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 340)
        {
            DragonRes += 2;
        }

        if (GSR() >= 350)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 360)
        {
            Def += 2;
        }

        if (GSR() >= 370)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 380)
        {
            AllRes += 1;
        }

        if (GSR() >= 390)
        {
            Def += 2;
        }

        if (GSR() >= 400)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 410)
        {
            Def += 2;
        }

        if (GSR() >= 420)
        {
            FireRes += 2;
        }

        if (GSR() >= 430)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 440)
        {
            WaterRes += 2;
        }

        if (GSR() >= 450)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 460)
        {
            Def += 2;
        }

        if (GSR() >= 470)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 480)
        {
            ThunderRes += 2;
        }

        if (GSR() >= 490)
        {
            Def += 2;
        }

        if (GSR() >= 500)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 510)
        {
            Def += 2;
        }

        if (GSR() >= 520)
        {
            IceRes += 2;
        }

        if (GSR() >= 530)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 540)
        {
            DragonRes += 2;
        }

        if (GSR() >= 550)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 560)
        {
            Def += 2;
        }

        if (GSR() >= 570)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 580)
        {
            AllRes += 1;
        }

        if (GSR() >= 590)
        {
            Def += 2;
        }

        if (GSR() >= 600)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 610)
        {
            Def += 2;
        }

        if (GSR() >= 620)
        {
            FireRes += 2;
        }

        if (GSR() >= 630)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 640)
        {
            WaterRes += 2;
        }

        if (GSR() >= 650)
        {

            ConquestAtk += 2;
        }

        if (GSR() >= 660)
        {
            Def += 2;
        }

        if (GSR() >= 670)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 680)
        {
            ThunderRes += 2;
        }

        if (GSR() >= 690)
        {
            Def += 2;
        }

        if (GSR() >= 700)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 710)
        {
            Def += 2;
        }

        if (GSR() >= 720)
        {
            IceRes += 2;
        }

        if (GSR() >= 730)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 740)
        {
            DragonRes += 2;
        }

        if (GSR() >= 750)
        {
            ConquestAtk += 2;
        }

        if (GSR() >= 760)
        {
            Def += 2;
        }

        if (GSR() >= 770)
        {
            ConquestDef += 10;
        }

        if (GSR() >= 780)
        {
            AllRes += 1;
        }

        if (GSR() >= 790)
        {
            Def += 2;
        }

        if (GSR() >= 800)
        {
            ConquestAtk += 4;
        }

        if (GSR() >= 810)
        {
            Def += 2;
        }

        if (GSR() >= 820)
        {
            FireRes += 2;
        }

        if (GSR() >= 830)
        {
            ConquestDef += 10;// 15?
        }

        if (GSR() >= 840)
        {
            WaterRes += 2;
        }

        if (GSR() >= 850)
        {
            ConquestAtk += 4;
        }

        if (GSR() >= 860)
        {
            Def += 2;
        }

        if (GSR() >= 870)
        {
            ConquestDef += 10;//15?
        }

        if (GSR() >= 880)
        {
            ThunderRes += 2;
        }

        if (GSR() >= 890)
        {
            Def += 2;
        }

        if (GSR() >= 900)
        {
            ConquestAtk += 4;
        }

        if (GSR() >= 910)
        {
            Def += 2;
        }

        if (GSR() >= 920)
        {
            IceRes += 2;
        }

        if (GSR() >= 930)
        {
            ConquestDef += 10;// 15?
        }

        if (GSR() >= 940)
        {
            DragonRes += 2;
        }

        if (GSR() >= 950)
        {
            ConquestAtk += 4;
        }

        if (GSR() >= 960)
        {
            Def += 2;
        }

        if (GSR() >= 970)
        {
            ConquestDef += 10;// 15?
        }

        if (GSR() >= 980)
        {
            AllRes += 1;
        }

        if (GSR() >= 960)
        {

            Def += 2;
        }

        if (GSR() >= 999)
        {
            ConquestAtk += 4;
        }

        var GSRSkillMultiplier = GetTotalGSRWeaponUnlocks() switch
        {
            "11" => 0,
            "12" => 1,
            "13" => 2,
            "14" => 3,
            _ => 0,
        };

        if (Is11GSR999() && (skillNameType == "Conquest Atk" || skillNameType == "Conquest Def"))
        {
            ConquestAtk = 100 + 5 * GSRSkillMultiplier;
            ConquestDef = 300 + 10 * GSRSkillMultiplier;
        }

        // already tested
        return skillNameType switch
        {
            "Nothing" => "Nothing",
            "Def" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + Def),//goes to 80?
            "Conquest Atk" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + ConquestAtk),
            "Conquest Def" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + ConquestDef),
            "Fire Res" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + FireRes),
            "Water Res" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + WaterRes),
            "Thunder Res" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + ThunderRes),
            "Ice Res" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + IceRes),
            "Dragon Res" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + DragonRes),
            "All Res" => string.Format(CultureInfo.InvariantCulture, "{0}+{1}", skillNameType, skillNameNumber + AllRes),
            _ => "None",
        };
    }

    /// <summary>
    /// Gets the metadata.
    /// </summary>
    /// <value>
    /// The metadata.
    /// </value>
    public static string GetMetadata
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            if (!s.EnableMetadataExport)
            {
                return string.Empty;
            }

            string guildName;
            string hunterName;

            var DateAndTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            if (s.GuildName.Length >= 1)
            {
                guildName = s.GuildName;
            }
            else
            {
                guildName = "Guild Name";
            }

            if (s.HunterName.Length >= 1)
            {
                hunterName = s.HunterName;
            }
            else
            {
                hunterName = "Hunter Name";
            }

            return string.Format(CultureInfo.InvariantCulture, "\n{0} | {1} | {2}", hunterName, guildName, DateAndTime);
        }
    }

    public static string GetHunterName
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            return s.HunterName;
        }
    }

    public static string GetGuildName
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            return s.GuildName;
        }
    }

    public string GetGZenny
    {
        get
        {
            return GZenny().ToString(CultureInfo.InvariantCulture) + " Gz";
        }
    }

    public string GetCaravanLevel
    {
        get
        {
            return "Lv. " + (CaravenGemLevel() + 1).ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetGSRLevel
    {
        get
        {
            return GSR().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetGSRP
    {
        get
        {
            return GSRP().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetGRLevel
    {
        get
        {
            return GRankNumber().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetGRP
    {
        get
        {
            return GRP().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetSR
    {
        get
        {
            var style = WeaponStyle() switch
            {
                0 => "Earth Style",
                1 => "Heaven Style",
                2 => "Storm Style",
                3 => "Extreme Style",
                _ => "Earth Style"
            };

            return string.Format(CultureInfo.InvariantCulture, "{0}", style);
        }
    }

    public string GetRP
    {
        get
        {
            return GSR().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetBloatAtk
    {
        get
        {
            return BloatedWeaponAttack().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetTotalDef
    {
        get
        {
            return TotalDefense().ToString(CultureInfo.InvariantCulture);
        }
    }

    public static string GetGenderName
    {
        get
        {
            return GetGender();
        }
    }

    public string GetGCP
    {
        get
        {
            return GCP().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetGSRLevelString
    {
        get
        {
            return GSR().ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetWeaponClassName
    {
        get
        {
            return GetWeaponClass();
        }
    }

    /// <summary>
    /// Gets the gear description.
    /// </summary>
    /// <value>
    /// The gear description.
    /// </value>
    public static string GetGearDescription
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            if (s.GearDescriptionExport != null || s.GearDescriptionExport != string.Empty)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}\n", s.GearDescriptionExport);
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// Determines whether item [is meta item].
    /// </summary>
    /// <returns>
    ///   <c>true</c> if [is meta item]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMetaItem(int id)
    {
        switch (id)
        {
            default:
                return false;
            case 14642:// hunter taloncharm
            case 1681:// maiden items
            case 1682:
            case 1683:
            case 1684:
            case 1481:
            case 1482:
            case 1483:
            case 4750:// perfect whetstone
            case 8030:// shiri
            case 8026:// encourage
            case 8037:// impatience charm
            case 8031:// elixir of peace
            case 8040:// gathering charm min
            case 8042:// gathering charm max
            case 8047:// health charm
            case 8052:// stamina charm
            case 1548:// starving wolf potion
            case 4943:// serious drink affinity
            case 9719:// all element drug
            case 9715:// quick mega juice
            case 9714:// quick max potion
            case 4358:// potion g2
            case 78:// farcaster
            case 4952:// halk pot
            case 5771:// halk call
            case 1537:// nearcaster
            case 1888:// power sword crystal
            case 1553:// LS spirit drink
            case 14803:// adrenaline charm
            case 14804:// starving wolf charm
            case 91:// sm all barrel bombs
            case 92:
            case 1782:// random ball
            case 300:// poogie crackers
            case 9717:// attack medicine
            case 34:// lifepowder
            case 4723:// gook amulet
            case 5277:// gook bomb
            case 5279:// gook cracker
            case 5711:// gook whistle
            case 5712:// gook fireworks
            case 5714:// gook pickaxe
            case 5715:// gook fishing net
            case 5716:// gook bugnet
            case 5717:// gook gloves

            // decos
            // all ravi
            case 12724:
            case 12725:
            case 12726:
            case 12727:
            case 12728:
            case 12729:
            case 12730:
            case 12731:
            case 12732:
            case 12733:
            case 12734:
            case 12735:
            case 12736:
            case 12737:
            case 12738:
            case 12739:
            case 12740:
            case 12741:
            case 12742:
            case 12743:
            case 12744:
            case 12745:
            case 12746:
            case 12747:
            case 12748:
            case 12749:
            case 12750:
            case 12751:
            case 12752:
            case 12753:
            case 15070:
            case 15071:
            case 15072:
            case 15073:
            case 15074:
            case 15075:
            case 15076:
            case 15077:
            case 15078:
            case 15079:

            // all true hiden
            case 13640:
            case 13641:
            case 13642:
            case 13643:
            case 13644:
            case 13645:
            case 13646:
            case 13647:
            case 13648:
            case 13649:
            case 13650:
            case 13651:
            case 13652:
            case 13653:
            case 13654:
            case 13655:
            case 13656:
            case 13657:
            case 13658:
            case 13659:
            case 13660:
            case 13661:
            case 13662:
            case 13663:
            case 13664:
            case 13665:
            case 13666:
            case 13667:
            case 13668:
            case 13669:
            case 13670:
            case 13671:
            case 13672:
            case 13673:
            case 13674:
            case 13675:
            case 13676:
            case 13677:
            case 13678:
            case 13679:
            case 13680:
            case 13681:
            case 13682:
            case 13683:
            case 13684:
            case 13685:
            case 13686:
            case 13687:
            case 13688:
            case 13689:
            case 13690:
            case 13691:
            case 15546:
            case 15547:
            case 15548:
            case 15549:
                return true;
        }
    }

    /// <summary>
    /// Checks whether the gear is part of the meta
    /// </summary>
    /// <param name="piece"></param>
    /// <returns></returns>
    public static bool IsMetaGear(string piece)
    {
        if (piece.Contains("ZP") || piece.Contains("PZ") || piece.Contains("SnS・") || piece.Contains("DS・") || piece.Contains("GS・") || piece.Contains("LS・") || piece.Contains("Hammer・") || piece.Contains("HH・") || piece.Contains("Lance・") || piece.Contains("GL・") || piece.Contains("Swaxe・") || piece.Contains("Tonfa・") || piece.Contains("Magspike・") || piece.Contains("LBG・") || piece.Contains("HBG・") || piece.Contains("Bow・"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// gets the g rank weapon level
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string GetGRWeaponLevel(int level)
    {
        if (level == 0)
        {
            return string.Empty;
        }
        else
        {
            return " Lv. " + level;
        }
    }

    /// <summary>
    /// gets the road/duremudira skills
    /// </summary>
    public string GetRoadDureSkills
    {
        get
        {
            var Skill1 = RoadDureSkill1Name();
            var Skill2 = RoadDureSkill2Name();
            var Skill3 = RoadDureSkill3Name();
            var Skill4 = RoadDureSkill4Name();
            var Skill5 = RoadDureSkill5Name();
            var Skill6 = RoadDureSkill6Name();
            var Skill7 = RoadDureSkill7Name();
            var Skill8 = RoadDureSkill8Name();
            var Skill9 = RoadDureSkill9Name();
            var Skill10 = RoadDureSkill10Name();
            var Skill11 = RoadDureSkill11Name();
            var Skill12 = RoadDureSkill12Name();
            var Skill13 = RoadDureSkill13Name();
            var Skill14 = RoadDureSkill14Name();
            var Skill15 = RoadDureSkill15Name();
            var Skill16 = RoadDureSkill16Name();

            SkillRoadTower.IDName.TryGetValue(Skill1, out var SkillName1);
            SkillRoadTower.IDName.TryGetValue(Skill2, out var SkillName2);
            SkillRoadTower.IDName.TryGetValue(Skill3, out var SkillName3);
            SkillRoadTower.IDName.TryGetValue(Skill4, out var SkillName4);
            SkillRoadTower.IDName.TryGetValue(Skill5, out var SkillName5);
            SkillRoadTower.IDName.TryGetValue(Skill6, out var SkillName6);
            SkillRoadTower.IDName.TryGetValue(Skill7, out var SkillName7);
            SkillRoadTower.IDName.TryGetValue(Skill8, out var SkillName8);
            SkillRoadTower.IDName.TryGetValue(Skill9, out var SkillName9);
            SkillRoadTower.IDName.TryGetValue(Skill10, out var SkillName10);
            SkillRoadTower.IDName.TryGetValue(Skill11, out var SkillName11);
            SkillRoadTower.IDName.TryGetValue(Skill12, out var SkillName12);
            SkillRoadTower.IDName.TryGetValue(Skill13, out var SkillName13);
            SkillRoadTower.IDName.TryGetValue(Skill14, out var SkillName14);
            SkillRoadTower.IDName.TryGetValue(Skill15, out var SkillName15);
            SkillRoadTower.IDName.TryGetValue(Skill16, out var SkillName16);

            var SkillLevel1 = RoadDureSkill1Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel2 = RoadDureSkill2Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel3 = RoadDureSkill3Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel4 = RoadDureSkill4Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel5 = RoadDureSkill5Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel6 = RoadDureSkill6Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel7 = RoadDureSkill7Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel8 = RoadDureSkill8Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel9 = RoadDureSkill9Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel10 = RoadDureSkill10Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel11 = RoadDureSkill11Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel12 = RoadDureSkill12Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel13 = RoadDureSkill13Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel14 = RoadDureSkill14Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel15 = RoadDureSkill15Level().ToString(CultureInfo.InvariantCulture);
            var SkillLevel16 = RoadDureSkill16Level().ToString(CultureInfo.InvariantCulture);

            // pls
            if (GetTextFormat() == "Markdown")
            {
                if (IsMaxRoadDureSkillLevel(Skill1, SkillLevel1) && (SkillName1 != null || SkillName1 != "None" || SkillName1 != string.Empty))
                {
                    SkillName1 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName1);
                }

                if (IsMaxRoadDureSkillLevel(Skill2, SkillLevel2) && (SkillName2 != null || SkillName2 != "None" || SkillName2 != string.Empty))
                {
                    SkillName2 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName2);
                }

                if (IsMaxRoadDureSkillLevel(Skill3, SkillLevel3) && (SkillName3 != null || SkillName3 != "None" || SkillName3 != string.Empty))
                {
                    SkillName3 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName3);
                }

                if (IsMaxRoadDureSkillLevel(Skill4, SkillLevel4) && (SkillName4 != null || SkillName4 != "None" || SkillName4 != string.Empty))
                {
                    SkillName4 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName4);
                }

                if (IsMaxRoadDureSkillLevel(Skill5, SkillLevel5) && (SkillName5 != null || SkillName5 != "None" || SkillName5 != string.Empty))
                {
                    SkillName5 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName5);
                }

                if (IsMaxRoadDureSkillLevel(Skill6, SkillLevel6) && (SkillName6 != null || SkillName6 != "None" || SkillName6 != string.Empty))
                {
                    SkillName6 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName6);
                }

                if (IsMaxRoadDureSkillLevel(Skill7, SkillLevel7) && (SkillName7 != null || SkillName7 != "None" || SkillName7 != string.Empty))
                {
                    SkillName7 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName7);
                }

                if (IsMaxRoadDureSkillLevel(Skill8, SkillLevel8) && (SkillName8 != null || SkillName8 != "None" || SkillName8 != string.Empty))
                {
                    SkillName8 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName8);
                }

                if (IsMaxRoadDureSkillLevel(Skill9, SkillLevel9) && (SkillName9 != null || SkillName9 != "None" || SkillName9 != string.Empty))
                {
                    SkillName9 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName9);
                }

                if (IsMaxRoadDureSkillLevel(Skill10, SkillLevel10) && (SkillName10 != null || SkillName10 != "None" || SkillName10 != string.Empty))
                {
                    SkillName10 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName10);
                }

                if (IsMaxRoadDureSkillLevel(Skill11, SkillLevel11) && (SkillName11 != null || SkillName11 != "None" || SkillName11 != string.Empty))
                {
                    SkillName11 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName11);
                }

                if (IsMaxRoadDureSkillLevel(Skill12, SkillLevel12) && (SkillName12 != null || SkillName12 != "None" || SkillName12 != string.Empty))
                {
                    SkillName12 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName12);
                }

                if (IsMaxRoadDureSkillLevel(Skill13, SkillLevel13) && (SkillName13 != null || SkillName13 != "None" || SkillName13 != string.Empty))
                {
                    SkillName13 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName13);
                }

                if (IsMaxRoadDureSkillLevel(Skill14, SkillLevel14) && (SkillName14 != null || SkillName14 != "None" || SkillName14 != string.Empty))
                {
                    SkillName14 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName14);
                }

                if (IsMaxRoadDureSkillLevel(Skill15, SkillLevel15) && (SkillName15 != null || SkillName15 != "None" || SkillName15 != string.Empty))
                {
                    SkillName15 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName15);
                }

                if (IsMaxRoadDureSkillLevel(Skill16, SkillLevel16) && (SkillName16 != null || SkillName16 != "None" || SkillName16 != string.Empty))
                {
                    SkillName16 = string.Format(CultureInfo.InvariantCulture, "**{0}** ", SkillName16);
                }
            }

            if (SkillLevel1 == "0")
            {
                SkillLevel1 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill1, SkillLevel1))
                {
                    SkillLevel1 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel1);
                }
                else
                {
                    SkillLevel1 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel1);
                }
            }

            if (SkillLevel2 == "0")
            {
                SkillLevel2 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill2, SkillLevel2))
                {
                    SkillLevel2 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel2);
                }
                else
                {
                    SkillLevel2 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel2);
                }
            }

            if (SkillLevel3 == "0")
            {
                SkillLevel3 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill3, SkillLevel3))
                {
                    SkillLevel3 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel3);
                }
                else
                {
                    SkillLevel3 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel3);
                }
            }

            if (SkillLevel4 == "0")
            {
                SkillLevel4 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill4, SkillLevel4))
                {
                    SkillLevel4 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel4);
                }
                else
                {
                    SkillLevel4 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel4);
                }
            }

            if (SkillLevel5 == "0")
            {
                SkillLevel5 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill5, SkillLevel5))
                {
                    SkillLevel5 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel5);
                }
                else
                {
                    SkillLevel5 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel5);
                }
            }


            if (SkillLevel6 == "0")
            {
                SkillLevel6 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill6, SkillLevel6))
                {
                    SkillLevel6 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel6);
                }
                else
                {
                    SkillLevel6 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel6);
                }
            }

            if (SkillLevel7 == "0")
            {
                SkillLevel7 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill7, SkillLevel7))
                {
                    SkillLevel7 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel7);
                }
                else
                {
                    SkillLevel7 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel7);
                }
            }

            if (SkillLevel8 == "0")
            {
                SkillLevel8 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill8, SkillLevel8))
                {
                    SkillLevel8 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel8);
                }
                else
                {
                    SkillLevel8 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel8);
                }
            }

            if (SkillLevel9 == "0")
            {
                SkillLevel9 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill9, SkillLevel9))
                {
                    SkillLevel9 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel9);
                }
                else
                {
                    SkillLevel9 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel9);
                }
            }

            if (SkillLevel10 == "0")
            {
                SkillLevel10 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill10, SkillLevel10))
                {
                    SkillLevel10 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel10);
                }
                else
                {
                    SkillLevel10 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel10);
                }
            }

            if (SkillLevel11 == "0")
            {
                SkillLevel11 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill11, SkillLevel11))
                {
                    SkillLevel11 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel11);
                }
                else
                {
                    SkillLevel11 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel11);
                }
            }

            if (SkillLevel12 == "0")
            {
                SkillLevel12 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill12, SkillLevel12))
                {
                    SkillLevel12 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel12);
                }
                else
                {
                    SkillLevel12 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel12);
                }
            }

            if (SkillLevel13 == "0")
            {
                SkillLevel13 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill13, SkillLevel13))
                {
                    SkillLevel13 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel13);
                }
                else
                {
                    SkillLevel13 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel13);
                }
            }

            if (SkillLevel14 == "0")
            {
                SkillLevel14 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill14, SkillLevel14))
                {
                    SkillLevel14 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel14);
                }
                else
                {
                    SkillLevel14 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel14);
                }
            }

            if (SkillLevel15 == "0")
            {
                SkillLevel15 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill15, SkillLevel15))
                {
                    SkillLevel15 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel15);
                }
                else
                {
                    SkillLevel15 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel15);
                }
            }

            if (SkillLevel16 == "0")
            {
                SkillLevel16 = string.Empty;
            }
            else
            {
                if (GetTextFormat() == "Markdown" && IsMaxRoadDureSkillLevel(Skill16, SkillLevel16))
                {
                    SkillLevel16 = string.Format(CultureInfo.InvariantCulture, "**LV{0}**", SkillLevel16);
                }
                else
                {
                    SkillLevel16 = string.Format(CultureInfo.InvariantCulture, " LV{0}", SkillLevel16);
                }
            }

            if (SkillName1 == null || SkillName1 == "None" || SkillName1 == string.Empty)
            {
                SkillName1 = string.Empty;
            }
            else if (SkillName2 == null || SkillName2 == "None" || SkillName2 == string.Empty)
            {
                SkillName1 += string.Empty;
            }
            else
            {
                SkillLevel1 += ", ";
            }

            if (SkillName2 == null || SkillName2 == "None" || SkillName2 == string.Empty)
            {
                SkillName2 = string.Empty;
            }
            else if (SkillName3 == null || SkillName3 == "None" || SkillName3 == string.Empty)
            {
                SkillName2 += string.Empty;
            }
            else
            {
                SkillLevel2 += ", ";
            }

            if (SkillName3 == null || SkillName3 == "None" || SkillName3 == string.Empty)
            {
                SkillName3 = string.Empty;
            }
            else if (SkillName4 == null || SkillName4 == "None" || SkillName4 == string.Empty)
            {
                SkillName3 += string.Empty;
            }
            else
            {
                SkillLevel3 += ", ";
            }

            if (SkillName4 == null || SkillName4 == "None" || SkillName4 == string.Empty)
            {
                SkillName4 = string.Empty;
            }
            else if (SkillName5 == null || SkillName5 == "None" || SkillName5 == string.Empty)
            {
                SkillName4 += string.Empty;
            }
            else
            {
                SkillLevel4 += ", ";
            }

            if (SkillName5 == null || SkillName5 == "None" || SkillName5 == string.Empty)
            {
                SkillName5 = string.Empty;
            }
            else if (SkillName6 == null || SkillName6 == "None" || SkillName6 == string.Empty)
            {
                SkillName5 += string.Empty;
            }
            else
            {
                SkillLevel5 += "\n";
            }

            if (SkillName6 == null || SkillName6 == "None" || SkillName6 == string.Empty)
            {
                SkillName6 = string.Empty;
            }
            else if (SkillName7 == null || SkillName7 == "None" || SkillName7 == string.Empty)
            {
                SkillName6 += string.Empty;
            }
            else
            {
                SkillLevel6 += ", ";
            }

            if (SkillName7 == null || SkillName7 == "None" || SkillName7 == string.Empty)
            {
                SkillName7 = string.Empty;
            }
            else if (SkillName8 == null || SkillName8 == "None" || SkillName8 == string.Empty)
            {
                SkillName7 += string.Empty;
            }
            else
            {
                SkillLevel7 += ", ";
            }

            if (SkillName8 == null || SkillName8 == "None" || SkillName8 == string.Empty)
            {
                SkillName8 = string.Empty;
            }
            else if (SkillName9 == null || SkillName9 == "None" || SkillName9 == string.Empty)
            {
                SkillName8 += string.Empty;
            }
            else
            {
                SkillLevel8 += ", ";
            }

            if (SkillName9 == null || SkillName9 == "None" || SkillName9 == string.Empty)
            {
                SkillName9 = string.Empty;
            }
            else if (SkillName10 == null || SkillName10 == "None" || SkillName10 == string.Empty)
            {
                SkillName9 += string.Empty;
            }
            else
            {
                SkillLevel9 += ", ";
            }

            if (SkillName10 == null || SkillName10 == "None" || SkillName10 == string.Empty)
            {
                SkillName10 = string.Empty;
            }
            else if (SkillName11 == null || SkillName11 == "None" || SkillName11 == string.Empty)
            {
                SkillName10 += string.Empty;
            }
            else
            {
                SkillLevel10 += "\n";
            }

            if (SkillName11 == null || SkillName11 == "None" || SkillName11 == string.Empty)
            {
                SkillName11 = string.Empty;
            }
            else if (SkillName12 == null || SkillName12 == "None" || SkillName12 == string.Empty)
            {
                SkillName11 += string.Empty;
            }
            else
            {
                SkillLevel11 += ", ";
            }

            if (SkillName12 == null || SkillName12 == "None" || SkillName12 == string.Empty)
            {
                SkillName12 = string.Empty;
            }
            else if (SkillName13 == null || SkillName13 == "None" || SkillName13 == string.Empty)
            {
                SkillName12 += string.Empty;
            }
            else
            {
                SkillLevel12 += ", ";
            }

            if (SkillName13 == null || SkillName13 == "None" || SkillName13 == string.Empty)
            {
                SkillName13 = string.Empty;
            }
            else if (SkillName14 == null || SkillName14 == "None" || SkillName14 == string.Empty)
            {
                SkillName13 += string.Empty;
            }
            else
            {
                SkillLevel13 += ", ";
            }

            if (SkillName14 == null || SkillName14 == "None" || SkillName14 == string.Empty)
            {
                SkillName14 = string.Empty;
            }
            else if (SkillName15 == null || SkillName15 == "None" || SkillName15 == string.Empty)
            {
                SkillName14 += string.Empty;
            }
            else
            {
                SkillLevel14 += ", ";
            }

            if (SkillName15 == null || SkillName15 == "None" || SkillName15 == string.Empty)
            {
                SkillName15 = string.Empty;
            }
            else if (SkillName16 == null || SkillName16 == "None" || SkillName16 == string.Empty)
            {
                SkillName15 += string.Empty;
            }
            else
            {
                SkillLevel15 += "\n";
            }

            if (SkillName16 == null || SkillName16 == "None" || SkillName16 == string.Empty)
            {
                SkillName16 = string.Empty;
            }
            else
            {
                SkillLevel16 += string.Empty;
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}", SkillName1, SkillLevel1, SkillName2, SkillLevel2, SkillName3, SkillLevel3, SkillName4, SkillLevel4, SkillName5, SkillLevel5, SkillName6, SkillLevel6, SkillName7, SkillLevel7, SkillName8, SkillLevel8, SkillName9, SkillLevel9, SkillName10, SkillLevel10, SkillName11, SkillLevel11, SkillName12, SkillLevel12, SkillName13, SkillLevel13, SkillName14, SkillLevel14, SkillName15, SkillLevel15, SkillName16, SkillLevel16);
        }
    }

    /// <summary>
    /// Generates the gear stats
    /// </summary>
    public string GenerateGearStats(long? runID = null)
    {
        if (runID == null)
        {
            // save gear to variable
            var showGouBoost = string.Empty;

            if (GetGouBoostMode())
            {
                showGouBoost = " (After Gou/Muscle Boost)";
            }

            // zp in bold for markdown
            // fruits and speedrunner items also in bold
            SavedGearStats = string.Format(CultureInfo.InvariantCulture, "【MHF-Z】Overlay {0} {1}({2}){3}\n\n{4}{5}: {6}\nHead: {7}\nChest: {8}\nArms: {9}\nWaist: {10}\nLegs: {11}\nCuffs: {12}\n\nWeapon Attack: {13} | Total Defense: {14}\n\nZenith Skills:\n{15}\n\nAutomatic Skills:\n{16}\n\nActive Skills{17}:\n{18}\n\nCaravan Skills:\n{19}\n\nDiva Skill:\n{20}\n\nGuild Food:\n{21}\n\nStyle Rank:\n{22}\n\nItems:\n{23}\n\nAmmo:\n{24}\n\nPoogie Item:\n{25}\n\nRoad/Duremudira Skills:\n{26}\n", App.CurrentProgramVersion, GetWeaponClass(), GetGender(), GetMetadata, GetGearDescription, CurrentWeaponName, GetRealWeaponName, GetArmorHeadName, GetArmorChestName, GetArmorArmName, GetArmorWaistName, GetArmorLegName, GetCuffs, BloatedWeaponAttack().ToString(CultureInfo.InvariantCulture), TotalDefense().ToString(CultureInfo.InvariantCulture), GetZenithSkills, GetAutomaticSkills, showGouBoost, GetArmorSkills, GetCaravanSkills, GetDivaSkillNameFromID(DivaSkill()), GetArmorSkill(GuildFoodSkill()), GetGSRSkills, GetItemPouch, GetAmmoPouch, GetItemName(PoogieItemUseID()), GetRoadDureSkills);
            MarkdownSavedGearStats = string.Format(CultureInfo.InvariantCulture, "__【MHF-Z】Overlay {0}__ *{1}({2})*{3}\n\n{4}**{5}**: {6}\n**Head:** {7}\n**Chest:** {8}\n**Arms:** {9}\n**Waist:** {10}\n**Legs:** {11}\n**Cuffs:** {12}\n\n**Weapon Attack:** {13} | **Total Defense:** {14}\n\n**Zenith Skills:**\n{15}\n\n**Automatic Skills:**\n{16}\n\n**Active Skills{17}:**\n{18}\n\n**Caravan Skills:**\n{19}\n\n**Diva Skill:**\n{20}\n\n**Guild Food:**\n{21}\n\n**Style Rank:**\n{22}\n\n**Items:**\n{23}\n\n**Ammo:**\n{24}\n\n**Poogie Item:**\n{25}\n\n**Road/Duremudira Skills:**\n{26}\n", App.CurrentProgramVersion, GetWeaponClass(), GetGender(), GetMetadata, GetGearDescription, CurrentWeaponName, GetRealWeaponName, GetArmorHeadName, GetArmorChestName, GetArmorArmName, GetArmorWaistName, GetArmorLegName, GetCuffs, BloatedWeaponAttack().ToString(CultureInfo.InvariantCulture), TotalDefense().ToString(CultureInfo.InvariantCulture), GetZenithSkills, GetAutomaticSkills, showGouBoost, GetArmorSkills, GetCaravanSkills, GetDivaSkillNameFromID(DivaSkill()), GetArmorSkill(GuildFoodSkill()), GetGSRSkills, GetItemPouch, GetAmmoPouch, GetItemName(PoogieItemUseID()), GetRoadDureSkills);
            return string.Format(CultureInfo.InvariantCulture, "【MHF-Z】Overlay {0} {1}({2}){3}\n\n{4}{5}: {6}\nHead: {7}\nChest: {8}\nArms: {9}\nWaist: {10}\nLegs: {11}\nCuffs: {12}\n\nWeapon Attack: {13} | Total Defense: {14}\n\nZenith Skills:\n{15}\n\nAutomatic Skills:\n{16}\n\nActive Skills{17}:\n{18}\n\nCaravan Skills:\n{19}\n\nDiva Skill:\n{20}\n\nGuild Food:\n{21}\n\nStyle Rank:\n{22}\n\nItems:\n{23}\n\nAmmo:\n{24}\n\nPoogie Item:\n{25}\n\nRoad/Duremudira Skills:\n{26}\n", App.CurrentProgramVersion, GetWeaponClass(), GetGender(), GetMetadata, GetGearDescription, CurrentWeaponName, GetRealWeaponName, GetArmorHeadName, GetArmorChestName, GetArmorArmName, GetArmorWaistName, GetArmorLegName, GetCuffs, BloatedWeaponAttack().ToString(CultureInfo.InvariantCulture), TotalDefense().ToString(CultureInfo.InvariantCulture), GetZenithSkills, GetAutomaticSkills, showGouBoost, GetArmorSkills, GetCaravanSkills, GetDivaSkillNameFromID(DivaSkill()), GetArmorSkill(GuildFoodSkill()), GetGSRSkills, GetItemPouch, GetAmmoPouch, GetItemName(PoogieItemUseID()), GetRoadDureSkills);
        }
        else
        {
            var activeSkills = DatabaseManagerInstance.GetActiveSkills((long)runID);

            if (string.IsNullOrEmpty(activeSkills.CreatedBy))
            {
                return "Run Not Found.\n\nReload the section.";
            }

            var ammoPouch = DatabaseManagerInstance.GetAmmoPouch((long)runID);
            var automaticSkills = DatabaseManagerInstance.GetAutomaticSkills((long)runID);
            var caravanSkills = DatabaseManagerInstance.GetCaravanSkills((long)runID);
            var playerGear = DatabaseManagerInstance.GetPlayerGear((long)runID);
            var playerInventory = DatabaseManagerInstance.GetPlayerInventory((long)runID);
            var roadDureSkills = DatabaseManagerInstance.GetRoadDureSkills((long)runID);
            var styleRankSkills = DatabaseManagerInstance.GetStyleRankSkills((long)runID);
            var zenithSkills = DatabaseManagerInstance.GetZenithSkills((long)runID);
            var quest = DatabaseManagerInstance.GetQuest((long)runID);

            var createdBy = playerGear.CreatedBy;
            var weaponClass = GetWeaponClass((int?)playerGear.WeaponClassID);
            var gender = GetGender();
            var metadata = GetMetadata;
            var gearName = playerGear.GearName;
            var weaponName = GetWeaponNameFromType((int)playerGear.WeaponTypeID);
            var weaponID = (long)(
                playerGear.BlademasterWeaponID == null ?
                    playerGear.GunnerWeaponID == null ?
                        0
                        : playerGear.GunnerWeaponID
                    : playerGear.BlademasterWeaponID);
            var realweaponName = GetRealWeaponNameForRunID(GetWeaponClass((int)playerGear.WeaponClassID), GetWeaponNameFromType((int)playerGear.WeaponTypeID), playerGear.StyleID, weaponID, playerGear.WeaponSlot1, playerGear.WeaponSlot2, playerGear.WeaponSlot3);
            var head = GetArmorHeadNameForRunID((int)playerGear.HeadID, (int)playerGear.HeadSlot1ID, (int)playerGear.HeadSlot2ID, (int)playerGear.HeadSlot3ID);
            var chest = GetArmorChestNameForRunID((int)playerGear.ChestID, (int)playerGear.ChestSlot1ID, (int)playerGear.ChestSlot2ID, (int)playerGear.ChestSlot3ID);
            var arms = GetArmorArmNameForRunID((int)playerGear.ArmsID, (int)playerGear.ArmsSlot1ID, (int)playerGear.ArmsSlot2ID, (int)playerGear.ArmsSlot3ID);
            var waist = GetArmorWaistNameForRunID((int)playerGear.WaistID, (int)playerGear.WaistSlot1ID, (int)playerGear.WaistSlot2ID, (int)playerGear.WaistSlot3ID);
            var legs = GetArmorLegNameForRunID((int)playerGear.LegsID, (int)playerGear.LegsSlot1ID, (int)playerGear.LegsSlot2ID, (int)playerGear.LegsSlot3ID);
            var cuffs = GetCuffsForRunID(playerGear.Cuff1ID, playerGear.Cuff2ID);
            var date = playerGear.CreatedAt;
            var hash = playerGear.PlayerGearHash;
            var zenithSkillsList = GetZenithSkillsForRunID((int)zenithSkills.ZenithSkill1ID, (int)zenithSkills.ZenithSkill2ID, (int)zenithSkills.ZenithSkill3ID, (int)zenithSkills.ZenithSkill4ID, (int)zenithSkills.ZenithSkill5ID, (int)zenithSkills.ZenithSkill6ID, (int)zenithSkills.ZenithSkill7ID);
            var automaticSkillsList = GetAutomaticSkillsForRunID((int)automaticSkills.AutomaticSkill1ID, (int)automaticSkills.AutomaticSkill2ID, (int)automaticSkills.AutomaticSkill3ID, (int)automaticSkills.AutomaticSkill4ID, (int)automaticSkills.AutomaticSkill5ID, (int)automaticSkills.AutomaticSkill6ID);
            var gouBoost = string.Empty;
            var armorSkills = GetArmorSkillsForRunID((int)activeSkills.ActiveSkill1ID, (int)activeSkills.ActiveSkill2ID, (int)activeSkills.ActiveSkill3ID, (int)activeSkills.ActiveSkill4ID, (int)activeSkills.ActiveSkill5ID, (int)activeSkills.ActiveSkill6ID, (int)activeSkills.ActiveSkill7ID, (int)activeSkills.ActiveSkill8ID, (int)activeSkills.ActiveSkill9ID, (int)activeSkills.ActiveSkill10ID, (int)activeSkills.ActiveSkill11ID, (int)activeSkills.ActiveSkill12ID, (int)activeSkills.ActiveSkill13ID, (int)activeSkills.ActiveSkill14ID, (int)activeSkills.ActiveSkill15ID, (int)activeSkills.ActiveSkill16ID, (int)activeSkills.ActiveSkill17ID, (int)activeSkills.ActiveSkill18ID, (int)activeSkills.ActiveSkill19ID);
            var caravanSkillsList = GetCaravanSkillsForRunID((int)caravanSkills.CaravanSkill1ID, (int)caravanSkills.CaravanSkill2ID, (int)caravanSkills.CaravanSkill3ID);
            var divaSkill = GetDivaSkillNameFromID((int)playerGear.DivaSkillID);
            var guildFood = GetArmorSkill((int)playerGear.GuildFoodID);
            var styleRankSkillsList = GetGSRSkillsForRunID((int)styleRankSkills.StyleRankSkill1ID, (int)styleRankSkills.StyleRankSkill2ID);
            var inventory = GetItemsForRunID(new int[] { (int)playerInventory.Item1ID, (int)playerInventory.Item2ID, (int)playerInventory.Item3ID, (int)playerInventory.Item4ID, (int)playerInventory.Item5ID, (int)playerInventory.Item6ID, (int)playerInventory.Item7ID, (int)playerInventory.Item8ID, (int)playerInventory.Item9ID, (int)playerInventory.Item10ID, (int)playerInventory.Item11ID, (int)playerInventory.Item12ID, (int)playerInventory.Item13ID, (int)playerInventory.Item14ID, (int)playerInventory.Item15ID, (int)playerInventory.Item16ID, (int)playerInventory.Item17ID, (int)playerInventory.Item18ID, (int)playerInventory.Item19ID, (int)playerInventory.Item20ID });
            var ammo = GetItemsForRunID(new int[] { (int)ammoPouch.Item1ID, (int)ammoPouch.Item2ID, (int)ammoPouch.Item3ID, (int)ammoPouch.Item4ID, (int)ammoPouch.Item5ID, (int)ammoPouch.Item6ID, (int)ammoPouch.Item7ID, (int)ammoPouch.Item8ID, (int)ammoPouch.Item9ID, (int)ammoPouch.Item10ID });
            var poogieItem = GetItemName((int)playerGear.PoogieItemID);
            var roadDureSkillsList = GetRoadDureSkillsForRunID(new int[] { (int)roadDureSkills.RoadDureSkill1ID, (int)roadDureSkills.RoadDureSkill2ID, (int)roadDureSkills.RoadDureSkill3ID, (int)roadDureSkills.RoadDureSkill4ID, (int)roadDureSkills.RoadDureSkill5ID, (int)roadDureSkills.RoadDureSkill6ID, (int)roadDureSkills.RoadDureSkill7ID, (int)roadDureSkills.RoadDureSkill8ID, (int)roadDureSkills.RoadDureSkill9ID, (int)roadDureSkills.RoadDureSkill10ID, (int)roadDureSkills.RoadDureSkill11ID, (int)roadDureSkills.RoadDureSkill12ID, (int)roadDureSkills.RoadDureSkill13ID, (int)roadDureSkills.RoadDureSkill14ID, (int)roadDureSkills.RoadDureSkill15ID, (int)roadDureSkills.RoadDureSkill16ID });
            var questName = EZlion.Mapper.Quest.IDName[(int)quest.QuestID];
            var questObjectiveType = EZlion.Mapper.ObjectiveType.IDName[(int)quest.ObjectiveTypeID];
            var questObjectiveQuantity = quest.ObjectiveQuantity;
            var questObjectiveName = quest.ObjectiveName;
            var questCategory = quest.ActualOverlayMode;
            var partySize = quest.PartySize;

            // TODO: fix
            // var partnyaBagItems = GetItemsForRunID(new int[] { (int)partnyaBag.Item1ID, (int)partnyaBag.Item2ID, (int)partnyaBag.Item3ID, (int)partnyaBag.Item4ID, (int)partnyaBag.Item5ID, (int)partnyaBag.Item6ID, (int)partnyaBag.Item7ID, (int)partnyaBag.Item8ID, (int)partnyaBag.Item9ID, (int)partnyaBag.Item10ID });
            return string.Format(CultureInfo.InvariantCulture,
@"{0} {1}({2}){3}

Set Name: {4}
{5}: {6}
Head: {7}
Chest: {8}
Arms: {9}
Waist: {10}
Legs: {11}
Cuffs: {12}

Run Date: {13} | Run Hash: {14}

Zenith Skills:
{15}

Automatic Skills:
{16}

Active Skills{17}:
{18}

Caravan Skills:
{19}

Diva Skill:
{20}

Guild Food:
{21}

Style Rank:
{22}

Items:
{23}

Ammo:
{24}

Poogie Item:
{25}

Road/Duremudira Skills:
{26}

Quest: {27}
{28} {29} {30}
Category: {31}
Party Size: {32}",
            createdBy,
            weaponClass,
            gender,
            metadata,
            gearName,
            weaponName,
            realweaponName,
            head,
            chest,
            arms,
            waist,
            legs,
            cuffs,
            date,
            hash,
            zenithSkillsList,
            automaticSkillsList,
            gouBoost,
            armorSkills,
            caravanSkillsList,
            divaSkill,
            guildFood,
            styleRankSkillsList,
            inventory,
            ammo,
            poogieItem,
            roadDureSkillsList,

            // partnyaBagItems
            questName,
            questObjectiveType,
            questObjectiveQuantity,
            questObjectiveName,
            questCategory,
            partySize
            );
        }
    }

    /// <summary>
    /// Gets the gear stats.
    /// </summary>
    /// <value>
    /// The gear stats.
    /// </value>
    public string GetGearStats
    {
        get
        {
            return GenerateGearStats();
        }
    }

    public int CalculateTotalLargeMonstersHunted()
    {
        return RoadFatalisSlain() +
            FirstDistrictDuremudiraSlays() +
            SecondDistrictDuremudiraSlays() +
            RathianHunted() +
            FatalisHunted() +
            YianKutKuHunted() +
            LaoShanLungHunted() +
            CephadromeHunted() +
            RathalosHunted() +
            DiablosHunted() +
            KhezuHunted() +
            GraviosHunted() +
            GypcerosHunted() +
            PlesiothHunted() +
            BasariosHunted() +
            MonoblosHunted() +
            VelocidromeHunted() +
            GendromeHunted() +
            IodromeHunted() +
            KirinHunted() +
            CrimsonFatalisHunted() +
            PinkRathianHunted() +
            BlueYianKutKuHunted() +
            PurpleGypcerosHunted() +
            YianGarugaHunted() +
            SilverRathalosHunted() +
            GoldRathianHunted() +
            BlackDiablosHunted() +
            WhiteMonoblosHunted() +
            RedKhezuHunted() +
            GreenPlesiothHunted() +
            BlackGraviosHunted() +
            DaimyoHermitaurHunted() +
            AzureRathalosHunted() +
            AshenLaoShanLungHunted() +
            BlangongaHunted() +
            CongalalaHunted() +
            RajangHunted() +
            KushalaDaoraHunted() +
            ShenGaorenHunted() +
            YamaTsukamiHunted() +
            ChameleosHunted() +
            RustedKushalaDaoraHunted() +
            LunastraHunted() +
            TeostraHunted() +
            ShogunCeanataurHunted() +
            BulldromeHunted() +
            WhiteFatalisHunted() +
            HypnocHunted() +
            VolganosHunted() +
            TigrexHunted() +
            AkantorHunted() +
            BrightHypnocHunted() +
            RedVolganosHunted() +
            EspinasHunted() +
            OrangeEspinasHunted() +
            SilverHypnocHunted() +
            AkuraVashimuHunted() +
            AkuraJebiaHunted() +
            BerukyurosuHunted() +
            PariapuriaHunted() +
            WhiteEspinasHunted() +
            KamuOrugaronHunted() +
            NonoOrugaronHunted() +
            DyuragauaHunted() +
            DoragyurosuHunted() +
            GurenzeburuHunted() +
            RukodioraHunted() +
            UnknownHunted() +
            GogomoaHunted() +
            TaikunZamuzaHunted() +
            AbioruguHunted() +
            KuarusepusuHunted() +
            OdibatorasuHunted() +
            DisufiroaHunted() +
            RebidioraHunted() +
            AnorupatisuHunted() +
            HyujikikiHunted() +
            MidogaronHunted() +
            GiaoruguHunted() +
            MiRuHunted() +
            FarunokkuHunted() +
            PokaradonHunted() +
            ShantienHunted() +
            GoruganosuHunted() +
            AruganosuHunted() +
            BaruragaruHunted() +
            ZerureusuHunted() +
            GougarfHunted() +
            ForokururuHunted() +
            MeraginasuHunted() +
            DiorexHunted() +
            GarubaDaoraHunted() +
            InagamiHunted() +
            VarusaburosuHunted() +
            PoborubarumuHunted() +
            GureadomosuHunted() +
            HarudomeruguHunted() +
            ToridclessHunted() +
            GasurabazuraHunted() +
            YamaKuraiHunted() +
            ZinogreHunted() +
            DeviljhoHunted() +
            BrachydiosHunted() +
            ToaTesukatoraHunted() +
            BariothHunted() +
            UragaanHunted() +
            StygianZinogreHunted() +
            GuanzorumuHunted() +
            StarvingDeviljhoHunted() +
            VoljangHunted() +
            NargacugaHunted() +
            KeoaruboruHunted() +
            ZenaserisuHunted() +
            GoreMagalaHunted() +
            BlinkingNargacugaHunted() +
            ShagaruMagalaHunted() +
            AmatsuHunted() +
            ElzelionHunted() +
            ArrogantDuremudiraHunted() +
            SeregiosHunted() +
            BogabadorumuHunted() +
            BlitzkriegBogabadorumuHunted() +
            SparklingZerureusuHunted() +
            KingShakalakaHunted();
    }

    public int CalculateTotalSmallMonstersHunted()
    {
        return KelbiHunted() +
        MosswineHunted() +
        BullfangoHunted() +
        FelyneHunted() +
        AptonothHunted() +
        GenpreyHunted() +
        VelocipreyHunted() +
        VespoidHunted() +
        MelynxHunted() +
        HornetaurHunted() +
        ApcerosHunted() +
        RocksHunted() +
        IopreyHunted() +
        CephalosHunted() +
        GiapreyHunted() +
        GreatThunderbugHunted() +
        ShakalakaHunted() +
        BlangoHunted() +
        CongaHunted() +
        RemobraHunted() +
        HermitaurHunted() +
        AntekaHunted() +
        PopoHunted() +
        CeanataurHunted() +
        CactusHunted() +
        GorgeObjectsHunted() +
        BurukkuHunted() +
        ErupeHunted() +
        PokaraHunted() +
        UrukiHunted() +
        KusubamiHunted() +
        PSO2RappyHunted();
    }

    /// <summary>
    /// Generates the compendium.
    /// </summary>
    /// <returns></returns>
    public string GenerateCompendium(DataLoader dataLoader)
    {
        var createdBy = GetFullCurrentProgramVersion();
        var createdAt = DateTime.UtcNow;

        if (createdBy == null)
        {
            return "Program Version Not Found.\n\nReload the section.";
        }

        var questCompendium = DatabaseManagerInstance.GetQuestCompendium();
        var gearCompendium = DatabaseManagerInstance.GetGearCompendium();
        var performanceCompendium = DatabaseManagerInstance.GetPerformanceCompendium();
        var mezeportaFestivalCompendium = DatabaseManagerInstance.GetMezFesCompendium();
        var miscellaneousCompendium = DatabaseManagerInstance.GetMiscellaneousCompendium();
        var monsterCompendium = DatabaseManagerInstance.GetMonsterCompendium();

        var mostCompletedQuest = questCompendium.MostCompletedQuestRuns;
        var mostCompletedQuestAttempts = questCompendium.MostCompletedQuestRunsAttempted;
        var mostCompletedQuestID = questCompendium.MostCompletedQuestRunsQuestID;

        var mostAttemptedQuest = questCompendium.MostAttemptedQuestRuns;
        var mostAttemptedQuestCompletions = questCompendium.MostAttemptedQuestRunsCompleted;
        var mostAttemptedQuestID = questCompendium.MostAttemptedQuestRunsQuestID;

        var totalQuestsCompleted = questCompendium.TotalQuestsCompleted;
        var totalQuestsAttempted = questCompendium.TotalQuestsAttempted;

        var questCompletionTimeElapsedAverage = questCompendium.QuestCompletionTimeElapsedAverage;
        var questCompletionTimeElapsedMedian = questCompendium.QuestCompletionTimeElapsedMedian;

        var totalTimeElapsedDuringQuest = questCompendium.TotalTimeElapsedQuests;

        var mostCompletedQuestWithCarts = questCompendium.MostCompletedQuestWithCarts;
        var mostCompletedQuestWithCartsQuestID = questCompendium.MostCompletedQuestWithCartsQuestID;

        var totalCartsInQuest = questCompendium.TotalCartsInQuest;
        var totalCartsInQuestAverage = questCompendium.TotalCartsInQuestAverage;
        var totalCartsInQuestMedian = questCompendium.TotalCartsInQuestMedian;

        var questPartySizeAverage = questCompendium.QuestPartySizeAverage;
        var questPartySizeMedian = questCompendium.QuestPartySizeMedian;
        var questPartySizeMode = questCompendium.QuestPartySizeMode;

        var percentOfSoloQuests = questCompendium.PercentOfSoloQuests;
        var percentOfGuildFood = questCompendium.PercentOfGuildFood;
        var percentOfDivaSkill = questCompendium.PercentOfDivaSkill;
        var percentOfSkillFruit = questCompendium.PercentOfSkillFruit;
        var mostCommonDivaSkill = questCompendium.MostCommonDivaSkill;
        var mostCommonGuildFood = questCompendium.MostCommonGuildFood;

        var mostUsedWeaponType = gearCompendium.MostUsedWeaponType;
        var totalUniqueArmorPieces = gearCompendium.TotalUniqueArmorPiecesUsed;
        var totalUniqueWeapons = gearCompendium.TotalUniqueWeaponsUsed;
        var totalUniqueDecorations = gearCompendium.TotalUniqueDecorationsUsed;

        var mostCommonDecorationID = gearCompendium.MostCommonDecorationID;
        var leastUsedArmorSkill = gearCompendium.LeastUsedArmorSkill;

        var highestTrueRaw = performanceCompendium.HighestTrueRaw;
        var trueRawAverage = performanceCompendium.TrueRawAverage;
        var trueRawMedian = performanceCompendium.TrueRawMedian;
        var highestTrueRawRunID = performanceCompendium.HighestTrueRawRunID;

        var highestSingleHitDamage = performanceCompendium.HighestSingleHitDamage;
        var singleHitDamageAverage = performanceCompendium.SingleHitDamageAverage;
        var singleHitDamageMedian = performanceCompendium.SingleHitDamageMedian;
        var highestSingleHitDamageRunID = performanceCompendium.HighestSingleHitDamageRunID;

        var highestHitCount = performanceCompendium.HighestHitCount;
        var hitCountAverage = performanceCompendium.HitCountAverage;
        var hitCountMedian = performanceCompendium.HitCountMedian;
        var highestHitCountRunID = performanceCompendium.HighestHitCountRunID;

        var highestHitsTakenBlocked = performanceCompendium.HighestHitsTakenBlocked;
        var hitsTakenBlockedAverage = performanceCompendium.HitsTakenBlockedAverage;
        var hitsTakenBlockedMedian = performanceCompendium.HitsTakenBlockedMedian;
        var highestHitsTakenBlockedRunID = performanceCompendium.HighestHitsTakenBlockedRunID;

        var highestDPS = performanceCompendium.HighestDPS;
        var DPSAverage = performanceCompendium.DPSAverage;
        var DPSMedian = performanceCompendium.DPSMedian;
        var highestDPSRunID = performanceCompendium.HighestDPSRunID;

        var highestHitsPerSecond = performanceCompendium.HighestHitsPerSecond;
        var hitsPerSecondAverage = performanceCompendium.HitsPerSecondAverage;
        var hitsPerSecondMedian = performanceCompendium.HitsPerSecondMedian;
        var highestHitsPerSecondRunID = performanceCompendium.HighestHitsPerSecondRunID;

        var highestHitsTakenBlockedPerSecond = performanceCompendium.HighestHitsTakenBlockedPerSecond;
        var hitsTakenBlockedPerSecondAverage = performanceCompendium.HitsTakenBlockedPerSecondAverage;
        var hitsTakenBlockedPerSecondMedian = performanceCompendium.HitsTakenBlockedPerSecondMedian;
        var highestHitsTakenBlockedPerSecondRunID = performanceCompendium.HighestHitsTakenBlockedPerSecondRunID;

        var highestActionsPerMinute = performanceCompendium.HighestActionsPerMinute;
        var actionsPerMinuteAverage = performanceCompendium.ActionsPerMinuteAverage;
        var actionsPerMinuteMedian = performanceCompendium.ActionsPerMinuteMedian;
        var highestActionsPerMinuteRunID = performanceCompendium.HighestActionsPerMinuteRunID;

        var totalHitsCount = performanceCompendium.TotalHitsCount;
        var totalHitsTakenBlocked = performanceCompendium.TotalHitsTakenBlocked;
        var totalActions = performanceCompendium.TotalActions;

        var healthAverage = performanceCompendium.HealthAverage;
        var healthMedian = performanceCompendium.HealthMedian;
        var healthMode = performanceCompendium.HealthMode;

        var staminaAverage = performanceCompendium.StaminaAverage;
        var staminaMedian = performanceCompendium.StaminaMedian;
        var staminaMode = performanceCompendium.StaminaMode;

        var minigamesPlayed = mezeportaFestivalCompendium.MinigamesPlayed;

        var urukiPachinkoTimesPlayed = mezeportaFestivalCompendium.UrukiPachinkoTimesPlayed;
        var urukiPachinkoHighscore = mezeportaFestivalCompendium.UrukiPachinkoHighscore;
        var urukiPachinkoScoreAverage = mezeportaFestivalCompendium.UrukiPachinkoAverageScore;
        var urukiPachinkoScoreMedian = mezeportaFestivalCompendium.UrukiPachinkoMedianScore;

        var guukuScoopTimesPlayed = mezeportaFestivalCompendium.GuukuScoopTimesPlayed;
        var guukuScoopHighscore = mezeportaFestivalCompendium.GuukuScoopHighscore;
        var guukuScoopScoreAverage = mezeportaFestivalCompendium.GuukuScoopAverageScore;
        var guukuScoopScoreMedian = mezeportaFestivalCompendium.GuukuScoopMedianScore;

        var nyanrendoTimesPlayed = mezeportaFestivalCompendium.NyanrendoTimesPlayed;
        var nyanrendoHighscore = mezeportaFestivalCompendium.NyanrendoHighscore;
        var nyanrendoScoreAverage = mezeportaFestivalCompendium.NyanrendoAverageScore;
        var nyanrendoScoreMedian = mezeportaFestivalCompendium.NyanrendoMedianScore;

        var panicHoneyTimesPlayed = mezeportaFestivalCompendium.PanicHoneyTimesPlayed;
        var panicHoneyHighscore = mezeportaFestivalCompendium.PanicHoneyHighscore;
        var panicHoneyScoreAverage = mezeportaFestivalCompendium.PanicHoneyAverageScore;
        var panicHoneyScoreMedian = mezeportaFestivalCompendium.PanicHoneyMedianScore;

        var dokkanBattleCatsTimesPlayed = mezeportaFestivalCompendium.DokkanBattleCatsTimesPlayed;
        var dokkanBattleCatsHighscore = mezeportaFestivalCompendium.DokkanBattleCatsHighscore;
        var dokkanBattleCatsScoreAverage = mezeportaFestivalCompendium.DokkanBattleCatsAverageScore;
        var dokkanBattleCatsScoreMedian = mezeportaFestivalCompendium.DokkanBattleCatsMedianScore;

        var monster1AttackMultiplierHighest = monsterCompendium.HighestMonsterAttackMultiplier;
        var monster1AttackMultiplierHighestRunID = monsterCompendium.HighestMonsterAttackMultiplierRunID;

        var monster1AttackMultiplierLowest = monsterCompendium.LowestMonsterAttackMultiplier;
        var monster1AttackMultiplierLowestRunID = monsterCompendium.LowestMonsterAttackMultiplierRunID;

        var monster1DefenseRateHighest = monsterCompendium.HighestMonsterDefenseRate;
        var monster1DefenseRateHighestRunID = monsterCompendium.HighestMonsterDefenseRateRunID;

        var monster1DefenseRateLowest = monsterCompendium.LowestMonsterDefenseRate;
        var monster1DefenseRateLowestRunID = monsterCompendium.LowestMonsterDefenseRateRunID;

        var monster1SizeMultiplierHighest = monsterCompendium.HighestMonsterSizeMultiplier;
        var monster1SizeMultiplierHighestRunID = monsterCompendium.HighestMonsterSizeMultiplierRunID;

        var monster1SizeMultiplierLowest = monsterCompendium.LowestMonsterSizeMultiplier;
        var monster1SizeMultiplierLowestRunID = monsterCompendium.LowestMonsterSizeMultiplierRunID;

        var totalLargeMonstersHunted = CalculateTotalLargeMonstersHunted();
        var totalSmallMonstersHunted = CalculateTotalSmallMonstersHunted();

        var totalOverlaySessions = miscellaneousCompendium.TotalOverlaySessions;
        var sessionDurationHighest = miscellaneousCompendium.HighestSessionDuration;
        var sessionDurationLowest = miscellaneousCompendium.LowestSessionDuration;
        var sessionDurationAverage = miscellaneousCompendium.AverageSessionDuration;
        var sessionDurationMedian = miscellaneousCompendium.MedianSessionDuration;

        SkillArmor.IDName.TryGetValue((int)mostCommonGuildFood, out var mostCommonGuildFoodName);
        SkillDiva.IDName.TryGetValue((int)mostCommonDivaSkill, out var mostCommonDivaSkillName);
        EZlion.Mapper.WeaponType.IDName.TryGetValue((int)mostUsedWeaponType, out var mostUsedWeaponTypeName);
        Item.IDName.TryGetValue((int)mostCommonDecorationID, out var mostCommonDecorationName);
        SkillArmor.IDName.TryGetValue((int)leastUsedArmorSkill, out var leastUsedArmorSkillName);

        return string.Format(CultureInfo.InvariantCulture,
@"{0} (UTC)
{1}

Quest
Most Completed Quest: {2} (Attempted {3}) [Quest ID {4}]
Most Attempted Quest: {5} (Completed {6}) [Quest ID {7}]
Total Quests Completed/Attempted: {8}/{9}
Quest Completion Time Elapsed (Average/Median): {10} / {11}
Total Time Elapsed during Quest: {12}
Most Completed Quest with Carts: {13} [Quest ID {14}]
Total Carts in Quest (Average/Median): {15} ({16}/{17})
Quest Party Size (Average/Median/Mode): {18}/{19}/{20}
Percent of Solo Quests: {21}
Percent of Guild Food in Quests: {22}
Percent of Diva Skill in Quests: {23}
Percent of Skill Fruit in Quests: {24}
Most Common Diva Skill in Quests: {25}
Most Common Guild Food in Quests: {26}

Gear
Most Used Weapon Type: {27}
Total Unique Armor Pieces/Weapons/Decorations used: {28}/{29}/{30}
Most Common Decoration: {31} [ID {32}]
Least Used Armor Skill: {33}

Hunter Performance
Highest True Raw (Average/Median): {34} ({35}/{36}) [Run ID {37}]
Highest Single Hit Damage (Average/Median): {38} ({39}/{40}) [Run ID {41}]
Highest Hit Count (Average/Median): {42} ({43}/{44}) [Run ID {45}]
Highest Hits Taken/Blocked (Average/Median): {46} ({47}/{48}) [Run ID {49}]
Highest DPS (Average/Median): {50} ({51}/{52}) [Run ID {53}]
Highest Hits per Second (Average/Median): {54} ({55}/{56}) [Run ID {57}]
Highest Hits Taken/Blocked per Second (Average/Median): {58} ({59}/{60}) [Run ID {61}]
Highest Actions per Minute (Average/Median): {62} ({63}/{64}) [Run ID {65}]
Total Hits Count: {66}
Total Hits Taken/Blocked: {67}
Total Actions: {68}
Health (Average/Median/Mode): {69}/{70}/{71}
Stamina (Average/Median/Mode): {72}/{73}/{74}

Mezeporta Festival (MezFes)
Minigames Played: {75}
Uruki Pachinko Times Played: {76}
Uruki Pachinko High-score (Average/Median): {77} ({78}/{79})
Guuku Scoop Times Played: {80}
Guuku Scoop High-score (Average/Median): {81} ({82}/{83})
Nyanrendo Times Played: {84}
Nyanrendo High-score (Average/Median): {85} ({86}/{87})
Panic Honey Times Played: {88}
Panic Honey High-score (Average/Median): {89} ({90}/{91})
Dokkan Battle Cats Times Played: {92}
Dokkan Battle Cats High-score (Average/Median): {93} ({94}/{95})

Monster
Highest Monster Attack Multiplier: {96} [Run ID {97}]
Lowest Monster Attack Multiplier: {98} [Run ID {99}]
Highest Monster Defense Rate: {100} [Run ID {101}]
Lowest Monster Defense Rate: {102} [Run ID {103}]
Highest Monster Size Multiplier: {104} [Run ID {105}]
Lowest Monster Size Multiplier: {106} [Run ID {107}]
Total Large Monsters Hunted: {108}
Total Small Monsters Hunted: {109}

Miscellaneous
Total Overlay Sessions: {110}
Session Duration (Highest/Lowest/Average/Median): {111} / {112} / {113} / {114}
",
        createdAt,
        createdBy,
        mostCompletedQuest,
        mostCompletedQuestAttempts,
        mostCompletedQuestID,
        mostAttemptedQuest,
        mostAttemptedQuestCompletions,
        mostAttemptedQuestID,
        totalQuestsCompleted,
        totalQuestsAttempted,
        dataLoader.model.GetMinutesSecondsMillisecondsFromFrames((long)questCompletionTimeElapsedAverage),
        dataLoader.model.GetMinutesSecondsMillisecondsFromFrames((long)questCompletionTimeElapsedMedian),
        dataLoader.model.GetMinutesSecondsMillisecondsFromFrames(totalTimeElapsedDuringQuest),
        mostCompletedQuestWithCarts,
        mostCompletedQuestWithCartsQuestID,
        totalCartsInQuest,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", totalCartsInQuestAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", totalCartsInQuestMedian),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", questPartySizeAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", questPartySizeMedian),
        questPartySizeMode,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", percentOfSoloQuests),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", percentOfGuildFood),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", percentOfDivaSkill),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", percentOfSkillFruit),
        mostCommonDivaSkillName,
        mostCommonGuildFoodName,
        mostUsedWeaponTypeName,
        totalUniqueArmorPieces,
        totalUniqueWeapons,
        totalUniqueDecorations,
        mostCommonDecorationName,
        mostCommonDecorationID.ToString("X", CultureInfo.InvariantCulture),
        leastUsedArmorSkillName,
        highestTrueRaw,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", trueRawAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", trueRawMedian),
        highestTrueRawRunID,
        highestSingleHitDamage,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", singleHitDamageAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", singleHitDamageMedian),
        highestSingleHitDamageRunID,
        highestHitCount,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitCountAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitCountMedian),
        highestHitCountRunID,
        highestHitsTakenBlocked,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitsTakenBlockedAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitsTakenBlockedMedian),
        highestHitsTakenBlockedRunID,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", highestDPS),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", DPSAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", DPSMedian),
        highestDPSRunID,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", highestHitsPerSecond),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitsPerSecondAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitsPerSecondMedian),
        highestHitsPerSecondRunID,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", highestHitsTakenBlockedPerSecond),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitsTakenBlockedPerSecondAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", hitsTakenBlockedPerSecondMedian),
        highestHitsTakenBlockedPerSecondRunID,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", highestActionsPerMinute),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", actionsPerMinuteAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", actionsPerMinuteMedian),
        highestActionsPerMinuteRunID,
        totalHitsCount,
        totalHitsTakenBlocked,
        totalActions,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", healthAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", healthMedian),
        healthMode,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", staminaAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", staminaMedian),
        staminaMode,
        minigamesPlayed,
        urukiPachinkoTimesPlayed,
        urukiPachinkoHighscore,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", urukiPachinkoScoreAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", urukiPachinkoScoreMedian),
        guukuScoopTimesPlayed,
        guukuScoopHighscore,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", guukuScoopScoreAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", guukuScoopScoreMedian),
        nyanrendoTimesPlayed,
        nyanrendoHighscore,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", nyanrendoScoreAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", nyanrendoScoreMedian),
        panicHoneyTimesPlayed,
        panicHoneyHighscore,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", panicHoneyScoreAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", panicHoneyScoreMedian),
        dokkanBattleCatsTimesPlayed,
        dokkanBattleCatsHighscore,
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", dokkanBattleCatsScoreAverage),
        string.Format(CultureInfo.InvariantCulture, "{0:0.##}", dokkanBattleCatsScoreMedian),
        monster1AttackMultiplierHighest,
        monster1AttackMultiplierHighestRunID,
        monster1AttackMultiplierLowest,
        monster1AttackMultiplierLowestRunID,
        monster1DefenseRateHighest,
        monster1DefenseRateHighestRunID,
        monster1DefenseRateLowest,
        monster1DefenseRateLowestRunID,
        monster1SizeMultiplierHighest,
        monster1SizeMultiplierHighestRunID,
        monster1SizeMultiplierLowest,
        monster1SizeMultiplierLowestRunID,
        totalLargeMonstersHunted,
        totalSmallMonstersHunted,
        totalOverlaySessions,
        TimeSpan.FromSeconds(sessionDurationHighest).ToString(TimeFormats.HoursMinutesSecondsMilliseconds, CultureInfo.InvariantCulture),
        TimeSpan.FromSeconds(sessionDurationLowest).ToString(TimeFormats.HoursMinutesSecondsMilliseconds, CultureInfo.InvariantCulture),
        TimeSpan.FromSeconds(sessionDurationAverage).ToString(TimeFormats.HoursMinutesSecondsMilliseconds, CultureInfo.InvariantCulture),
        TimeSpan.FromSeconds(sessionDurationMedian).ToString(TimeFormats.HoursMinutesSecondsMilliseconds, CultureInfo.InvariantCulture)
        );
    }

    /// <summary>
    /// https://dorielrivalet.github.io/mhfz-ferias-english-project/
    /// </summary>
    public static string GetGameArmorSkillsPriority
    {
        get
        {
            return "Skill Priority:\n" +
                "1: SnS Tech\n" +
                "2: DS Tech\n" +
                "3: GS Tech\n" +
                "4: LS Tech\n" +
                "5: Hammer Tech\n" +
                "6: HH Tech\n" +
                "7: Lance Tech\n" +
                "8: GL Tech\n" +
                "9: Switch Axe F Tech\n" +
                "10: Tonfa Tech\n" +
                "11: Magnet Spike Tech\n" +
                "12: HBG Tech\n" +
                "13: LBG Tech\n" +
                "14: Bow Tech\n" +
                "15: Determination\n" +
                "16: Strong attack\n" +
                "17: Issen\n" +
                "18: Furious\n" +
                "19: Exploit Weakness\n" +
                "20: Dissolver\n" +
                "21: Thunder Clad\n" +
                "22: Rush\n" +
                "23: Grace\n" +
                "24: Sword God\n" +
                "25: Edgemaster\n" +
                "26: Critical Shot\n" +
                "27: Point Breakthrough\n" +
                "28: Compensation\n" +
                "29: Rapid Fire\n" +
                "30: Three Worlds\n" +
                "31: Reflect\n" +
                "32: Drawing Arts\n" +
                "33: Encourage\n" +
                "34: Steady Hand\n" +
                "35: Mounting\n" +
                "36: Gentle Shot\n" +
                "37: Spacing\n" +
                "38: Combo Expert\n" +
                "39: Shiriagari\n" +
                "40: Lone Wolf\n" +
                "41: Light Tread\n" +
                "42: Vitality\n" +
                "43: Skilled\n" +
                "44: Trained\n" +
                "45: Rage\n" +
                "46: Iron Arm\n" +
                "47: Breeder\n" +
                "48: Survivor\n" +
                "49: Relief\n" +
                "50: Hunter\n" +
                "51: Sobriety\n" +
                "52: Blast Resistance\n" +
                "53: Crystal Res\n" +
                "54: Magnetic Res\n" +
                "55: Freeze Res\n" +
                "56: Evade Distance\n" +
                "57: Charge Attack Up\n" +
                "58: Bullet Saver\n" +
                "59: Movement Speed\n" +
                "60: Reinforcement\n" +
                "61: Vampirism\n" +
                "62: Adaptation\n" +
                "63: Ice Age\n" +
                "64: Vigorous\n" +
                "65: Dark Pulse\n" +
                "66: Herbal Science\n" +
                "67: Incitement\n" +
                "68: Blazing Grace\n" +
                "69: Abnormality\n" +
                "70: Drug Knowledge\n" +
                "71: Status Assault\n" +
                "72: Stylish Assault\n" +
                "73: Stylish\n" +
                "74: Absolute Defense\n" +
                "75: Assistance\n" +
                "76: Combat Supremacy\n" +
                "77: Mindfulness\n" +
                "78: 相討ち\n" +
                "79: Weapon Handling\n" +
                "80: Elemental Attack\n" +
                "81: Stamina Recovery\n" +
                "82: Guts\n" +
                "83: Speed Setup\n" +
                "84: Status Res\n" +
                "85: Fencing\n" +
                "86: Knife Throwing\n" +
                "87: Caring\n" +
                "88: Def Lock\n" +
                "89: Para\n" +
                "90: Sleep\n" +
                "91: Stun\n" +
                "92: Poison\n" +
                "93: Deoderant\n" +
                "94: Snowball Res\n" +
                "95: Stealth\n" +
                "96: Health\n" +
                "97: Recovery Speed\n" +
                "98: Lavish Attack\n" +
                "99: Sharpness\n" +
                "100: Artisan\n" +
                "101: Expert\n" +
                "102: Crit Conversion\n" +
                "103: Ceaseless\n" +
                "104: Sharpening\n" +
                "105: Obscurity\n" +
                "106: Fortification\n" +
                "107: Guard\n" +
                "108: Auto-Guard\n" +
                "109: Throwing\n" +
                "110: Reload\n" +
                "111: Sniper\n" +
                "112: Auto-Reload\n" +
                "113: Recoil\n" +
                "114: Normal Shot Up\n" +
                "115: Pierce Shot Up\n" +
                "116: Pellet Shot Up\n" +
                "117: Normal Shot Add\n" +
                "118: Pierce Shot Add\n" +
                "119: Pellet Shot Add\n" +
                "120: Crag Shot Add\n" +
                "121: Cluster Shot Add\n" +
                "122: Status Attack\n" +
                "123: Bomb Boost\n" +
                "124: Hunger\n" +
                "125: Gluttony\n" +
                "126: Attack\n" +
                "127: Defense\n" +
                "128: Protection\n" +
                "129: Hearing Protection\n" +
                "130: Anti-Theft\n" +
                "131: Wide-Area\n" +
                "132: Backpacking\n" +
                "133: All Res Up\n" +
                "134: Fire Res\n" +
                "135: Water Res\n" +
                "136: Ice Res\n" +
                "137: Thunder Res\n" +
                "138: Dragon Res\n" +
                "139: Heat Res\n" +
                "140: Cold Res\n" +
                "141: Wind Pressure\n" +
                "142: Map\n" +
                "143: Gathering\n" +
                "144: Gathering Speed\n" +
                "145: Whim\n" +
                "146: Fate\n" +
                "147: Fishing\n" +
                "148: Psychic\n" +
                "149: Recovery\n" +
                "150: Combining\n" +
                "151: Ammo Combiner\n" +
                "152: Alchemy\n" +
                "153: Evasion Boost\n" +
                "154: Evasion\n" +
                "155: Adrenaline\n" +
                "156: Everlasting\n" +
                "157: Stamina\n" +
                "158: Loading\n" +
                "159: Precision\n" +
                "160: Monster\n" +
                "161: Eating\n" +
                "162: Carving\n" +
                "163: Terrain\n" +
                "164: Quake Res\n" +
                "165: Vocal Chords\n" +
                "166: Cooking\n" +
                "167: Gunnery\n" +
                "168: Flute Expert\n" +
                "169: Breakout\n" +
                "170: Martial Arts\n" +
                "171: Strong Arm\n" +
                "172: Inspiration\n" +
                "173: Passive\n" +
                "174: Bond\n" +
                "175: Pressure\n" +
                "176: Capture Proficiency\n" +
                "177: Poison Coating Add\n" +
                "178: Para Coating Add\n" +
                "179: Sleep Coating Add\n" +
                "180: Fire Attack\n" +
                "181: Water Attack\n" +
                "182: Thunder Attack\n" +
                "183: Ice Attack\n" +
                "184: Dragon Attack\n" +
                "185: Fasting\n" +
                "186: Bomb Sword\n" +
                "187: 強撃剣\n" +
                "188: Poison Sword\n" +
                "189: Para Sword\n" +
                "190: Sleep Sword\n" +
                "191: Fire Sword\n" +
                "192: Water Sword\n" +
                "193: Thunder Sword\n" +
                "194: Ice Sword\n" +
                "195: Dragon Sword\n" +
                "196: Focus\n\n" +
                "※Up to 10 skills (12 with G Rank Armour, 19 with Zenith Gear, 20 with Hiden Cuff, 21 with Guild Food, 22 with Fruits, 23 with Diva Skill) can be active at once, the priority of skills is shown above."
                ;
        }
    }

    public static string GetGameArmorSkillsHealthAndStamina
    {
        get
        {
            return "Health and Stamina\n\nHealth\t○\tHealth+50\t40\tMaximum Health +50\n\nHealth+40\t30\tMaximum Health +40\n\nHealth+30\t20\tMaximum Health +30\n\nHealth+20\t15\tMaximum Health +20\n\nHealth+10\t10\tMaximum Health +10\n\nHealth-10\t-10\tMaximum Health -10\n\nHealth-20\t-15\tMaximum Health -20\n\nHealth-30\t-20\tMaximum Health -30\n\nRecovery Speed\t○\tRecovery Speed+2\t20\t4x Health Recovery Speed\n\nRecovery Speed+1\t10\t3x Health Recovery Speed\n\nRecovery Speed-1\t-10\t3x Slower Health Recovery Speed\n\nRecovery Speed-2\t-20\t4x Slower Health Recovery Speed\n\nHunger\t○\tHunger Negated\t15\tStamina bar length does not decrease in length over time.\n\nHunger Halved\t10\tStamina bar length decreases at 0.5x the speed over time.\n\nHunger Up (Sm)\t-10\tStamina bar length decreases at 1.5x the speed over time\n\nHunger Up (Lg)\t-15\tStamina bar length decreases at 2.0x the speed over time\n\nRecovery\t×\tRecovery Items Up\t10\tRecovery item effect boosted 1.25x\n\nRecovery Items Down\t-10\tRecovery item effectlowered to 0.75x\n\nVampirism\t×\tVampirism+2\t20\tWhen attacking a monster, there is an 80% that your Health will recover.\n\nVampirism+1\t10\tWhen attacking a monster, there is a 60% that your Health will recover.\n\nHerbal Science\t×\tMedical Sage\t10\tInstant recovery of any Red HP when using any healing item.\n\nAdditional effects if multiple party members have the skill：\n\n2Players: recovery items apply to the entire party.\n\n3Players: recovery items apply to the entire party, +20extra HP Recovery\n\n4Players: recovery items apply to the entire party, +50extra HP Recovery\n\n※※Stacks with Recovery skill, only pure recovery items are party wide (e.g.Max Potions yes,Ancient Potions no.)\n\nStamina Recovery\t○\tStamina Recovery Up【Large】\t20\tDoubles stamina recovery speed over time\n\nStamina Rec (Small)\t10\tStamina recovery speed increases by 1.5 times over time.\n\nStamina Rec Down\t-10\tHalves stamina recovery speed over time\n\nStamina\t○\t\n\nStamina values usually decrease by 15 units\n\nPeerless\t20\tStamina decrease rate is halved (decrease is reduced to 8 units)\n\nIn addition, the reduction of stamina when evading or guarding is reduced to 50%.\n\nMarathon Runner\t10\tStamina decrease rate is halved (decrease is reduced to 8 units)\n\nIn addition, the reduction of stamina when evading or guarding is reduced to 75%.\n\nShort Sprinter\t-10\tStamina depletion speed is increased by 1.2 times (up to 18 units)\n\nEating\t×\tSpeed Eating\t10\tIncreases the speed of eating consumables such as meat and healing potions.\n\nSlow Eating\t-10\tReduces the speed of eating consumables such as meat and healing potions.\n\nGluttony\t×\tScavenger\t15\tUsing consumables restores 25 maximum stamina\n\nGourmand\t10\tStamina recovery when eating meat is increased by 25";
        }
    }

    public string GetGameArmorSkills
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            return s.ArmorSkillsInfo switch
            {
                "Priority" => GetGameArmorSkillsPriority,
                "Health and Stamina" => GetGameArmorSkillsHealthAndStamina,
                "Attack Skills" => GetGameArmorSkillsAttackSkills,
                "Defensive Skills" => GetGameArmorSkillsDefensiveSkills,
                "Blademaster Skills" => GetGameArmorSkillsBlademasterSkills,
                "Gunner Skills" => GetGameArmorSkillsGunnerSkills,
                "Resistance Skills" => GetGameArmorSkillsResistanceSkills,
                "Abnormal Status Skills" => GetGameArmorSkillsAbnormalStatusSkills,
                "Other Protection Skills" => GetGameArmorSkillsOtherProtectionSkills,
                "Item/Combo Skills" => GetGameArmorSkillsItemComboSkills,
                "Map/Detection Skills" => GetGameArmorSkillsMapDetectionSkills,
                "Gathering/Transport Skills" => GetGameArmorSkillsGatheringTransportSkills,
                "Reward Skills" => GetGameArmorSkillsRewardSkills,
                "Other Skills" => GetGameArmorSkillsOtherSkills,
                "Colors" => GetGameArmorColors,
                "Types" => GetGameArmorTypesInfo,
                _ => GetGameArmorSkillsPriority,
            };
        }
    }

    public static string GetGameArmorColors
    {
        get
        {
            return "Armour and Icon Colours\r\nIn Frontier armour colours are unlocked by doing a number of different tasks. These colours are also the colour that is displayed behind your weapon icon in the town.\r\nColours other than Green, Red and White require that you have reached at least HR3 to be used.\r\nThe menu to actually customise the colour used is unlocked by simply creating or otherwise obtaining an armour piece that lets you set its colour. The absolute easiest way to do this is to buy the T-Shirt that the NPC wearing glasses in the Blacksmith sells.\r\n\n" +
                "Colours other than Green, Red and White require that you have reached at least HR3 to be used." +
                "Colour Requirements\r\n◆ Green\r\nCompletion of a large number of Delivery Quests\r\nEach Delivery quest adds +3 points, 100 points required.\r\n◆ Red\r\nCompletion of a large number of Slaying Quests\r\nSlaying a main objective adds +2 points, Sub-quests +1, 100 points required\r\n◆ White\r\nCapturing a large number of Monsters\r\nEach capture adds +3 points, 100 points required\r\nIf your total points for Green, Red or White pass 100, the total points of all three goes down by -30.\r\n◆ Blue Have 50 Friends at once on your friendslist\r\n◆ Dark Blue Have 20 or more Rare-7 weapons or higher of each type (excluding tonfas)\r\n◆ Light Green 35x any armour pieces that are needed for Well Dressed title\r\n◆ Purple Achieve a single PVP victory\r\n◆ Orange Complete a large number of Hunter Guide Challenges\r\n◆ Yellow 15 or more Large Gold Crown Monsters\r\n◆ Grey Eat 100 times in your house\r\n◆ Pink Contribute 5000 Souls during the festival\r\n◆ Rainbow Unlock all other colours\r\n\n" +
                "Brief Unlocking Guides\r\n White,Red and Green\r\nGreen is easily earned while working on your Caravan Gem, delivering 3 iron ore grants you the same colours as killing a monster but takes only a matter of seconds. These deliveries all count towards the green colour so in the space of an hour or so you can easily finish up the colour\n\n" +
                "Red\n" +
                "Red you will almost certainly unlock naturally just by playing, it is likely to be the first colour you unlock.\n\n" +
                "White can be pretty hard to get at this point in frontier due to newer content consisting solely of monsters you cannot actually capture. The best way to go about getting these consistent is to buy 2x 捕獲珠Ｇ from the road store,these giveCapture Guru which will cause monsters to blink on the mini-map when they are able to be captured. You can also get Auto-Tracker from using 3x 洞察珠Ｇ but this is much less practical and you can simply use paintballs and psychoserums instead.\n\n" +
                "Blue\n" +
                "As the friend list is only on your end and does not produce a mutual prompt you can simply add 50 random other\r\nplayers and then delete them afterwards (Do actually delete them, or else you will spam them with messages whenever you sign in).\n\n" +
                "Dark Blue\n" +
                "For Dark Blue you have to craft 20 Rare-7 weapons of each type to unlock the Weapon Maniac titles\n" +
                "Duplicate weapons do not count, G Rank weapons do not count as being overRare-7 and Tonfas and Switch Axe titles are not considered for this\n" +
                "Pure raw SP Weapons upgrade to Rare-7 and bowguns can outright be made at Rare-7 or higher unlike most other weapons. Make whichever type you have the most materials for\r\n\n" +
                "Light Green\n" +
                "Light Green can be unlocked by making 35 of theChest Piece: 廚師服飾\n" +
                "This only needs Raw Meat and items that can be bought at the Pugi Farm store:\n" +
                "熱烤肉 x105 Buyable (Pugi Farm Store Lv3)\r\n辣椒 x210 Buy-able Herb\r\n生肉 x210 Raw Meat (Carves)\r\n調味生肉 x105Combine生肉 + 辣椒 (Raw Meat + Buyable Herb)\n\n" +
                "Purple\n" +
                "You will need to have a pet monster,to get one you need to go talk to the Blue NPC in town and choose:\n" +
                "獵人道場 > 選擇演習 > 鬥技演習\r\nThis will bring up a list ofArena quests with preset gear, you will need to do the get a pet. The quests are hunting 2Velocidromes, hunting 1 Gypceros and hunting 1 Rathian.\n" +
                "After this you can capture certain monsters as pets by simply capturing them while hosting the quest.When you get one as a pet you will be presented with a fairly obvious 'do you want to keep this monster' screen and will have to choose one of 3 slots for the monster. Dromes have incredibly high capture rates but many monsters are fairly easy to capture.\n" +
                "When you and one other have a pet simply choose 鬥技大會 on the same NPC to set up a fight. After you win one match you will get the Purple Colour.\n\n" +
                "Yellow\n" +
                "Hunt things until you get it naturally. There's so many monsters that simply trying out their normal and HC versions makes it pretty likely to unlock.\n\n" +
                "Grey / Black\n" +
                "Eat in your house a lot. There is no way to speed it up.\n\n" +
                "Pink\n" +
                "Participate in the Hunter Festival. 5000 Souls required.";
        }
    }

    public static string GetGameArmorSkillsAttackSkills
    {
        get
        {
            return "Attack Skills\n\nStrong Attack\t×\tStrong Attack+6\t50\tIncreases attack power by 200. Does not overlap with skills that increase attack power\n\nStrong Attack+5\t40\tIncreases attack power by 150. Does not overlap with skills that increase attack power\n\nStrong Attack+4\t30\tIncreases attack power by 80. Does not overlap with skills that increase attack power\n\nStrong Attack+3\t20\tIncreases attack power by 50. Does not overlap with skills that increase attack power\n\nStrong Attack+2\t15\tIncreases attack power by 35. Does not overlap with skills that increase attack power\n\nStrong Attack+1\t10\tIncreases attack power by 20. Does not overlap with skills that increase attack power\n\nAttack\t○\tAttack Up(Absolute)\t80\tAttack power increases by 50\n\nAttack Up (Very Large)\t50\tAttack power increases by 30\n\nAttack Up (Large)\t25\tAttack power increases by 20\n\nAttack Up (Medium)\t15\tAttack power increases by 10\n\nAttack Up (Small)\t10\tAttack power increases by 5\n\nLone Wolf\t×\tLone Wolf\t10\tIncreases attack power by 100 when there are no Hunters or Rastas in the same area.\n\nHalks are allowed\n\nAdrenaline\t×\t\n\nNormally, at 40 HP, attack power remains unchanged, but defense increases by 60.\n\nAdrenaline+2\t15\n\n∥\n\n20\tAttack power increases by 1.5 times (1.3 times for Bowgun) when HP reaches 40.\n\nDefense increased to 90\n\nAdrenaline+1\t10\n\n21\tWhen HP reaches 40, defense increased to 90\n\nWorry\t-15\n\n30\tWhen HP reaches 40, attack power is reduced by 0.7 times. Defense increase is reduced to 42\n\nIssen\t×\tIssen+3\t30\tIncreases crit rate by 20% and changes critical multiplier to 1.50 (from 1.25)\n\nIssen+2\t20\tIncreases crit rate by 10% and changes critical multiplier to 1.40 (from 1.25)\n\nIssen+1\t10\tIncreases crit rate by 5% and changes critical multiplier to 1.35 (from 1.25)\n\nExpert\t○\tCritical Eye+5\t50\tAffinity +50%\n\nCritical Eye+4\t35\tAffinity +40%\n\nCritical Eye+3\t20\tAffinity +30%\n\nCritical Eye+2\t15\tAffinity +20%\n\nCritical Eye+1\t10\tAffinity +10%\n\nSkilled\t×\tSkilled\t15\tGrants the effects of Speed Eater, Movement Speed+2, True Guts and Weapon Handling.\n\nTrained\t×\tTrained+2\t15\tGrants Focus+2 and Kickboxing King. Stops hits interacting with players and AI.\n\nTrained+1\t10\tGrants Focus+1 and Martial Arts. Stops hits interacting with players and AI.\n\nAbnormality\t×\tAbnormality\t15\tGrants Status Attack Up, Status Pursuit and Drug Knowledge.\n\nCrit Conversion\t×\tCrit Conversion\t10\tAffinity increases by 30%\n\nWhen Affinity Rate is 101% or higher, attack power increases by √(Excess Affinity x 7)\n\nExploit Weakness\t×\tExploit Weakness\t20\tWhen attacking a part with a value of 35 or higher, +5 to the value of the target part. Attribute damage remains the same\n\nReduce Weakness\t-10\tWhen attacking a part with a value of 35 or higher, -5 to the value of the target part. Attribute damage remains the same\n\nStylish Assault\t×\tStylish Assault\t15\tEvading through attacks increases attack by +100 true raw for a time period that varies based on weapon class.\n\nSnS,DS,Tonfa: 14 Seconds、Heavy Bowgun:24 Seconds\n\nAll Others: 19 Seconds\n\nDissolver\t×\tElemental Exploit\t15\tIncreases monsters' hitzones elemental effectiveness values by a value that varies based on weapon class when they are 20 or higher.\n\nSnS,GS, LS,Hammer,Hunting Horn, Lance,Switch Axe F,Magnet Spike: +15\n\nDual Swords,Gunlance, LightBowgun:+10\n\nTonfas,Heavy Bowgun,Bow:+5\n\nElemental Diffusion\t-10\tDecreases monsters' hitzones elemental effectiveness values -5 when they are 20 or higher.\n\nStatus Assault\t×\tStatus Pursuit\t10\tInflicts additional damage with status attacks on monsters suffering status ailments. *However, there is no effect on Raviente.\n\nDrug Knowledge\t×\tDrug Knowledge\t10\tThe attack power of weapons with status attributes is increased by 1/4 of the abnormal status value, and status damage is dealt on every hit.\n\n（※status value on weapon is set to 0.38x with 25% of the true base status）\n\nStatus Attack\t×\tStatus Attack Up\t10\tPoison,Paralysis and Sleep multiplied by 1.125x\n\nBomb Boost\t×\tBomber\t10\tBomb Strength 1.5x、Bombs Combination Rate 100%, Blast Element Damage increased.\n\nGunnery\t○\tArtillery God\t35\tBallista: Damage x1.3\n\nGunlance: Shelling Raw Attribute x1.3,Wyvern Fire Raw Attribute x1.4\n\nBowguns: Crag/Clust S Raw Attribute x1.5,HBG Fire Beam damage x1.3\n\nTonfa: Ryuuki Detonation damage x1.6\n\nArtillery Expert\t20\tBallista: Damage x1.2\n\nGunlance: Shelling Raw Attribute x1.2,Wyvern Fire Raw Attribute x1.3\n\nBowguns: Crag/Clust S Raw Attribute x1.5,HBG Fire Beam damage x1.2\n\nTonfa: Ryuuki Detonation damage x1.4\n\nGunnery\t10\tBallista: Damage x1.1\n\nGunlance: Shelling Raw Attribute x1.1,Wyvern Fire Raw Attribute x1.2\n\nBowguns: Crag/Clust S Raw Attribute x1.5,HBG Fire Beam damage x1.1\n\nTonfa: Ryuuki Detonation damage x1.25\n\nFire Attack\t○\tFire Attack (Large)\t20\tFire Attack 1.2x\n\nFire Attack (Small)\t10\tFire Attack 1.1x\n\nWater Attack\t○\tWater Attack (Large)\t20\tWater Attack 1.2x\n\nWater Attack (Small)\t10\tWater Attack 1.1x\n\nThunder Attack\t○\tThunder Attack (Large)\t20\tThunder Attack 1.2x\n\nThunder Attack (Small)\t10\tThunder Attack 1.1x\n\nIce Attack\t○\tIce Attack (Large)\t20\tIce Attack 1.2x\n\nIce Attack (Small)\t10\tIce Attack 1.1x\n\nDragon Attack\t○\tDragon Attack (Large)\t20\tDragon Attack 1.2x\n\nDragon Attack (Small)\t10\tDragon Attack 1.1x\n\nElemental Attack\t×\tElemental Attack Up\t10\tAll Elements 1.1x\n\nElemental Attack Down\t-10\tAll Elements 0.9x\n\nMartial Arts\t○\tKickboxing King\t20\tKick deals 15damage and becomes a roundhouse kick while\n\nstanding or a drop kick while moving.Tonfa kick attacks deal higherdamage as well as adding more air time to all aerial attacks.\n\nMartial Arts\t10\tKick deals 15 damage.Tonfa kick attacks deal slightly higher Damage\n\nSurvivor\t×\tTenacity\t20\tEach time the number of remaining revivals in the quest decreases,\n\n1st time: Attack power +15, Defense +100\n\n2nd time: Attack power +20, Defense +150\n\n※Does not work in Great Slaying and Caravan Quests\n\nFortify\t10\tEach time the number of remaining revivals in the quest decreases,\n\nEach time the number of remaining revivals in the quest decreases,\n\n1st time: Attack power +5, Defense +50\n\n2nd time: Attack power +10, Defense +100\n\n※Does not work in Great Slaying and Caravan Quests\n\nRage\t×\tBuchigiri\t20\tTrue Guts and Adrenaline+2\n\nWrath Awoken\t15\tTrue Guts and Adrenaline+1\n\nFasting\t×\tStarving Wolf+2\t20\tIncreases affinity by 50%, grants evade+2, and critical hit modifier to 1.35x when hungry\n\n(Active at 25 Stamina *minimum bar*)\n\nStarving Wolf+1\t10\tIncreases affinity by 50%, grants evade+1\n\nPriority is given to higher skills (Active at 25 Stamina *minimum bar*)\n\nFocus\t○\tFocus+2\t20\t0.8x Charge attack duration for GS,Hammer,Long Sword,HBG,Lance,Tonfa and Switch Axe F where appropriate.\n\n1.2x fill rate for gauges (LS Spirit, SwAxe Sword and Tonfa)\n\nHBG heat beam meter filled an extra 2 units per shot (no effect for bows)\n\nFocus+1\t10\t0.9x Charge attack duration for GS,Hammer,Long Sword,HBG,Lance,Tonfa and Switch Axe F where appropriate.\n\n1.1x fill rate for gauges (LS Spirit, SwAxe Sword and Tonfa)\n\nHBG heat beam meter filled an extra 1 unit per shot (no effect for bows)\n\nDistraction\t-10\t1.2x Charge attack duration for GS,Hammer,Long Sword,HBG,Lance,Tonfa and Switch Axe F where appropriate.\n\n0.8x fill rate for gauges (LS Spirit, SwAxe Sword and Tonfa)\n\nHBG heat beam meter loses 1 unit per shot (no effect for bows)\n\nCharge Attack Up\t×\tCharge Attack Up+2\t20\tGreatsword, long sword, hammer, lance, and tonfa damage and attribute values are further increased when charging attacks.\n\nCan be used in combination with 「Focus」\n\nCharge Attack Up+1\t10\tGreatsword, long sword, hammer, lance, and tonfa damage and attribute values are increased when charging attacks.\n\nCan be used in combination with 「Focus」\n\nWeapon Handling\t×\tWeapon Handling\t10\tWeapon draw/sheath speed increased by 20% 「Does not stack with Master Seal or G Finesse weapon effects」\n\nShiriagari\t×\tShiriagari\t15\tAttack power (multiplier) increases according to the elapsed time of the quest\n\n1 min +20, 3 min +50, 5 min +80, 10 min +130, 15 min +180, 20 min +200\n\nAdaptation\t×\tAdaptation+2\t20\tWhen HP is over 100, other attack systems are added based on hitbox values using the multipliers 0.81x for melee and 0.72x for range\n\nType of damage changes to the type with the best result. (ex. Hammers can cut tails if hitbox values for cutting are higher on tail)\n\nAdaptation+1\t10\tWhen HP is over 100, other attack systems are added based on hitbox values using the multipliers 0.72x for melee and 0.64x for range\n\nType of damage changes to the type with the best result. (ex. Hammers can cut tails if hitbox values for cutting are higher on tail)\n\nCombat Supremacy\t×\tCombat Supremacy\t10\tAttack increases by 1.2 times while weapon is unsheathed. However, stamina will always decrease while the effect is active.\n\n※Even if the stamina runs out, the effect will continue as long as the sword is not sheathed.\n\n※Effect is nullified if Rasta is activated\n\nVigorous\t×\tVigorous\t10\tWhen HP is 100 or more, attack power increases by 1.15x\n\nLavish Attack\t×\tConsumption Slayer\t10\tAttack+100 at the cost of additional sharpness loss per hit with Blademaster weapons\n\nadditional 0.2x multiplier on coatings at the cost of double consumption per shot. (no effect on bowguns)\n\nObscurity\t×\tObscurity\t10\tYour attack will increase by a set value every time you block an attack, up to 10x. Values differ per weapon.\n\nSnS,Lance,Gunlance,Tonfa: 1-5 Blocked Attacks +40, 6-10 Blocked Attacks +20,Max Buff +300.\n\nSwitch Axe F,GS,Magnet Spike: 1-5 Blocked Attacks +30, 6-10 Blocked Attacks +15,Max Buff +225\n\nLong Sword: 1-5 Blocked Attacks +20, 6-10 Blocked Attacks +10,Max Buff +150\n\nRush\t×\tRush\t10\tSuccessful attacks and guard actions gradually increase an invisible meter that has two stages.\n\nStage one: Purple flash and +50 attack, Stage two: repeate flash and +130 attack. running with weapons now also costs 0 stamina.\n\nThe effect is completely reset when you use any items or sheathe your weapon.\n\nCeaseless\t×\tCeaseless\t10\tIncreases affinity and critical multiplier as you land attacks\n\nDissapears if you stop attacking for a number of seconds.\n\nReflect and Stylish Up count towards hit totals but Fencing+2 does not.\n\nPoint Breakthrough\t×\tPoint Breakthrough\t10\tIncreases the Raw Weakness Value of a part that has been hit repeatedly by +5.\n\nThe effect has a time limit and is only applied to a single part at a time.\n\nFurious\t×\tFurious\t10\tIncrease attack, affinity, elemental and status across 3 stages as you perform attacks, evasions or guards.\n\nThunder Clad\t×\tThunder Clad\t10\tGauge is accumulated by evading, attacking, and moving.\n\nWhen the gauge reaches its maximum, \"Status Negate\", \"Movement speed up +2\", \"Weapon handling\", and \"Evasion distance up\" will be activated\n\nHowever, if a skill of the same system is activated, the skill with the highest effect takes precedence.\n\nAlso, movement speed increases for a certain period of time when the sword is drawn, and damage to the monster parts always increases.\n\nAttribute damage is unchanged\n\n※Part damage increase stacks with「Weakness Exploit」 and 「Solid Determination」\n\nDetermination\t×\tSolid Determination\t10\tAttack power rises and Affinity increases by 100%.\n\nCrit damage is also increased、「Adrenaline+2」 and 「Sharpness+1」 are also activated、\n\nDamage to the attacked parts and attribute damage is always increased.\n\nAlso, damage increases when certain bullets and arrows hit at a critical distance.\n\nDoes not stack with、「Attack」「Strong Attack」「Issen」「Exploit Weakness」「Elemental Attack」「Precision」「Critical Shot」「Expert」、\n\nIf skills of the same type are activated, the skill with the highest effect takes precedence.\n\n「Absolute Defense」「True Guts」「Great Guts」「Guts」 are disabled\n\nas well as「Guts Tickets」「Mega Guts Ticket」 and 「Soul Revival」";
        }
    }

    public static string GetGameArmorSkillsDefensiveSkills
    {
        get
        {
            return "Defensive Skills\n\nVitality\t×\tVitality+3\t30\tDefense+90,Damage Recovery Speed+2,Recovery Items Up,\n\n100% Combination success-rate for Potions,Mega Potions and Max Potions.\n\nVitality+2\t15\tDefense+45,Damage Recovery Speed+1,Recovery Items Up,\n\n100% Combination success-rate for Potions,Mega Potions and Max Potions.\n\nVitality+1\t10\tDefense+15,Damage Recovery Speed+1,Recovery Items Up,\n\n100% Combination success-rate for Potions,Mega Potions and Max Potions.\n\nVitality-1\t-10\tHealth-10,Damage Recovery Speed-1,Recovery Items Weakened.\n\nReflect\t×\tReflect+3\t20\tBlocking an attack causes a special motion to trigger from the point of guarding with an impact based MotionValue of 48.\n\nThe hit uses your current attack and sharpness values.\n\nReflect+2\t15\tBlocking an attack causes a special motion to trigger from the point of guarding with an impact based MotionValue of 36.\n\nThe hit uses your current attack and sharpness values.\n\nReflect+1\t10\tBlocking an attack causes a special motion to trigger from the point of guarding with an impact based MotionValue of 24.\n\nThe hit uses your current attack and sharpness values.\n\nDefense\t○\tDefense+120\t45\tIncreases defense by 120\n\nDefense+90\t35\tIncreases defense by 90\n\nDefense+60\t25\tIncreases defense by 60\n\nDefense+30\t15\tIncreases defense by 30\n\nDefense+20\t10\tIncreases defense by 20\n\nDefense-20\t-10\tDecrease defense by 20\n\nDefense-30\t-15\tDecrease defense by 30\n\nDefense-40\t-20\tDecrease defense by 40\n\nGuard\t○\t\n\nNo damage if not significantly attacked.\n\nWeapon type\tPower Value1～15\tPower Value16～39\tPower Value40+\n\nSnS\tSlight KB\tLarge KB\tLarge KB\n\nGS\tStand Still\tSlight KB\tLarge KB\n\nLance\tStand Still\tStand Still\tLarge KB\n\nGuard+2\t20\tMaximum blocking damage reduction. Further decreases knock-back and stamina consumption for blocking.\n\nAllows for blocking of some previously unblockable attacks\n\nIncreases Lance's shield aura hit limit to 6.\n\nGuard+1\t10\tIncreases blocking damage reduction.Decreases knock-back and stamina consumption forblocking.\n\nIncreases Lance's shield aura hit limit to 4.\n\nGuard-1\t-10\tIncreases damage taken, knockback and stamina consumption while blocking.\n\nGuard-2\t-15\tGreatly Increases damage taken, knockback and stamina consumption while blocking.\n\nAuto-Guard\t×\tAuto-Guard\t10\tAutomatically blocks any incoming attacks which can be blocked while a weapon is unsheathed.\n\nZZ Changes: No longer activates Reflect,Obscurity or Rush. Does not fill lance guard gauge.\n\nFortification\t×\tFortification+2\t15\tHybrid skill containing Guard+2, Peerless and Weapon Handling.\n\nFortification+1\t10\tHybrid skill containing Guard+1, Marathon Runner and Weapon Handling.";
        }
    }

    public static string GetGameArmorSkillsBlademasterSkills
    {
        get
        {
            return "Blademaster Skills\n\nSharpening\t○\tSharpening Artisan\t20\tSharpen at 4x original speed, gain 30 seconds of infinite sharpness and Sharpness+1 after sharpening\n\nSpeed Sharpening\t10\tSharpen with a single stroke of a whetstone.\n\nSlothful Sharpening\t-10\tSharpening duration is doubled.\n\nSharpness\t○\tRazor Sharp+2\t20\tSharpness loss is halved where applicable\n\n50% chance of any sharpness loss being completely negated.\n\nRazor Sharp+1\t10\tSharpness loss is halved where applicable\n\nBlunt Edge\t-10\tSharpness loss is doubled.\n\nStylish\t×\tStylish\t15\tRecover some sharpness when evading through attacks.Amount recovered varies per weapon type.\n\nDual Swords: 12Units.\n\nGunlance: 10 Units.\n\nSword and Shield: 8 Units.\n\nHunting Horn / Switch Axe F / Great Sword / Lance: 5 Units.\n\nLong Sword: 5 Units (Blink: 7 Units).\n\nHammer: 3 Units.\n\nTonfa: 4 Units\n\nTonfa Jump: 3 Units\n\nTonfa EX,Emergency: 10 Units\n\nTonfa DashKick: 6 Units\n\nMagnet Spike: 10 Units (Magnetism Evade: 9)\n\nEdgemaster\t×\tHoned Blade+3\t20\tAttack Up (Absolute) and Sharpness+1\n\nHaving a stronger Attack Up or Strong Attack Up skill activated replaces the Attack component of this skill.\n\nHoned Blade+2\t15\tAttack Up (Very Large) and Sharpness+1\n\nHaving a stronger Attack Up or Strong Attack Up skill activated replaces the Attack component of this skill.\n\nHoned Blade+1\t10\tAttack Up (Large) and Sharpness+1\n\nHaving a stronger Attack Up or Strong Attack Up skill activated replaces the Attack component of this skill.\n\nArtisan\t×\tSharpness+1\t10\tMaximum weapon sharpness increased by +1 level if possible.\n\nBomb Sword\t○\tBomb Sword+3\t20\tWhen the bomb sword crystal is loaded, the effect of the bomb sword [strong] is activated. * Consumes 3 times sharpness\n\nBomb Sword+2\t15\tWhen the bomb sword crystal is loaded, the effect of the bomb sword [medium] is activated. * Consumes 3 times sharpness\n\nBomb Sword+1\t10\tWhen the bomb sword crystal is loaded, the effect of the bomb sword [weak] is activated. * Consumes 3 times sharpness\n\nPoison Sword\t○\tPoison Sword+3\t20\tWhen the poison sword crystal is loaded, the effect of poison sword [strong] is activated. *Consumes double sharpness\n\nPoison Sword+2\t15\tWhen the poison sword crystal is loaded, the effect of poison sword [medium] is activated. *Consumes double sharpness\n\nPoison Sword+1\t10\tWhen the poison sword crystal is loaded, the effect of poison sword [weak] is activated. *Consumes double sharpness\n\nPara Sword\t○\tPara Sword+3\t20\tParalysis sword [strong] effect is activated when the paralysis sword crystal is loaded. *Consumes double sharpness\n\nPara Sword+2\t15\tParalysis sword [medium] effect is activated when the paralysis sword crystal is loaded. *Consumes double sharpness\n\nPara Sword+1\t10\tParalysis sword [weak] effect is activated when the paralysis sword crystal is loaded. *Consumes double sharpness\n\nSleep Sword\t○\tSleep Sword+3\t20\tWhen the sleep sword crystal is loaded, the effect of sleep sword [strong] is activated. *Consumes double sharpness\n\nSleep Sword+2\t15\tWhen the sleep sword crystal is loaded, the effect of sleep sword [medium] is activated. *Consumes double sharpness\n\nSleep Sword+1\t10\tWhen the sleep sword crystal is loaded, the effect of sleep sword [weak] is activated. *Consumes double sharpness\n\nFire Sword\t○\tFire Sword+3\t20\tWhen the flame sword crystal is loaded, the effect of the flame sword [strong] is activated. *Consumes double sharpness\n\nFire Sword+2\t15\tWhen the flame sword crystal is loaded, the effect of the flame sword [medium] is activated. *Consumes double sharpness\n\nFire Sword+1\t10\tWhen the flame sword crystal is loaded, the effect of the flame sword [weak] is activated. *Consumes double sharpness\n\nWater Sword\t○\tWater Sword+3\t20\tWhen a Water Sword Crystal is loaded, the effect of Water Sword [strong] is activated. *Consumes double sharpness\n\nWater Sword+2\t15\tWhen a Water Sword Crystal is loaded, the effect of Water Sword [medium] is activated. *Consumes double sharpness\n\nWater Sword+1\t10\tWhen a Water Sword Crystal is loaded, the effect of Water Sword [weak] is activated. *Consumes double sharpness\n\nThunder Sword\t○\tThunder Sword+3\t20\tWhen a Thunder Sword Crystal is loaded, the effect of Thunder Sword [strong] is activated. *Consumes double sharpness\n\nThunder Sword+2\t15\tWhen a Thunder Sword Crystal is loaded, the effect of Thunder Sword [medium] is activated. *Consumes double sharpness\n\nThunder Sword+1\t10\tWhen a Thunder Sword Crystal is loaded, the effect of Thunder Sword [weak] is activated. *Consumes double sharpness\n\nIce Sword\t○\tIce Sword+3\t20\tWhen an ice sword crystal is loaded, the effect of the ice sword [strong] is activated. *Consumes double sharpness\n\nIce Sword+2\t15\tWhen an ice sword crystal is loaded, the effect of the ice sword [medium] is activated. *Consumes double sharpness\n\nIce Sword+1\t10\tWhen an ice sword crystal is loaded, the effect of the ice sword [weak] is activated. *Consumes double sharpness\n\nDragon Sword\t○\tDragon Sword+3\t20\the effect of Dragon Sword [strong] is activated when a Dragon Sword Crystal is loaded. *Consumes double sharpness\n\nDragon Sword+2\t15\the effect of Dragon Sword [medium] is activated when a Dragon Sword Crystal is loaded. *Consumes double sharpness\n\nDragon Sword+1\t10\the effect of Dragon Sword [weak] is activated when a Dragon Sword Crystal is loaded. *Consumes double sharpness\n\nSnS Tech\t×\tSnS Tech【Master】\tBoth Hiden\tAdds sharpness level +1 to SnS Tech [Sword Saint]\n\nSword Saint\t30\tSuper High-Grade Earplugs\n\nAttack x1.3 when wielding a One-handed Sword.\n\nAll Elemental Sword Stone Skills+3,All Status Sword Stone Skills+2\n\nBomb Sword+2, Faster Movement with SnS unsheathed and Fencing.\n\nSnS Tech (Kaiden)\t20\tFencing and Attack 1.1x while wielding a Sword and Shield.\n\nSnS Tech (Expert)\t10\tFencing while wielding a Sword and Shield.\n\nSnS Tech (Novice)\t-10\tAttack 0.8x while wielding a Sword and Shield.\n\nDS Tech\t×\tDS Tech【Master】\tBoth Hiden\tSharpness level +1 is added to DS Tech [Dual Dragon]\n\nDual Dragon\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding Dual Blades\n\nFencing,Recover 3 units of Stamina per hit while attacking in either Demon Mode\n\nFaster activation of Demon Modes.\n\nDS Tech (Kaiden)\t20\tFencing and Attack 1.1x while wielding Dual Swords.\n\nDS Tech (Expert)\t10\tFencing while wielding Dual Swords.\n\nDS Tech (Novice)\t-10\tAttack 0.8x while wielding Dual Swords.\n\nGS Tech\t×\tGS Tech【Master】\tBoth Hiden\tSharpness level +1 is added to GS Tech [Sword King]\n\nSword King\t30\tSuper HG Earplugs,Attack 1.2x when wielding a Great Sword\n\nFencing+1.Guard slash counterblock recovers half of the sharpness that would be lost\n\nfaster charging (stacks with Focus),charge remains at level 4 longer before dropping to level 2 charge(Storm Style)\n\nPerfectly timed blocks cause no knockback, can be evaded out of\n\ndrain no stamina and cause you to recover half of the sharpness that would be lost.\n\nGS Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Great Sword.\n\nGS Tech (Expert)\t10\tFencing while wielding Great Swords.\n\nGS Tech (Novice)\t-10\tAttack 0.8x while wielding Great Swords.\n\nLS Tech\t×\tLS Tech【Master】\tBoth Hiden\tSharpness level +1 is added to LS Tech [Katana God]\n\nKatana God\t30\tSuper High-Grade Earplugs,Attack x1.2when wielding a Long Sword\n\nFencing, Spirit Bar Consumption Halved, Sharp Sword+2 while Spirit Bar is full\n\nand Attack x1.25(originally x1.15)while the Spirit Bar is glowing (after being filled completely).\n\nLS Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Long Sword.\n\nLS Tech (Expert)\t10\tFencing when wielding a Long Sword.\n\nLS Tech (Novice)\t-10\tAttack x0.8 when wielding a Long Sword.\n\nHammer Tech\t×\tHammer Tech【Master B.Beast】\tBoth Hiden\t+1 sharpness level is added to Hammer Tech [Blunt Beast]\n\nBlunt Beast\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding a Hammer\n\nFencing and Attack x1.3 when releasing a perfectly timed charge attack for that entire combo\n\n(includes infinite).\n\nHammer Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Hammer.\n\nHammer Tech (Expert)\t10\tFencing when wielding a Hammer.\n\nHammer Tech (Novice)\t-10\tAttack x0.8 when wielding a Hammer.\n\nHH Tech\t×\tHH Tech【Master】\tBoth Hiden\tSharpness level +1 is added to HH Tech [Flamboyant Emporer]\n\nFlamboyant Emperor\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding a Hunting Horn\n\nFencing,Performance mode Note Color decision is made 34% faster.\n\nHH Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Hunting Horn.\n\nHH Tech (Expert)\t10\tFencing when wielding a Hunting Horn.\n\nHH Tech (Novice)\t-10\tAttack x0.8 when wielding a Hunting Horn.\n\nLance Tech\t×\tLance Tech【Master】\tBoth Hiden\tSharpness level +1 is added to Lance Tech [Heavenly Spear]\n\nHeavenly Spear\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding a Lance,Fencing\n\n0 Damage when blocking against all attacks,Perform 4 hops instead of 3\n\nMotion value +10 for final (3rd or 4th) Normal,Diagonal and Sky-Stabs.\n\nLance Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Lance.\n\nLance Tech (Expert)\t10\tFencing when wielding a Lance.\n\nLance Tech (Novice)\t-10\tAttack x0.8 when wielding a Lance.\n\nGL Tech\t×\tGL Tech【Master】\tBoth Hiden\tSharpness level +1 is added to GL Tech [Cannon Emperor]\n\nCannon Emperor\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding a Gunlance, Fencing\n\nWyvern Fire and Heat Blade Cooldown-time is halved,Heat Blade Activation Time reduced to 3 seconds\n\nNormal Shells +2 Long Shells +1and Wide Shells +1, stackable with Load Up\n\nGL Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Gunlance.\n\nGL Tech (Expert)\t10\tFencing when wielding a Gunlance.\n\nGL Tech (Novice)\t-10\tAttack x0.8 when wielding a Gunlance.\n\nTonfa Tech\t×\tTonfa Tech【Master】\tBoth Hiden\tSharpness level +1 is added to Tonfa Tech [Piercing Phoenix]\n\nPiercing Phoenix\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding Tonfa,Fencing\n\nan additional 6th Combo / EX Meter segment is added.\n\nTonfa Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding Tonfa.\n\nTonfa Tech (Expert)\t10\tFencing when wielding Tonfa.\n\nTonfa Tech (Novice)\t-10\tAttack x0.8 when wielding Tonfa.\n\nSwitch Axe Tech\t×\tSwitch Axe Tech【Master】\tBoth Hiden\tSharpness level +1 is added to Switch Axe Tech [Edge Marshal]\n\nEdge Marshal\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding a Switch Axe and Fencing.\n\nSuccessfully using certain actions successfully increases attack by 1.05x for a short time (Morphing in Earth Style, Sword Attacks in Heaven Style and Guarding in Storm Style.)\n\nStamina consumption for infinite swing combo is halved, attacking in axe mode increases phial gauge\n\nattacks utilising the phial consume less meter.\n\nSwitch Axe Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Switch Axe.\n\nSwitch Axe Tech (Expert)\t10\tFencing when wielding a Switch Axe.\n\nSwitch Axe Tech (Novice)\t-10\tAttack x0.8 when wielding a Switch Axe.\n\nMagnet Spike Tech\t×\tMagnet Spike Tech【Master】\tBoth Hiden\tSharpness level +1 is added to Magnet Spike Tech [Magnetic Star]\n\nMagnetic Star\t30\tSuper High-Grade Earplugs,Attack x1.2 when wielding a Magnet Spike, Fencing\n\nSuccessful evasive motions boost attack (1.03x) and gauge recovery (1.5x)for 30seconds.\n\nMovement speed up (1.2x). Magnetic target marker doesn't disappear.\n\nMagnet Spike Tech (Kaiden)\t20\tAttack x1.1 and Fencing when wielding a Magnet Spike.\n\nMagnet Spike Tech (Expert)\t10\tFencing when wielding a Magnet Spike.\n\nMagnet Spike Tech (Novice)\t-10\tAttack x0.8 when wielding a Magnet Spike.\n\nFencing\t○\tFencing+2\t20\tGrants ESP (no bouncing) as well as a 2nd hit for each attack.The 2nd hit has has 20% the value of the first\n\nthis includes Raw, Element, Status and Sword Crystals.\n\nEven if you hit twice Sharpness and Sword Crystals will only be decreased as if you had hit once.\n\nFencing+1\t10\tGrants ESP (no bouncing) when attacking.\n\nSword God\t×\tSword God+3\t20\tSharpness+1, Razor Sharp+2, Fencing+2 and Sharpening Artisan in one skill.\n\nSword God+2\t10\tSharpness+1,Razor Sharp+2, Fencing+2in one skill.\n\nSword God+1\t10\tSharpness+1,Razor Sharp+1, Fencing+1in one skill.";
        }
    }

    public static string GetGameArmorSkillsGunnerSkills
    {
        get
        {
            return "Gunner Skills\n\nSteady Hand\t×\tSteady Hand+2\t20\tNormal/Rapid Up,Pierce/Pierce Up,Pellet/Spread Up and +5 to weakness value with critical distance.\n\nDoes not stack with the individual up skills, determination, precision, sniper or critical shot.\n\nSteady Hand+1\t10\tNormal/Rapid Up,Pierce/Pierce Up and Pellet/Spread Up. Does not stack with the individual skill versions.\n\nGentle Shot\t×\tGentle Shot+3\t30\tLoad Up & Recoil Reduction+3.\n\nGentle Shot+2\t15\tLoad Up & Recoil Reduction+2.\n\nGentle Shot+1\t10\tLoad Up & Recoil Reduction+1.\n\nNormal Shot Up\t×\tNormal/Rapid Up\t10\tNormal S and Rapid Bow Arrows do x1.1damage.\n\nPierce Shot Up\t×\tPierce/Pierce Up\t10\tPierce Shots and Pierce Bow Arrows do x1.1damage.\n\nPellet Shot Up\t×\tPellet/Spread Up\t10\tPellet Shots and Scatter Bow Arrows do x1.3damage.\n\nNormal Shot Add\t×\tNormal S All\t15\tGrants the ability to use all Normal S ammo.\n\nNormal S Lv1\t10\tLv1 Normal bullets can be used\n\nPierce Shot Add\t×\tPierce S All\t20\tGrants the ability to use all Pierce S ammo.\n\nPierce S Lv1&2 Add\t15\tPierce S Lv1&2 bullets can be used\n\nPierce S Lv1 Add\t10\tPierce S Lv1 bullets can be used\n\nPellet Shot Add\t×\tPierce S All\t20\tGrants the ability to use all Pellet S ammo.\n\nPellet S LV1&2 Add\t15\tPellet S Lv1&2 bullets can be used\n\nPellet S LV1 Add\t10\tPellet S Lv1 bullets can be used\n\nCrag Shot Add\t×\tCrag S All\t20\tGrants the ability to use all Crag S ammo.\n\nCrag S Lv1&Lv2 Add\t15\tCrag S Lv1&2 bullets can be used\n\nCrag S Lv1 Add\t10\tCrag S Lv1 bullets can be used\n\nCluster Shot Add\t×\tCluster S All\t20\tGrants the ability to use all Cluster S ammo.\n\nCluster S Lv1&Lv2 Add\t15\tCluster S Lv1&2 bullets can be used\n\nCluster S Lv1 Add\t10\tCluster S Lv1 bullets can be used\n\nLoading\t×\tLoadingＵＰ\t10\t+1 capacity for Bowgun and Gunlance ammo.Extra charge level for Bows.\n\nReload\t○\tReload Speed+3\t20\tBowguns: Reload Speed increases by 3 levels.\n\nBows: Coatings are automatically loaded when they are selected.\n\nReload Speed+2\t15\tBowguns: Reload Speed increases by 2 levels.\n\nBows: Coating loading time is 75% of default time.\n\nReload Speed+1\t10\tBowguns: Reload Speed increases by 1 level.\n\nBows: Coating loading time is 85% of default time.\n\nReload Speed-1\t-10\tBowguns: Reload Speed decreases by 1 level.\n\nBows: Coating loading time is 110% of default time.\n\nRecoil\t○\tRecoil Reduction+3\t30\tBowguns: Recoil is reduced by 6 levels.\n\nGunlance: Can evade after Shelling and Recoil from Wyvern Fire is reduced by 30 frames.\n\nRecoil Reduction+2\t15\tBowguns: Recoil is reduced by 4 levels.\n\nGunlance: Can evade after Shelling and Recoil from Wyvern Fire is reduced by 30 frames.\n\nRecoil Reduction+1\t10\tBowguns: Recoil is reduced by 2 levels.\n\nGunlance: Can evade after Shelling.\n\nCritical Shot\t×\tCritical Shot+3\t20\tAttack Up (Absolute)(True Raw+50) and Sniper (Less shot deviation, +5 to a body part's weakness when hit by a Normal/Pierce/Crag Shot or Bow Attacks at a properly spaced critical distance).\n\nDoes not stack with other Attack skills or Sniper (e.g.Attack Up portion would be overwritten by Strong Attack+4).\n\nStacks with Thunderclad,Acid Shots,HHSonicBomb Debuff and Point Breakthrough with the resulting value being used for Exploit Weakness.\n\nCritical Shot+2\t15\tAttack Up (Very Large)(True Raw+30) and Sniper (Less shot deviation, +5 to a body part's weakness when hit by a Normal/Pierce/Crag Shot or Bow Attacks at a properly spaced critical distance).\n\nDoes not stack with other Attack skills or Sniper (e.g.Attack Up portion would be overwritten by Strong Attack+4).\n\nStacks with Thunderclad,Acid Shots,HHSonicBomb Debuff and Point Breakthrough with the resulting value being used for Exploit Weakness.\n\nCritical Shot+1\t10\tAttack Up (Large)(True Raw+20) and Sniper (Less shot deviation, +5 to a body part's weakness when hit by a Normal/Pierce/Crag Shot or Bow Attacks at a properly spaced critical distance).\n\nDoes not stack with other Attack skills or Sniper (e.g.Attack Up portion would be overwritten by Strong Attack+4).\n\nStacks with Thunderclad,Acid Shots,HHSonicBomb Debuff and Point Breakthrough with the resulting value being used for Exploit Weakness.\n\nRapid Fire\t×\tRapid Fire\t15\tRapid Fire volleys fire one extra round, when loading bullets that can be rapid fired you will load all bullets.(Doesn't apply to Ultra Rapid Fire).\n\nAuto-Reload\t×\tAuto-Reload\t10\tBowguns: Can shoot without having to reload but receive high recoil for all shots.\n\nBows: Charging time is reduced to 85% of the default speed.\n\nAmmo Combiner\t×\tMaximum Bullets\t10\tWhen combining bullets or coatings you will always get the maximum possible back.\n\nBullet Saver\t×\tSaving Expert\t20\tWhen you fire a shot there is a 11/32(34.3%) chance that it will notconsume a shot or coating\n\nProcesses all bullets individually on compression shots (HBG only, any number from zero to all loaded shots can be saved.)\n\nbut does not work with Ultra Rapid Fire (LBG) or Heat Beams (HBG).\n\nSaving Master\t10\tWhen you fire a shot there is a 7/32(21.8%) chance that it will not consume a shot or coating\n\nAlso works with compression (HBG). Does not work with Ultra Rapid Fire (LBG) and Heat Beam (HBG).\n\nPrecision\t○\tPrecision+2\t20\tShot deviation (bullet drift) is decreased. and adds +5 to a bodypart's weakness (i.e. 35>40)\n\nwhen shooting with Normal/Pierce/Crag Shot or Bow Attacks within Critical Distance.\n\nDeviation Down\t10\tShot deviation (bullet drift) is decreased.\n\nDeviation Up\t-10\tShot deviation (bullet drift) is increased.\n\nPoison Coating Add\t×\tPoison Coating Add\t10\tGrants the ability to use Poison Coatings\n\nPara Coating Add\t×\tPara Coating Add\t10\tGrants the ability to use Para Coatings\n\nSleep Coating Add\t×\tSleep Coating Add\t10\tGrants the ability to use Sleep Coatings\n\nMounting\t×\tMounting+3\t30\tLoad Up & Reload Speed+3.\n\nMounting+2\t20\tLoad Up & Reload Speed+2.\n\nMounting+1\t10\tLoad Up & Reload Speed+1.\n\nSniper\t×\tSniper\t10\tAuto-Reload and +5 to weakness value within critical distance. *Should only be used with Bows.*\n\nSpacing\t×\tSpacing\t10\tAlters critical distance for Normal,Pierce,Pellet and Blunt shots to be lower and further increases damage at critical distance.\n\nIf you consistently attack within this critical distance you gain Movement Speed+2 and Evade Distance Up.\n\nLBG and HBG get increased perfect shot(3 Segments) and compression windows (2 segments).Bowgets lowered charge times that stack with Auto-Reload.\n\nDoes not buff attacks without Critical Distance, is added to the buff for the first half of HBG's Critical Distance.\n\nBuff builds an public meter that fills in much the same way as Thunder Clad and total hits needed will vary with Attack, Affinity, Shot Type, etc. but meter build up is not reset on being launched etc.\n\nHBG Tech\t×\tHBG Tech【Master】\tBoth Hiden\tHBG Tech [Gun Sage]'s attack power increases by 1.4x\n\nGun Sage\t30\tSuper HG Earplugs, Attack x1.3 when wielding a Heavy Bowgun, Power (value) of Element/Status Ammo x1.2,Evade Distance Up\n\nMelee Attacks and Crag/Clust Shots all do 15 KO Damage,Heat Beam does x1.2damage and Perfectly-Timed Compression will result in more Attack Power for that 1 salvo.\n\nHBG Tech (Kaiden)\t20\tAttack x1.1 and Evade Extender when wielding a Heavy Bowgun.\n\nHBG Tech (Expert)\t10\tEvade Extender when wielding a Heavy Bowgun.\n\nHBG Tech (Novice)\t-10\tAttack power x0.8 when wielding a Heavy Bowgun.\n\nLBG Tech\t×\tLBG Tech【Master】\tBoth Hiden\tIncreases attack power of LBG Tech [Gun Prodigy] by 1.4x\n\nGun Prodigy\t30\tSuper HG Earplugs,Attack 1.3 when wielding a Light Bowgun, Probability of Ammo bouncing off Monsters is reduced\n\ncan consume items while the weapon is unsheathed,Perfect Shot added to Just Shot meter that deals additional damage.\n\nLBG Tech (Kaiden)\t20\tAttack x1.1 and the probability of Ammo bouncing off Monsters is reduced when wielding a Light Bowgun.\n\nLBG Tech (Expert)\t10\tThe probability of Ammo bouncing off Monsters is reduced when wielding a Light Bowgun.\n\nLBG Tech (Novice)\t-10\tAttack 0.8x while wielding a Light Bowgun.\n\nBow Tech\t×\tBow Tech【Master】\tBoth Hiden\tBow Tech[Bow Demon]'s attack power increases by 1.4x\n\nBow Demon\t30\tSuper HG Earplugs,Attack x1.3 when wielding a Bow, Arrows cannot be deflected, PowerCoating Modifier increases to x1.6 for normal Bows and to x1.7for Gou (and upgrades)\n\nEvolution (Raviente) and G-Rank Bows and the Arc-Shot can be executed at Charge Lv2.\n\nBow Tech (Kaiden)\t20\tAttack x1.1 and arrows are no longer deflected when wielding a Bow.\n\nBow Tech (Expert)\t10\tArrows are no longer deflected when wielding a Bow.\n\nBow Tech (Novice)\t-10\tAttack 0.8x while wielding a Bow.";
        }
    }

    public static string GetGameArmorSkillsResistanceSkills
    {
        get
        {
            return "Resistance Skills\n\nAll Res Up\t×\tAll Res+20\t20\tAll resistance values increase by 20\n\nAll Res Up+10\t15\tAll resistance values increase by 10\n\nAll Res Up+5\t10\tAll resistance values increase by 5\n\nAll Res-5\t-10\tAll resistance values decrease by 5\n\nAll Res-10\t-15\tAll resistance values decrease by 10\n\nAll Res-20\t-20\tAll resistance values decrease by 20\n\nFire Res\t○\tFire Res+30\t20\tFire res values increase by 30\n\nFire Res+20\t15\tFire res values increase by 20\n\nFire Res+10\t10\tFire res values increase by 10\n\nFire Res-10\t-10\tFire res values decrease by 10\n\nFire Res-20\t-15\tFire res values decrease by 20\n\nFire Res-30\t-20\tFire res values decrease by 30\n\nWater Res\t○\tWater Res+30\t20\tWater res values increase by 30\n\nWater Res+20\t15\tWater res values increase by 20\n\nWater Res+10\t10\tWater res values increase by 10\n\nWater Res-10\t-10\tWater res values decrease by 10\n\nWater Res-20\t-15\tWater res values decrease by 20\n\nWater Res-30\t-20\tWater res values decrease by 30\n\nIce Res\t○\tIce Res+30\t20\tIce res values increase by 30\n\nIce Res+20\t15\tIce res values increase by 20\n\nIce Res+10\t10\tIce res values increase by 10\n\nIce Res-10\t-10\tIce res values decrease by 10\n\nIce Res-20\t-15\tIce res values decrease by 20\n\nIce Res-30\t-20\tIce res values decrease by 30\n\nThunder Res\t○\tThunder Res+30\t20\tThunder res values increase by 30\n\nThunder Res+20\t15\tThunder res values increase by 20\n\nThunder Res+10\t10\tThunder res values increase by 10\n\nThunder Res-10\t-10\tThunder res values decrease by 10\n\nThunder Res-20\t-15\tThunder res values decrease by 20\n\nThunder Res-30\t-20\tThunder res values decrease by 30\n\nDragon Res\t○\tDragon Res+30\t20\tDragon res values increase by 30\n\nDragon Res+20\t15\tDragon res values increase by 20\n\nDragon Res+10\t10\tDragon res values increase by 10\n\nDragon Res-10\t-10\tDragon res values decrease by 10\n\nDragon Res-20\t-15\tDragon res values decrease by 20\n\nDragon Res-30\t-20\tDragon res values decrease by 30";
        }
    }

    public static string GetGameArmorSkillsAbnormalStatusSkills
    {
        get
        {
            return "Abnormal Status Skills\n\nPoison\t○\tNegate Poison\t20\tImmunity to Poison.\n\nPoison Halved\t10\tPoison duration is halved.\n\nDouble Poison\t-10\tPoison duration is doubled.\n\nPara\t○\tNegate Para\t20\tImmunity to Paralysis\n\nPara Halved\t10\tParalysis duration is halved.\n\nPara Doubled\t-10\tParalysis duration is doubled.\n\nSleep\t○\tNegate Sleep\t20\tImmunity to Sleep.\n\nSleep Halved\t10\tSleep duration is halved.\n\nSleep Doubled\t-10\tSleep duration is doubled.\n\nStatus Res\t○\tS. Immunity (Myriad)\t30\tGrants immunity to Poison, Paralysis, Sleep, Stench, Snowman, Chat Disabled, Defense Down, Drunk, Magnetism, Crystallization and Blast.\n\nStatus Immunity\t20\tGrants immunity to Poison,Paralysis and Sleep.\n\nStatus Halved\t10\tHalves Poison,Paralysis and Sleep duration.\n\nStatus Doubled\t-10\tDoubles Poison,Paralysis and Sleep duration.\n\nStun\t○\tNegate Stun\t20\tImmunity to Stun.\n\nStun Halved\t10\tStun duration is halved.\n\nStun Doubled\t-10\tStun duration is doubled.\n\nDeoderant\t×\tDeoderant\t10\tPrevents Stench\n\nSnowball Res\t×\tSnowball Res\t10\tGrants immunity to Snowman.\n\nVocal Chords\t○\tVocal Chord Immunity\t15\tGrants immunity to Chat Disable and Fatigue. Exclusive to Chameleos.\n\nVocal Chord Halved\t10\tHalves chat Disable and Fatigue duration.\n\nDef Lock\t×\tDef Lock\t10\tGrants immunity to Defense Down\n\nSobriety\t×\tHeavy Drinker\t10\tGrants immunity to Drunk status\n\nDrunkard\t-10\tDoubles Drunk duration\n\nBlast Resistance\t×\tBlast Resistance\t10\tGrants immunity to Blast blight.\n\nMagnetic Res\t×\tMagnetic Res\t10\tGrants immunity to Magnetism\n\nMagnet Vulnerability\t-10\tDoubles Magnetism duration\n\nCrystal Res\t×\tCrystal Res\t10\tGrants immunity to Crystallization\n\nCrystal Vulnerability\t-10\tIt takes longer to recover from crystal state.\n\nAlso, crystals are more likely to explode\n\nFreeze Res\t×\tFreeze Resistance\t10\tGrants immunity to Freezing (Toa Tesukatora.)";
        }
    }

    public static string GetGameArmorSkillsOtherProtectionSkills
    {
        get
        {
            return "Other Protection Skills\n\nThree Worlds\t×\tUnaffected+3\t20\tSuper High-Grade Earplugs, Violent Wind Breaker and Quake Resistance+2.\n\nUnaffected+2\t15\tHigh-Grade Earplugs,Dragon Wind Breaker and Quake Resistance+1.\n\nUnaffected+1\t10\tEarplugs,Wind Resistance (Large) and Quake Resistance+1.\n\nHearing Res\t○\tSuper HG Earplugs\t25\tProtects against all Monster roars\n\nHigh-Grade Earplugs\t15\tProtects against second tier monster roars\n\nEarplugs\t10\tProtects against first tier monster roars\n\nLowers the length of the flinching duration for second tier roars that aren't blocked.\n\nWind Pressure\t○\tViolent Wind Breaker\t30\tProtects against Violent Wind\n\nDragon Wind Breaker\t20\tProtects against Dragon Wind\n\nWind Res (Large)\t15\tProtects against Large Wind\n\nWind Res (Small)\t10\tProtects against Small Wind\n\nQuake Res\t○\tQuake Res+2\t25\tProtects against large quakes\n\nQuake Res+1\t15\tProtects against small quakes\n\nEvasion\t○\t\n\nInvincible time is usually 6/30 seconds\n\nEvasion+2\t20\tEvasion invulnerability is increased to 12/30 seconds\n\nEvasion+1\t10\tEvasion invulnerability is increased to 10/30 seconds\n\nEvade Distance\t×\tEvade Distance Up\t20\tExtends movement distance for evasion and steps.\n\nEvasion Boost\t×\tEvasion Boost\t15\tGrants the effects of Evasion+2 and Evade Distance Up.\n\nHeat Res\t○\t\n\nNormally, the health gauge will decrease every 2 seconds in the volcano and every 3 seconds in the desert.\n\nSummer Person\t25\tHeat Cancel and Damage Recovery Speed+1 in Hot areas.\n\nHeat Cancel\t15\tGrants immunity to heat(Health loss in Hot areas).\n\nHeat Halved\t10\tThe speed at which Health is lost in Hot areas is halved.\n\nHeat Surge (Small)\t-10\tHealth loss is increased to 1 unit per 1.3 seconds in the Volcano and 1 unit per 2 seconds in the Desert\n\nHeat Surge (Large)\t-20\tHealth loss is increased to 1 unit per 1 second in the Volcano and 1 unit per 1.5 seconds in the Desert\n\nCold Res\t○\t\n\nStamina usually decreases every 6 minutes\n\nWinter General\t25\tCold Cancel, Frost bite Immunity and Marathon Runner effect while in cold areas.\n\nCold Cancel\t15\tGrants immunity to cold (faster stamina loss)\n\nCold Halved\t10\tThe speed at which stamina is lost(hunger)in Cold areas is halved.\n\nCold Surge (Small)\t-10\tThe speed at which stamina is lost in Cold areas is increased x1.5\n\nCold Surge (Large)\t-20\tThe speed at which stamina is lost in Cold areas is increased x2\n\nLight Tread\t×\tLight Tread\t10\tImmunity to monster's Pitfall Traps.\n\nAnti-Theft\t×\tAnti-Theft\t10\tPrevents items being stolen.\n\nTerrain\t○\t\n\nNormally you take a certain amount of damage every 8/30 seconds\n\nHazard Res (Large)\t15\tHealth Loss from Terrain Damage (Lava) and Heat Auras is reduced to 1/3speed.\n\nHazard Res (Small)\t10\tHealth Loss from Terrain Damage (Lava) and Heat Auras is reduced to 2/3speed.\n\nHazard Prone (Small)\t-10\tHealth Loss from Terrain Damage (Lava) and Heat Auras is increased to x1.5 speed.\n\nHazard Prone (Large)\t-15\tHealth Loss from Terrain Damage (Lava) and Heat Auras is increased to x2 speed.\n\nProtection\t○\tGoddess' Embrace\t20\t1/4 chance to receive 0 damage from any attack.\n\nDivine Protection\t10\t1/8 chance to receive 0 damage from any attack.\n\nDemonic Protection\t-10\t1/16 chance to die instantly from any attack.\n\nDeath God's Embrace\t-20\t1/8 chance to die instantly from any attack.\n\nPassive\t×\tPassive\t10\tInvincibility time after getting up from an attack is lengthened 3x.\n\nBreakout\t×\tBreakout\t10\tPanic run speed is increased when below 20% health.\n\nGuts\t○\tTrue Guts\t30\tWhen Health and Stamina are 50 or higher, an otherwise lethal attack will be survived with 1 Health Point left.\n\nGreat Guts\t20\tWhen Health is 70 or higher and Stamina is 50 or higher, an otherwise lethal attack will be survived with 1 Health Point left.\n\nGuts\t10\tWhen Health is 90 or higher and Stamina is 50 or higher, an otherwise lethal attack will be survived with 1 Health Point left.\n\nAbsolute Defense\t×\tAbsolute Defense\t20\tAdds a shield aura that completely negates hits, including all damage and status effects that the hit would cause.\n\nShield vanishes for a set duration after each hit blocked, with the duration before recharging taking longer with each hit taken.\n\nReduces physical damage to around 0.8x while the shield is down";
        }
    }

    public static string GetGameArmorSkillsItemComboSkills
    {
        get
        {
            return "Item/Combo Skills\n\nCombo Expert\t×\tCombo Expert+3\t20\tCombo Rate +30% and the effects of Bullet Combination Expert and Health Recovery Items Improved.\n\nCombo Expert+2\t15\tCombo Rate +20% and the effect of Bullet Combination Expert\n\nCombo Expert+1\t10\tCombo Rate +10% and the effect of Bullet Combination Expert\n\nCombo Expert-1\t-10\tCombo Rate -5%\n\nWide-Area\t○\t\n\nEffective when using herbs, healing potions, antidotes, power seeds, armor seeds, flinch-free fruit\n\nWide-Area+3\t30\tMega Potions, Blight Cure Fruits, Zenith Espinas Antitoxin and Crimson Raviente Blood affect allies in the same area\n\nas well as the items covered in Wide-Area+2.\n\nWide-Area+2\t20\tHerbs, Potions, Antidotes, Cool Drinks, Hot Drinks, Armor Seed and Power Seed affects allies in the same area.\n\nWide-Area+1\t10\tHerbs, Potions, Antidotes, Cool Drinks, Hot Drinks, Armor Seed and Power Seed affects allies in the same area\n\nwith 50% effectiveness.\n\nWide-Area-1\t-10\tThe Player cannot be healed through Wide-Range Recovery\n\nEverlasting\t×\tItem Duration Up\t10\tItem effect duration is increased to x1.5\n\nItem Duration DOWN\t-10\tItem effect duration is decreased x0.67.\n\nWhim\t○\t\n\nProbability of item breaking\n\nOld Pickaxe：1/3\u3000Iron Pickaxe：1/10\u3000Mega Pickaxe：1/15\u3000Pickaxe G：1/17\n\nOld Bugnet\u3000：1/3\u3000Bugnet\u3000：1/10\u3000Mega Bugnet\u3000：1/15\u3000Bugnet G：1/17\n\nHealth Flute：1/12\u3000Antidote Flute：1/12\u3000Demon Flute：1/ 8\u3000Armour Flute：1/8\u3000Flute：1/5\n\nShot Flute：1/16\u3000Assault Flute：1/16\u3000Tail Flute：1/12\u3000Deadly Flute：1/8\n\nSpecial Flute：1/16\u3000Rage Flute：1/16\u3000Wrath Flute：1/ 8\n\nDivine Whim\t15\tDecreases chance of breaking Pickaxes,Bug Nets,Horns, and Boomerangs by 50%.\n\nSpirit's Whim\t10\tDecreases chance of breaking Pickaxes,Bug Nets,Horns, and Boomerangs by 25%.\n\nSpectre's Whim\t-10\tIncreases chance of breaking Pickaxes,Bug Nets,Horns, and Boomerangs by 25%.\n\nDevil's Whim\t-15\tIncreases chance of breaking Pickaxes,Bug Nets,Horns, and Boomerangs by 50%.\n\nHunter\t×\tHunter Valhalla\t20\tAlways shows the map and any large monsters location.Easier to fish and BBQ meat.\n\nHunter Life\t10\tAlways shows the map.Easier to fish and BBQ meat.\n\nStrong Arm\t○\tStrong Arm+2\t20\tThrown items do x1.3damage.\n\nStrong Arm+1\t10\tThrown items do x1.1damage.\n\nThrowing\t×\tThrowing Distance UP\t10\tThrowing Distance of Throwing Items is increased and the probability of losing Boomerangs is decreased to 1/8.\n\nCooking\t○\tBBQ Master\t15\tFish and Meat remain Brown (ready)for much longer.\n\nBBQ Expert\t10\tFish and Meat remain Brown (ready)longer.\n\nFalse BBQ Expert\t-10\tFish and Meat remain Brown (ready)for half the time of the usual.\n\nFishing\t×\tFishing Expert\t10\tFish always bite on their first attempt.\n\nCombining\t○\tCombining+30%\t20\tCombo Rate +30%\n\nCombination+15%\t15\tCombo Rate +15%\n\nCombination+10%\t10\tCombo Rate +10%\n\nCombination-5%\t-10\tCombo Rate -5%\n\nAlchemy\t×\tAlchemy\t10\tGrants Alchemy Combinations (alternative item combinations).\n\nEncourage\t×\tEncourage+2\t20\tHorn Maestro.Party wide Evasion+2 and Stun Negate.\n\nEncourage+1\t10\tHorn Maestro.Party wide Evasion+1 and Stun Halved.\n\nFlute Expert\t×\tFlute Expert\t10\t50% less chance of horns breaking. All horn duration 1.5x (including Hunting Horn songs).\n\nSpeed Setup\t○\tTrap Master\t20\tTrap Setup Speed is lowered to x0.66. 100% combine rate for traps, Mocha Pots and Trap-related items.\n\nTrap Expert\t10\tTrap Setup Speed is lowered to x0.8. 100% combine rate for traps, Mocha Pots and Trap-related items.\n\nIron Arm\t×\tIron Arm+2\t20\tThrowing Distance Up, Strong Arm+2,Throwing Knives+2. Increases blocking duration on Great Sword Guard Slash (Heaven and Storm Styles)\n\nLance Shield Rush (Storm and Extreme Styles), and Tonfa's Standard 1, Standard 3, Standard 4,Aerial 1-3, Continuous Thrust 2 and Dash Tonfa Rotatation.\n\nIron Arm+1\t10\tThrowing Distance Up, Strong Arm+1,Throwing Knives+1. Increases blocking frames duration on Great Sword Guard Slash (Heaven and Storm Styles)\n\nLance Shield Rush (Storm and Extreme Styles), and Tonfa's Standard 1, Standard 3, Standard 4,Aerial 1-3, Continuous Thrust 2 and Dash Tonfa Rotatation.\n\nKnife Throwing\t○\tThrowing Knives+2\t20\tThrow 5K nives at once instead of 1. Only consumes 1.\n\nThrowing Knives+1\t10\tThrow 3 Knives at once instead of 1. Only consumes 1.";
        }
    }

    public static string GetGameArmorSkillsMapDetectionSkills
    {
        get
        {
            return "Map/Detection Skills\n\nMap\t×\tMap\t10\tAlways display the map.\n\nPsychic\t○\tAutotracker\t15\tMonster locations and detailed icon are always shown on the map.\n\nDetect\t10\tMarked monsters are shown as a detailed icon rather than a round dot on the map.\n\nStealth\t×\tSneak\t10\tMonsters are less likely to target you.\n\nTaunt\t-10\tMonsters are more likely to target you.\n\nIncitement\t×\tIncitement\t10\tAttacking a monsterwill force its attention towards you rather than your fellowHunters.Damage received from the monster will also be reduced during this time.The yellow eye icon indicating you were spotted will turn Red when the skill is in effect.\n\nGoing outside of the monster's range for too long will undo the effect prematurely,the skill has a 30 second cooldown before it can be activated again.\n\nWhile being targetted you gain +40 True Raw.";
        }
    }

    public static string GetGameArmorSkillsGatheringTransportSkills
    {
        get
        {
            return "Gathering/Transport Skills\n\nBackpacking\t×\tPro Transporter\t10\tIncreases speed when carrying heavy items such as eggs, powderstone, soothstone, etc.\n\nAlso be able to fall slightly higher altitudes without dropping the item.\n\nGathering Speed\t×\tHigh Speed Gathering\t10\tIncreases gathering speed.\n\nGathering\t○\t\n\nNormally, the probability of gathering after the second time is 27/32\n\nGathering+2\t15\tIncreases the chance of collecting and excavating from the second time onwards to 31/32.\n\nGathering+1\t10\tIncreases the chance of collecting and excavating from the second time onwards to 29/32.\n\nGathering-1\t-10\tIncreases the chance of collecting and excavating from the second time onwards to 23/32.\n\nGathering-2\t-15\tIncreases the chance of collecting and excavating from the second time onwards to 19/32.\n\nCarving\t×\tCarving Expert\t15\tNumberof carves increased by 1\n\nMindfulness\t○\tImperturbable\t15\tWhile using an item or gathering, the Hunter can't be interrupted by any attack.\n\nFully Prepared\t10\tWhile using an item or gathering, the Hunter can't be interrupted by other hunters or small monsters.\n\nNegligence\t-10\tDamage taken from monsters is increased when taking a hit while using an item or gathering.";
        }
    }

    public static string GetGameArmorSkillsRewardSkills
    {
        get
        {
            return "Reward Skills\n\nFate\t○\t\n\nUsually, the probability of increasing the number of slots is 22/32\n\nGreat Luck\t20\tGreatly increases chance for standard quest rewards. 29/32.\n\nGood Luck\t10\tIncreases chance for standard quest rewards 26/32.\n\nBad Luck\t-10\tDecreases chance for standard quest rewards 16/32.\n\nHorrible Luck\t-20\tGreatly Decreases chance for standard quest rewards. 8/32.\n\nMonster\t×\tCome on Big Guy!\t10\tIncreases the chances of larger boss monster spawns.?\n\nPressure\t○\tPressure【Large】\t20\tMonster Bounty randomly increases by 30, 50, 75, 100 or 150%.\n\nPressure (Small)\t10\tMonster Bounty increases by 30%.";
        }
    }

    public static string GetGameArmorSkillsOtherSkills
    {
        get
        {
            return "Other Skills\n\nBreeder\t×\tBreeder\t10\tPoogie item drops at the Pugi Farm become more common. Halks also drop items more often.\n\nBond\t×\tBond\t10\tWhen another Hunter of the opposite gender is present in a quest:\n\nMale Player: Attack +5.\n\nFemale Player: Defense +40.\n\nInspiration\t×\tInspiration\t10\tVarious effects are triggered at random when starting a quest.\n\nSkill\tMessage\tChance\n\nAll Res＋５\t各耐性に強くなった！\t30%\n\nHeat & Cold Res\t気候の変化に強くなった！\t16%\n\nNegate Stun\t気絶しなくなった！\t16%\n\nTaunt\tﾓﾝｽﾀｰに狙われやすくなった…\t15%\n\nSpeed Eating\t早食いになった！\t12%\n\nAll Res-20\t各耐性に弱くなった…\t\u30007%\n\nStatus Immunity\t状態異常に強くなった！\t\u30004%\n\nRelief\t×\tRelief\t10\tRastas, Fostas, Partners and Halks recover 50% faster after disappearing.\n\nLegendary Rastas do not leave and thus are not affected.\n\nCapture Proficiency\t○\tCapture Guru\t20\tMonsters will blink on the map when they can be captured.\n\nCapture Proficiency\t10\tChance of a captured Monster becoming a pet increases by 30%.\n\nCaring\t×\tCaring+3\t45\tAll Quests: Human Players cannot be interrupted by your attacks nor can they interrupt you.\n\nAll Quests: NPCs cannot be interrupted by Human Players and will roll when hit instead.\n\nCaring+2\t25\tNon-G Rank Quests: Human Players cannot be interrupted by your attacks nor can they interrupt you.\n\nAll Quests: NPCs cannot be interrupted by Human Players and will roll when hitinstead.\n\nCaring+1\t10\tAll Quests: NPCs cannot be interrupted by Human Players and will roll when hit instead.\n\nMovement Speed\t×\tMovement Speed ＵＰ+2\t20\tMovement speed with weapon sheathed is increased slightly further.\n\nMovement Speed ＵＰ+1\t10\tMovement speed with weapon sheathed is increased slightly.\n\nReinforcement\t×\tRed Soul\t10\tOwn Attack +15. Hitting another player will increase their Attack +30.\n\n※Hitting a player who is under the effect of Blue Soul\n\n※When hit by a player who has the Blue Soul skill you will be able to KO monsters with any weapon when hitting their head and your own Attack rises by +30. All effects last 2 minutes.\n\n※Attack is a final addition that is always the stated value and completely unaffected by other skills and multipliers.\n\nBlue Soul\t-10\tOwn Defense +50. Hitting another player will increase their Defense +100.\n\n※Hitting a player who is under the effect of Blue Soul while he/she is under any status ailment in the game will dispel the effect.\n\n※When hit by a player who has the Red Soul skill active will increase own Defense by +100 and grant Goddess' Embrace effect.\n\nAll effects last 2minutes.\n\nAssistance\t×\tAssistance\t10\tThe player's arm will glow red and grants +20 Attack, +50 Defense, Damage Recovery Speed+2, Status Immunity and Peerless to nearby Hunters.\n\nEffective radius is 3 rolls or 2 with Evade Distance Up. Affected players have their arms glow yellow.\n\n※The player with the skill does get +20 Attack and +50 Defense but does not benefit from Peerless, Status Immunity or Damage Recovery Speed.\n\n※The skills overwrite lower tier versions if any affected hunters have them. For example a hunter with Status Halved would gain Status Immunity within radius while a hunter with Status\n\nImmunity (Myriad) would retain their version of the skill.\n\n※Attack is a final addition thatis always the stated value and completely unaffected by other skills and multipliers.\n\nGrace\t×\tGrace+3\t30\tWhen there are only 3 or fewer active skills: Attack Up (Absolute), Issen+3, High-Grade Earplugs, WindRes (Large),Quake Res+1, Evasion+1,Guard+1,Goddess' Embrace and Weapon Handling will be active.\n\nGrace+2\t20\tWhen there are only 2 or fewer active skills: Attack Up (Absolute), Issen+3, High-Grade Earplugs, WindRes (Large),Quake Res+1, Evasion+1,Guard+1,Goddess' Embrace and Weapon Handling will be active.\n\nGrace+1\t10\tWhen there is only 1 active skill: Attack Up (Absolute), Issen+3, High-Grade Earplugs, WindRes (Large),Quake Res+1, Evasion+1,Guard+1,Goddess' Embrace and Weapon Handling will be active.\n\nCompensation\t×\tCompensation\t10\tDeath God's Embrace, Sharpness+1, Attack Up (Absolute), Evasion+2 and Critical Eye +4.\n\nDark Pulse\t×\tDark Finale\t20\tUpon health dropping to 0, you will rise again, gaining full Health and Stamina bars and Heavy Buffs but will also start constantly losing health at a steady rate.\n\nHealth loss cannot be halted by any methods and you will cart after exactly one minute has passed. Although the health degeneration cannot be stopped by any standard method, completing a quest will disable health loss meaning any final carts will not occur.\n\nIn your risen form you are buffed with the following skills: Adrenaline+2, Starving Wolf+2, Defense+120, Sharpening Artisan, Super HG Earplugs, Violent Wind Breaker, TremorRes +2, Heat Cancel and Cold Cancel.\n\nAlongside these skills you will gain a Super Armour effect that grants immunity to all forms of knock back.\n\nBlazing Grace\t×\tBlazing Majesty+2\t15\tAdrenaline+2, Red Soul, Bombardier, Fire Res+30, Artillery God, Summer Person, Terrain Damage Decreased (Large)\n\nFire Attack Up (Large), Flame Sword+3 and Bomb Sword+3 combined into 1 skill.\n\nBlazing Majesty+1\t10\tAdrenaline+1, Red Soul, Bombardier, Fire Res+20, Artillery Expert, Heat Cancel, Terrain Damage Decreased (Small)\n\nFire Attack Up (Small), Flame Sword+2 and Bomb Sword+2 combined into 1 skill.\n\nDrawing Arts\t×\tDrawing Arts+2\t20\tWhile weapon unsheathed: Peerless, Evasion+2, Weapon Handling\n\nWhile weapon is sheathed: Damage Recovery Speed+2, Quick Stamina Recovery (Large)\n\nDrawing Arts+1\t10\tWhile weapon unsheathed: Marathon Runner, Evasion+1, Weapon Handling\n\nWhile weapon is sheathed: Damage Recovery Speed+1, Quick Stamina Recovery (Small)\n\nIce Age\t×\tIce Age\t10\tUpon attacking a monster the hunter is surrounded by an icy aura. This aura deals damage to all monsters in its area and grants a number of different skills.This aura has three stages and will progress with more hits.\n\nThe aura also grants Stamina Recovery Up and Sharp Sword to all hunters affected by the aura and the one with the skill also gets Winter General.\n\nDamage is dealt once every second fixed rather than over time.";
        }
    }

    public static string GetGameSigilsInfo
    {
        get
        {
            return "Sigils are similar to decorations but exclusively used in G Rank weaponry. They are crafted at theCat Smith who creates random Gou weapons.Sigil Slots are triangular slots that either replace decoration slots in standard G Rank weaponry or are part of hybrid slots that can take either decorations or a sigil.Weapons can have up to three sigils. Sigils with variable values will generally always stack but ones that add or enhance abilities and motions generally have a fixed effect regardless of the number slotted.\n\n" +
                "Although most weapons have dedicated sigils, many are far from optimal. You should carefully consider the frequency with which you will actually be using the attack. For an excellent example,the Dual Swords sigils for the Rush Slash and Frontip Slash have incredibly low viability for Extreme Style because of Extreme Demon Mode not having access to them and because of optimal play involving almost exclusive use of that mode. Similarly,the GS Guard Slash is outright unavailable for Extreme Style and Upswings will mostly be charged - which does not benefit from the sigil - and so the sigils to buff those moves are mostly useless, especially compared to simply buffing raw. Realistically, if you want the best results you should really just be aiming almost entirely to get Attack or Elemental on sigils and one or more of your weapon's specific sigils. For Gunlance you should always aim to have Lv9 shelling if you are utilizing Shelling and Wyvern Fires.\n\n" +
                "Optimal sigil build is 15/15/15 Attack/Element Adv Shiten, 15/15/15 Attack/Element UL Sigil, 15/12(Duration)/20(Cooldown) Attack/Element Zenith Sigil (for multiplayer: Z Area of Effect)\n\n" +
                "Health and Stamina\n" +
                "Sleeping: Recover gear by using the gesture 「睡覺」 (does not stack)\n\n" +
                "Offensive\n" +
                "Attack Adjustment: -10 / +15. Adjusts the true raw of the weapon by the stated amount.\n\n" +
                "Attribute Adjustment: -10 / +15. Adjusts the true elemental of a weapon by the stated amount for +10 Displayed element per point.\n\n" +
                "Affinity: -10 / +15. Adjusts the affinity percentage of a weapon by the amount stated.\n\n" +
                "Flying Wyvern Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Bird Wyvern Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Carapaceon Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Piscine Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Fanged Beast Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Brute Wyvern Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Leviathan Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Elder Dragon Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing the specific defined species.\n\n" +
                "Minion Slayer: -10 / +15. Adjusts the true raw value stated while in an area containing small minion monsters.\n\n" +
                "Hot: -10 / +15. Adjusts true raw by the value stated while in a hot area that would require a Cold Pot(Desert,Volcano).\n\n" +
                "Cold: -10 / +15. Adjusts true raw by the value stated while in a cold area that would require a Hot Pot(Snowy Mountain, Swamp Caves).\n\n" +
                "Morning Slayer: -10 / +15. Adjusts true raw by the value stated while it is morning Taiwan Time or Japan Time\n\n" +
                "Night Slayer: -10 / +15. Adjusts true raw by the value stated while it is night Taiwan Time or Japan Time\n\n" +
                "Breeding Season Slayer: -10 / +15. Adjusts true raw by the value stated when in quests taking place in the Breeding Season (Green).\n\n" +
                "Warm Season Slayer: -10 / +15. Adjusts true raw by the value stated when in quests taking place in the Warm Season (Orange).\n\n" +
                "Cold Season Slayer: -10 / +15. Adjusts true raw by the value stated when in quests taking place in the Cold Season (Blue).\n\n" +
                "Monday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Monday Taiwanese or Japanese Time\n\n" +
                "Tuesday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Tuesday Taiwanese or Japanese Time\n\n" +
                "Wednesday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Wednesday Taiwanese or Japanese Time\n\n" +
                "Thursday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Thursday Taiwanese or Japanese Time\n\n" +
                "Friday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Friday Taiwanese or Japanese Time\n\n" +
                "Saturday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Saturday Taiwanese or Japanese Time\n\n" +
                "Sunday Slayer: -10 / +15. Adjusts true raw by the value stated when it is Sunday Taiwanese or Japanese Time\n\n" +
                "Unity: Adjusts true raw by +5 if all hunters in quest are nearby. Does not stack.\n\n" +
                "Strengthen Bow Raw: Adds additional raw damage based on attack strength. Formula: Attack x 0.015 x Charge Modifier x Hitbox.\nLv1: 0.04x\nLv2: 1.0x\nLv3: 1.5x\nLv4: 1.85x\nSnipe Lv3: 1.0x\nSnipe Lv4: 1.1x\nRising: 2.0x\nIs not affected by affinity, coatings or critical distance. Does not stack, does not work with Bows with Elemental\n\n" +
                "Strengthen SnS Raw: Adds additional raw damaged based on attack strength. Formula is: Attack x0.025 x Sharp Multiplier x Hitbox\nBlue: 1.0625x\nWhite: 1.125x\nPurple: 1.15x\nCyan: 1.20x\nIgnored motion values, Additional 0.2x the extra damage is added by Fencing+2. Does not stack, does not work with Sns with Elemental or Status.\n\n" +
                "Unlimited Mode: Only one Up roll can be active at any time. E.g. +14 DS Up and a +12 DS Up sigils would only be +14, not +26\n\n" +
                "Weapon Class Up: +1 / +15. Increases attack, affinity, and elemental values of appropriate weapon class.\n\n" +
                "Defensive\n\n" +
                "Defense: -10 / +20. Alters defense by the stated value.\n\n" +
                "Helper: Hunter's that are not G Rank gain +50defense in quests with a user of this sigil (does not stack).\n\n" +
                "Status\n" +
                "Status Attack: Increases Status value on weapons by 1.1x. Does not stack with more sigils. Does stack with Guild Poogie, Status Attack Up and Status Phials.\n\n" +
                "Stun Value: Increases KO inflicted by 1.1x. Only one sigil applies, does not stack with multiple sigils. Stacks with Caravan Skill (1.1x) and Active Feature (1.5x) for a maximum of 1.815x\n\n" +
                "Items\n" +
                "Omnivore: Chance to heal by using any consumable. Does not stack\n\n" +
                "Ballista Saver: Chance to not consume Ballista ammo by firing. Does not stack.\n\n" +
                "Map\n" +
                "Balloon's Friend: Allows unlimited waving at the Balloon to locate monster. Does not stack, does not force balloon spawns.\n\n" +
                "Decoy: Use the Clap gesture to attract a monster's attention to yourself. Does not stack.\n\n" +
                "Gathering\n" +
                "Dowsing: +1 / +15. Apparently increases the rate of Rare and G Rank items from a gathering spot's item pool. Unclear how it works mechanically, possibly similar to Caravan carving skill which rerolls if you get a carve above 51% and removes that item from the carving pool\n\n" +
                "Carving Division: Always carve Raw Meat in 2 stacks. Does not stack\n\n" +
                "Rewards\n" +
                "Hunter Soul: Increases Colour PP by +1 to the appropriate types on all eligible quests. Does not stack.\n\n" +
                "Soul Collection: Increases Soul from quests during festival by +1. Does not stack\n\n" +
                "Money Expert: +1 / +10. Increases money received from quest by 1.10 x Defined (Value/100) (e.g. +6 is 1.16x original Zeny or GZeny)\n\n" +
                "Experience Expert: +1 / +10. Increases Ranking Points received from quest by 1.01x Defined Value. Additive 1.10x, 1.20x and 1.30x on three separate sigils would be 1.50x, not 1.71x. Does not affect GSRP, only GRP.\n\n" +
                "Miscellaneous\n" +
                "Manager's Friend: +1 / +10. Increased chance of Farm Waifu appearing during quests.\n\n" +
                "Gook's Friend: +1 / +10. Increases chance of Rare Gooks on Gook Quests.\n\n" +
                "Halk's Friend: Increased chance of Halk dropping an egg at quest completion. Does not stack.\n\n" +
                "Cat Breeder: Partnyaa mood is more likely to increase.\n\n" +
                "Daddy's Influence: Perform「耍賴/ SpoiledBrat」action to get a 10% discount in stores.\n\n" +
                "Sonic Bomb Range: Increases the range of sonic bomb effect.\n\n" +
                "Weapon Movement Speed: Movement Speed will increase after unsheathing a weapon. Speed increase is between 1.13x to 1.20x. Does not stack or overlap with most similar armour skills. Overlaps with Hammer Charge Speed, SnS Hiden, Hunting Horn Movement Speed Song, Dual Swords Demon Mode and possibly MS Hiden speed buffs.\n\n" +
                "Rarity 1-12: Changes the rarity of a G Rank Weapon to be the defined value at the cost of lowering Raw, Elemental and Status values.\n\n" +
                "Zenith Sigil\n" +
                "Duration: +1 / +12. Dictates the effect of the Zenith Sigil, duration is 15 seconds plus the value of the sigil.\n\n" +
                "Cooldown: +1 / +20. Dictates the recharge duration of the sigil, cooldown is 120 seconds minus the value of the sigil.\n\n" +
                "[Zenith] Fire Res: +4 / +20. Increases the associated elemental resistance and removes any active blights of that element.\n\n" +
                "[Zenith] Water Res: +1 / +10. Increases the associated elemental resistance and removes any active blights of that element.\n\n" +
                "[Zenith] Thunder Res: +1 / +10. Increases the associated elemental resistance and removes any active blights of that element.\n\n" +
                "[Zenith] Ice Res: +1 / +10. Increases the associated elemental resistance and removes any active blights of that element.\n\n" +
                "[Zenith] Dragon Res: +1 / +10. Increases the associated elemental resistance and removes any active blights of that element.\n\n" +
                "[Zenith] Healing: +1 / +5. Recovers health over time while active. Can survive Rukodioras' nuke, Zinogres' nuke and Guanzorumus' nuke.\n\n" +
                "[Zenith] Heroics: Causes a single hit to deal 0damage.Reactions to the hit such as launching still occur.\n\n" +
                "[Zenith] Attack: +1 / +15. Increases raw values for its duration by 30+20x Value (e.g. for +8 it would be 20 * 8 = 160 + 30 = 190 True Raw.\n\n" +
                "[Zenith] Elemental: +1 / +15. Multiplies elemental values for its duration by 1.3 + Value * 0.1 (e.g. for +8 it would be 0.1 * 8 = 0.8 + 1.3 = 2.1x Elemental)\n\n" +
                "[Zenith] Movement Speed: +1 / +5. Increases movement speed for its duration.\n\n" +
                "Area of Effect Sigil\n" +
                "[Ranged] Attack: +1 / +15. Increases Attack Value while in the radius of the dome. Dome is coloured Red. Value for each roll is 25 + (Value * 5) (e.g. for +8 it would be 8 * 5 = 45 + 25 = 70 Attack Value)\n\n" +
                "[Ranged] Elemental: +1 / +15. Increases Elemental Value while in the radius of the dome. Dome is coloured Pink. Value for each roll is 50 + (Value * 50) (e.g. for +8 it would be 8 * 50 = 400 + 50 = 450 Elemental Value)\n\n" +
                "[Ranged] Affinity: +1 / +15. Increases Affinity Value while in the radius of the dome. Dome is coloured Blue. Value for each roll is 20+(Value *2)(e.g. for +8 it would be 8 * 2 = 16 + 20 = +36% Affinity\n\n" +
                "[Ranged] Status: Increases Status values by 1.50x while in the radius of the dome. Dome is coloured Purple. Does not stack.\n\n" +
                "[Ranged] Stun: Increases Stun values while in the radius of the dome. Dome is coloured Yellow.\n\n" +
                "[Ranged] Healing: Recovers Health while in the radius of the dome. Dome is coloured Green.\n\n" +
                "[Ranged] All Res: +1 / +15. Increases All Resistances while in the radius of the dome. Dome is coloured Black. Resistance is increased by roll x 2.\n\n"
                ;
        }
    }

    public static string GetGameMotionValuesSnS
    {
        get
        {
            return "KO indicates impact portions which use white sharpness as max multi\r\n" +
                "Jump Slash (Unsheathe)\t13(5)･21\r\n" +
                "Slide Slash\t21\r\n" +
                "Slash 1\t20\r\n" +
                "Slash 2\t26\r\n" +
                "Slash 3\t10(5)･16\r\n" +
                "Horizontal Slash\t31\r\n" +
                "Vacuum Slash Sigil\t31･16\r\n" +
                "Jump Slash 2\t26･24\r\n" +
                "Upslash\t20\r\n" +
                "Infinite Slash\t25\r\n" +
                "Infinite Slash Sigil\t28\r\n" +
                "Shield Bash\t13(5)\r\n" +
                "Shield Bash 2\t18(10)\r\n" +
                "Shield Bash Sigil\t19(5)\r\n" +
                "Shield Bash 2 Sigil\t27(10)\r\n" +
                "Slide Slash\t21\r\n" +
                "Slide Slash (Sigil)\t31\r\n" +
                "Continuous Bashing 1 to 3\t20(13) (Up to x3)\r\n" +
                "Dash: Aerial Shield Bash\t34(10)\r\n" +
                "Dash: Frontflip Slash\t18･34\r\n" +
                "Dash: Jump Slash (Unsheathe)\t13(5)･21\r\n" +
                "Evade Slash\t17\r\n" +
                "Reflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\n" +
                "Reflect +1\t24\r\n" +
                "Reflect +2\t36\r\n" +
                "Reflect +3\t48\r\n" +
                "Reflect Up\t68\r\n" +
                "Perfect Guard\t72\r\n" +
                "Perfect Guard Up\t92\r\n" +

                //"SnS Raw Sigil (Added to every hit of all motions)\r\n" +
                //"Sigil Additional\t0\r\n" +
                "Stylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\n" +
                "Stylish Up\t30";
        }
    }

    public static string GetGameMotionValuesDS
    {
        get
        {
            return "Extreme Demon Mode\r\nDownslash 1\t13･17\r\nDownslash 2\t13･10\r\nDownslash 3\t12･9･18･32\r\nUpslash\t22\r\nFlurry Combo\t13x3\r\nFlurry Rotation\t21･33･51\r\nWhirlwind Combo\t14x6\r\nWhirlwind Downslash\t36･54\r\nRising Slash\t13･13･12･12\r\nEvasion Slash (Forward･Back)\t16･16\r\nEvasion Slash (Left･Right)\t8･8･8･8\r\nDash: Upslash\t22\r\nDash: Sky Dance\t8x5\r\nSky Dance Finisher\t27･27\r\nDash: Evasion Slash\t16x2\r\nExtreme Demon Mode (Red Mode)\r\nDownslash 1\t14･18\r\nDownslash 2\t14･11\r\nDownslash 3\t13･10･20･34\r\nUpslash\t24\r\nFlurry Combo\t14x3\r\nFlurry Rotation\t23･36･55\r\nWhirlwind Combo\t15x6\r\nWhirlwind Downslash\t39･58\r\nRising Slash\t14･14･13･13\r\nEvasion Slash (Forward･Back)\t17･17\r\nEvasion Slash (Left･Right)\t9･8･9･8\r\nDash: Upslash\t24\r\nDash: Sky Dance\t8x5\r\nSky Dance Finisher\t29･29\r\nDash: Evasion Slash\t17x2\r\nStandard Mode\r\nRush Slash\t11･5\r\nRush Slash (Sigil)\t16･7\r\nDownslash 1\t10･13\r\nDownslash 2\t10･8\r\nDownslash 3\t9･7･14･24\r\nRotation Slash\t14･9･5\r\nUpslash\t17\r\nDash: Upslash\t17\r\nDash: Sky Dance\t6x5\r\nSky Dance Finisher\t20･20\r\nDemon Mode\r\nRush Slash\t14･6\r\nRush Slash (Sigil)\t21･9\r\nDownslash 1\t13･17\r\nDownslash 2\t13･10\r\nDownslash 3\t12･9･18･32\r\nRotation Slash\t18･12･6･29･17･12\r\nUpslash\t22\r\nFlurry Combo\t13x3\r\nFlurry Rotation\t21･33･51\r\nDash: Upslash\t22\r\nDash: Sky Dance\t8x5\r\nSky Dance Finisher\t27･27\r\nTrue Demon Mode\r\nRush Slash\t14･6\r\nRush Slash (Sigil)\t21･9\r\nDownslash 1\t13･17\r\nDownslash 2\t13･10\r\nDownslash 3\t12･9･18･32\r\nRotation Slash\t18･12･6･29･17･12\r\nUpslash\t22\r\nFlurry Combo\t13x3\r\nFlurry Rotation\t21･33･51\r\nWhirlwind Combo\t14x6\r\nWhirlwind Downslash\t36･54\r\nEvasion Slash\t12x2\r\nDash: Upslash\t22\r\nDash: Sky Dance\t8x5\r\nSky Dance Finisher\t27･27\r\nDash: Evasion Slash\t12x2\r\nTrue Demon Mode (Red Mode)\r\nRush Slash\t15･7\r\nRush Slash (Sigil)\t23･10\r\nDownslash 1\t14･18\r\nDownslash 2\t14･11\r\nDownslash 3\t13･9･20･35\r\nRotation Slash\t20･13･7･31･18･13\r\nUpslash\t22\r\nFlurry Combo\t13x3\r\nFlurry Rotation\t21･33･51\r\nWhirlwind Combo\t14x6\r\nWhirlwind Downslash\t36･54\r\nEvasion Slash\t13x2\r\nDash: Upslash\t24\r\nDash: Sky Dance\t8x5\r\nSky Dance Finisher\t29･29\r\nDash: Evasion Slash\t13x2\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesGS
    {
        get
        {
            return "Vertical Slash\t60\r\nVertical Finisher\t45･76\r\nHorizontal Slash\t46\r\nHorizontal Slash (Sigil)\t59\r\nHorizontal Finisher\t105\r\nUpswing\t58\r\nUpswing (Sigil)\t75\r\nUpswing Finisher\t117\r\nLv1 Charge\t85x110%\r\nLv2 Charge\t105x120%\r\nLv3 Charge\t146x130%\r\nUpswing Lv1 Charge\t92\r\nUpswing Lv2 Charge\t112x110%\r\nUpswing Lv3 Charge\t157x120%\r\nUpswing Lv4 Charge\t200x130%\r\nGuard: Vertical\t200\r\nGuard: Horizontal\t125\r\nShining Sword (Fencing Disabled)\t27x17 (459)\r\nCharge Attack Up+1\r\nLv1 Charge\t92x110%\r\nLv2 Charge\t112x120%\r\nLv3 Charge\t156x130%\r\nUpswing Lv1\t99\r\nUpswing Lv2\t119x110%\r\nUpswing Lv3\t167x120%\r\nUpswing Lv4\t213x130%\r\nShining Sword (Fencing Disabled)\t40x17 (680)\r\nCharge Attack Up+2\r\nLv1 Charge\t95x110%\r\nLv2 Charge\t115x120%\r\nLv3 Charge\t161.5x130%\r\nUpswing Lv1\t102\r\nUpswing Lv2\t122x110%\r\nUpswing Lv3\t172.5x120%\r\nUpswing Lv4\t220x130%\r\nShining Sword (Fencing Disabled)\t47x17 (799)\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30\r\n";
        }
    }

    public static string GetGameMotionValuesLS
    {
        get
        {
            return "Down Slash\t40\r\nDownslash 2\t33\r\nThrust\t25\r\nRising Slash\t28\r\nRising Slash (Sigil)\t42\r\nEvade Slash\t30\r\nSpirit 1 (0% Bar)\t17\r\nSpirit 1\t37\r\nSpirit 2\t39\r\nSpirit 3\t21･21･46\r\nSidehop Slash\t26\r\nPiercing Stab\t26･13･13\r\nPiercing Stab (Sigil)\t35･17･17\r\nStrong Piercing Stab\t25･12x5･31\r\nStrong Piercing Stab (Sigil)\t33･16x5･41\r\nBlade Release [<50%]\t5\r\nBlade Release [>50%]\t5･5\r\nBlade Release [100%]\t5･5･5\r\nRelease Blast [<50%] (No Fencing)\t10\r\nRelease Blast [>50%] (No Fencing)\t60\r\nRelease Blast [100%] (No Fencing)\t180\r\nDash: Charge (No Spirit use)\t18\r\nDash: Charge (Spirit use)\t70\r\nGuard: Teleport\t30･15\r\nGuard: Piercing Stab\t25･12x5･31\r\nSpirit Release\t40･※･※\r\nRelease [Spirit 1]\t60･※･※\r\nRelease [Spirit 2]\t70･※･※\r\nRelease [Spirit 3]\t90･※･※\r\n※ at 100% Spirit Bar\t35\r\n※ at 80% Spirit Bar\t26\r\n※ at 50% Spirit Bar\t20\r\nCharge Attack Up+1\r\nPiercing Stab\t29･15x2\r\nPiercing Stab (Sigil)\t38･19x2\r\nStrong Piercing Stab\t28･13x5･35\r\nStrong Piercing Stab (Sigil)\t36･17x5･45\r\nDash: Charge (No Spirit use)\t23\r\nDash: Charge (Spirit use)\t80\r\nSpirit Release\t49･※･※\r\nRelease [Spirit 1]\t71･※･※\r\nRelease [Spirit 2]\t82･※･※\r\nRelease [Spirit 3]\t105･※･※\r\n※ at 100% Spirit Bar\t41\r\n※ at 80% Spirit Bar\t32\r\n※ at 50% Spirit Bar\t26\r\nCharge Attack Up+2\r\nPiercing Stab\t30･16x2\r\nPiercing Stab (Sigil)\t39･20x2\r\nStrong Piercing Stab\t33･16x5･41\r\nStrong Piercing Stab (Sigil)\t38･18x5･47\r\nDash: Charge (No Spirit use)\t26\r\nDash: Charge (Spirit use)\t86\r\nSpirit Release\t54･※･※\r\nRelease [Spirit 1]\t76･※･※\r\nRelease [Spirit 2]\t88･※･※\r\nRelease [Spirit 3]\t112･※･※\r\n※ at 100% Spirit Bar\t44\r\n※ at 80% Spirit Bar\t35\r\n※ at 50% Spirit Bar\t29\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30\r\n";
        }
    }

    public static string GetGameMotionValuesHA
    {
        get
        {
            return "Unsheathe\t32(24)\r\nUnsheathe (Sigil)\t48(24)\r\nQuick Smash\t60(50)\r\nBaseball Swing\t100(50)\r\nBaseball Swing (Sigil)\t130(50)\r\nSmash 1\t52(25)\r\nSmash 2\t38(25)\r\nUpswing\t100(80)\r\nHorizontal Swing\t30(35)\r\nCharge Upswing\t45(30)\r\nCharge Lv1\t52(20)\r\nCharge Lv2\t52(20)･35(20)\r\nCharge Lv3\t40(20)･100(60)\r\nCharge Lv4 (Shoryuken)\t40(30)･30(30)･120(80)\r\nCharge Lv5 (Flip)\t60(60)･45(40)･145(115)\r\nSwipes Start\t60(15)\r\nSwipes Combo\t45(15)xn\r\nSwinging\t20(12)\r\nSwinging Slam Lv1\t30(20)･40(30)･10(10)\r\nSwinging Slam Lv2\t30(20)･40(30)･70(50)\r\nSwinging Slam Lv3\t30(20)･40(30)･160(130)\r\nCharge Attack Up +1\r\nCharge Upswing\t52(30)\r\nCharge Lv1\t56(20)\r\nCharge Lv2\t56(20)･36(20)\r\nCharge Lv3\t44(20)･109(60)\r\nCharge Lv4 (Shoryuken)\t41(30)･35(30)･128(80)\r\nCharge Lv5 (Flip)\t63(60)･47(40)･153(115)\r\nSwipes Start\t64(15)\r\nSwipes Combo\t48(15)xn\r\nSwinging Slam Lv1\t32(20)･43(30)･13(10)\r\nSwinging Slam Lv2\t32(20)･43(30)･75(50)\r\nSwinging Slam Lv3\t32(20)･43(30)･167(130)\r\nCharge Attack Up +2\r\nCharge Upswing\t55(30)\r\nCharge Lv1\t58(20)\r\nCharge Lv2\t58(20)･37(20)\r\nCharge Lv3\t46(20)･114(60)\r\nCharge Lv4 (Shoryuken)\t41(30)･35(30)･128(80)\r\nCharge Lv5 (Flip)\t64(60)･48(40)･157(115)\r\nSwipes Start\t66(15)\r\nSwipes Combo\t49(15)xn\r\nSwinging Slam Lv1\t33(20)･44(30)･15(10)\r\nSwinging Slam Lv2\t33(20)･44(30)･77(50)\r\nSwinging Slam Lv3\t33(20)･44(30)･170(130)\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesHH
    {
        get
        {
            return "Slam Attack (to Note 2)\t18(8)･57(30)\r\nUpthrust Start\t60(24)\r\nUpthrust (to Note 2)\t30(24)xn\r\nUpthrust End\t39(30)\r\nUpswings (to Note 3)\t40(20)\r\nPoke (to Note 1)\t16\r\nPoke (to Note 1) (Sigil)\t27\r\nStart Playing\t26(26)\r\nStart Playing (Sigil)\t39(26)\r\nNote 1 (L/R Face Button)\t50(26)\r\nNote 1 (L/R Face Button) (Sigil)\t75(26)\r\nNote 2 (Top Face Button)\t43(26)\r\nNote 2 (Top Face Button) (Sigil)\t64(26)\r\nNote 3 (T+R Face Buttons)\t49(26)\r\nNote 3 (T+R Face Buttons) (Sigil)\t73(26)\r\nStop Playing\t31(30)\r\nStop Playing (Sigil)\t46(30)\r\nDash: Turning Point\t40(20)･40(20)\r\nSonic Bomb Debuff\t30 Fixed\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesLA
    {
        get
        {
            return "Unsheathe Stabs\t36\r\nThrust 1 - 3\t32\r\nThrust 4\t38\r\nThrust 4 (Hiden)\t48\r\nUpthrust 1 - 3\t35\r\nUpthrust 4\t38\r\nUpthrust 4 (Hiden)\t48\r\nSky Stab 1 - 3\t38\r\nSky Stab 4\t43\r\nSky Stab 4 (Hiden)\t53\r\nShield Rush ※ White Sharp\t3(5)･25(10) ※ Impact\r\nShield Rush (Sigil) ※ White Sharp\t4(5)･37(10) ※ Impact\r\nYellow Charge\t50\r\nRed Charge\t78\r\nEvade Thrust\t24\r\nFinishing Thrust\t91\r\nDash: Heavy Thrust\t50\r\nDash: Guard ※ White Sharp\t25(13) ※ Impact\r\nCharge Attack Up +1\r\nYellow Charge\t55\r\nRed Charge\t89\r\nCharge Attack Up +2\r\nYellow Charge\t58\r\nRed Charge\t94\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesGL
    {
        get
        {
            return "Rush Thrust (Unsheathe)\t38\r\nRush Thrust (Unsheathe) (Sigil)\t57\r\nHorizontal Sweep\t34\r\nHorizontal Sweep (Sigil)\t51\r\nBackhop Slash\t18 + Shelling\r\nThrust 1-2\t33\r\nThrust 3\t34\r\nUpswing\t42\r\nUpthrusts\t30\r\nBombardment Boost: Dash Cleave\t58\r\nHeatblade (Elemental)\t5 + Element\r\nHeatblade (Raw)\t9\r\nHeatblade Slam\t62\r\nEle Shell Charge Up\t25\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesTO
    {
        get
        {
            return "Long Mode\r\nUnsheathe\t28(12)\r\nStandard 1\t14(6)\r\nStandard 2 Lv1\t11(8)\r\nStandard 2 Lv2\t38(22)\r\nStandard 2 Lv3\t58(32)\r\nEX Pursuit 1\t8(7)\r\nEX Pursuit 2\t23(20)\r\nEX Pursuit 3\t33(30)\r\nStandard 2 & EX Lv1\t11(8)･8(7)\r\nStandard 2 & EX Lv2\t38(8)･23(20)\r\nStandard 2 & EX Lv3\t58(8)･33(30)\r\nStandard 3\t18(6)\r\nStandard 4\t9x2(7)\r\nStandard 5 Lv1\t16(20)\r\nStandard 5 Lv2\t50(60)\r\nStandard 5 Lv3\t110(110)\r\nEX Evade (Back)\t34(20)\r\nEX Evade (Side)\t20(20)･20(10)\r\nEX Evade (Forward)\t24(15)\r\nAerial EX Evade\t16(8)･16(8)\r\nDash Stab\t12(10)･20(15)\r\nDash Kick\t7x2(3)[K]･12(4)･20(4)\r\nSpecial 1\t11x2(2)[K]\r\nSpecial 2\t10(10)\r\nSpecial 3\t9(6)･12(8)\r\nSpecial 4\t26(24)\r\nSpecial 5\t16(15)･11x4(13)･24(20)\r\nSpecial 6\t19x2(2)[K]\r\nJump Evasion\t5x2(2)[K]\r\nAerial 1\t15(14)\r\nAerial 2\t10x2(13)\r\nAerial 3\t15(11)[K]･16(11)\r\nAerial 1-3 Combo\t66 (5 hits)\r\nLanding Kick\t12x2(12)\r\nRyuuki\t1x2･40(2)\r\nAerial Ryuuki\t1x2･30(2)\r\nBoxing Dance 4 Hits\t12(2)\r\nBoxing Dance 8 Hits\t14(2)\r\nBoxing Dance 12 Hits\t16(2)\r\nBoxing Dance 12 Hit Combo\t168(24)\r\nBoxing Dance 13 Hits ~\t28(2)\r\nRelease\t30(10)\r\n~ Burst ~ 3 Hits\tBomb 50(5)\r\n~ Burst ~ 11 Hits\tBomb 100(5)\r\n~ Burst ~ 12 Hits+\tBomb 200(5)\r\nCharge Attack Up +2 & Gunnery God\r\nStandard 2 & EX Lv1\t13(8)･11(7)\r\nStandard 2 & EX Lv2\t42(22)･32(20)\r\nStandard 2 & EX Lv3\t65(32)･48(30)\r\nCharge Attack Up +1\r\nStandard 2 Lv1\t12(8)\r\nStandard 2 Lv2\t41(22)\r\nStandard 2 Lv3\t62(32)\r\nStandard 5 Lv1\t18(20)\r\nStandard 5 Lv2\t55(60)\r\nStandard 5 Lv3\t119(110)\r\nCharge Attack Up +2\r\nStandard 2 Lv1\t13(8)\r\nStandard 2 Lv2\t42(22)\r\nStandard 2 Lv3\t65(32)\r\nStandard 5 Lv1\t20(20)\r\nStandard 5 Lv2\t61(60)\r\nStandard 5 Lv3\t131(110)\r\nGunnery\r\nEX Pursuit 1 (G. God)\t11(7)\r\nEX Pursuit 2 (G. God)\t32(20)\r\nEX Pursuit 3 (G. God)\t48(30)\r\nEX Pursuit 1 (G. Expert)\t10(7)\r\nEX Pursuit 2 (G. Expert)\t29(20)\r\nEX Pursuit 3 (G. Expert)\t43(30)\r\nEX Pursuit 1 (G. Novice)\t9(7)\r\nEX Pursuit 2 (G. Novice)\t26(20)\r\nEX Pursuit 3 (G. Novice)\t38(30)\r\nRyuuki (G. Novice)\t8x2･40(2)\r\nRyuuki (G. Expert)\t10x2･40(2)\r\nRyuuki (G. God)\t15x2･40(2)\r\nAerial Ryuuki (G. Novice)\t7x2･30(2)\r\nAerial Ryuuki (G. Expert)\t9x2･30(2)\r\nAerial Ryuuki (G. God)\t12x2･30(2)\r\nMartial Arts +1\r\nDash Kick\t11x2(3)[K]･15(4)･25(4)\r\nJump Evasion\t6x2(2)[K]\r\nSpecial 1\t16x2(2)[K]\r\nSpecial 6\t28x2(2)[K]\r\nAerial 1 - 3\t74 (5 hits) (Orig: 116)\r\nMartial Arts +2\r\nDash Kick\t15x2(3)[K]･15(4)･25(4)\r\nJump Evasion\t7x2(2)[K]\r\nSpecial 1\t18x2(2)[K]\r\nSpecial 6\t33x2(2)[K]\r\nAerial 3\t23(11)[K]･25(11) (Orig: 31･33)\r\nAerial 1-3 Combo\t83 (5 hits) (Orig: 116)\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesSAF
    {
        get
        {
            return "Axe Mode\r\nVertical Slash\t46\r\nUpslash\t35\r\nHorizontal Slash\t34\r\nRush Slash\t33\r\nHack 'n' Slash\t35\r\nEle, Status, Stun Phials Sword Mode\r\nRunning Rotation Slash\t32･41 (15･0)\r\nRunning Upslash\t29 (14)\r\nSlash Evasion\t21 (6)\r\nS Evasion into Thrust\t33\r\nS Evasion Charged Upslash\t75 (38)\r\nCharge Infinite Slash\t33 (5)\r\nCharged Upslash\t75 (38)\r\nCharge Attack Up +1\r\nS Evasion Charged Upslash\t81 (38)\r\nCharge Infinite Slash\t35 (5)\r\nCharged Upslash\t81 (38)\r\nCharge Attack Up +2\r\nS Evasion Charged Upslash\t83 (38)\r\nCharge Infinite Slash\t37 (5)\r\nCharged Upslash\t83 (38)\r\nEle, Status, Stun Phials Lightsword Mode\r\nLight Sword Transformation\t46 (19)\r\nVertical Slash\t46 (19)\r\nUpslash\t48 (15)\r\nSide Slash\t41 (15)\r\nRunning Rotation Slash\t33･45 (21･21)\r\nRunning Upslash\t31 (15)\r\nSlash Evasion\t22 (7)\r\nS Evasion Upslash\t48 (15)\r\nS Evasion Charged Upslash\t79 (40)\r\nCharged Upslash\t79 (38)\r\nOvercharged Downslash\t46 (19)\r\nCharge Infinite Slash\t35 (6)\r\nContinuous Slash\t53･53 (19･19)\r\nContinuous Slash Finisher\t42･73 (26･35)\r\nDischarge [Start]\t19 (8)\r\nDischarge [Ticks]\t13 (0)\r\nDischarge [Early Blast]\t160 (20) (40 Sigil)\r\nDischarge [Blast]\t270 (50) (80 Sigil)\r\nAbs. Release [Ticks]\t13･13･13 (4･4･4)\r\nAbs. Release [Early Finisher]\t9･9 (5･5)\r\nAbs. Release [Early Blast]\t125 (40) (50 sigil)\r\nAbs. Release [1~2 Ticks Finisher]\t18･18 (5･5)\r\nAbs. Release [1~2 Ticks Blast]\t280 (130) (150 Sigil)\r\nAbs. Release [Finisher]\t27･27 (5･5)\r\nAbs. Release [Blast]\t375 (150) (220 Sigil)\r\nCharge Attack Up +1\r\nS Evasion Charged Upslash\t85 (40)\r\nCharged Upslash\t85 (38)\r\nCharge Infinite Slash\t37 (6)\r\nCharge Attack Up +2\r\nS Evasion Charged Upslash\t87 (40)\r\nCharged Upslash\t87 (38)\r\nCharge Infinite Slash\t39 (6)\r\nPower Phial Sword Mode\r\nRunning Rotation Slash\t35･41 (15･0)\r\nRunning Upslash\t31\r\nSlash Evasion\t23\r\nS Evasion into Thrust\t33\r\nS Evasion Charged Upslash\t82\r\nCharge Infinite Slash\t36\r\nCharged Upslash\t82\r\nCharge Attack Up +1\r\nS Evasion Charged Upslash\t89\r\nCharge Infinite Slash\t38\r\nCharged Upslash\t89\r\nCharge Attack Up +2\r\nS Evasion Charged Upslash\t89\r\nCharge Infinite Slash\t38\r\nCharged Upslash\t89\r\nPower Phial Lightsword Mode\r\nLight Sword Transformation\t50\r\nVertical Slash\t50\r\nUpslash\t52\r\nSide Slash\t45\r\nRunning Rotation Slash\t36･45\r\nRunning Upslash\t34\r\nSlash Evasion\t24\r\nS Evasion Upslash\t52\r\nS Evasion Charged Upslash\t86\r\nCharged Upslash\t86\r\nOvercharged Downslash\t50\r\nCharge Infinite Slash\t38\r\nContinuous Slash\t58･58\r\nContinuous Slash Finisher\t46･80\r\nDischarge [Start]\t20\r\nDischarge [Ticks]\t14\r\nDischarge [Early Blast]\t176\r\nDischarge [Blast]\t297\r\nAbs. Release [Ticks]\t14･14･14\r\nAbs. Release [Early Finisher]\t9･9\r\nAbs. Release [Early Blast]\t125\r\nAbs. Release [1~2 Ticks Finisher]\t19･19\r\nAbs. Release [1~2 Ticks Blast]\t280\r\nAbs. Release [Finisher]\t29･29\r\nAbs. Release [Blast]\t375\r\nCharge Attack Up +1\r\nS Evasion Charged Upslash\t85\r\nCharged Upslash\t85\r\nCharge Infinite Slash\t37\r\nCharge Attack Up +2\r\nS Evasion Charged Upslash\t87\r\nCharged Upslash\t87\r\nCharge Infinite Slash\t39\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesMS
    {
        get
        {
            return "Cutting Mode\r\nUnsheathe Vertical\t61\r\nVertical 1\t43\r\nUpslash\t35\r\nVertical 2\t46\r\nHorizontal 1\t56\r\nHorizontal 2\t46\r\nHorizontal 3\t64\r\nFinishing Slash\t120\r\nFinishing Slash [Magnetised]\t120‧10x3\r\nGuard Counter\t30\r\nGuard Counter [Success]\t30‧80\r\nMagnetic Double Slash\t44‧54\r\nMagnetic Evade Upslash\t55\r\nRetreat Slash\t20‧20\r\nMagnetic Assault (Leap)\t80\r\nFalling Downslash\t200\r\nMagnetic Pin\t1\r\nMagnetic Pin Finisher\t600\r\nMagnetic Pin Finisher [Feature]\t750\r\nImpact Mode\r\nHorizontal 1\t44 (30)\r\nHorizontal 2\t53 (30)\r\nOverhead Horizontal\t100 (45)\r\nDown Swipe\t46 (25)\r\nTriple Strike 1\t51 (10)\r\nTriple Strike 2\t58 (20)\r\nTriple Strike 3\t130 (50)\r\nCharged Strike\t338 (190)\r\nCharged Strike [Charge UP+1]\t363 (190)\r\nCharged Strike [Charge UP+2]\t373 (190)\r\nSupplex\t96 ‧ 121 (50 ‧ 65)\r\nGuard Assault\t73 ‧ 90 (35 ‧ 45)\r\nMagnetic Double Swipe\t42 ‧ 52 (30 ‧ 35)\r\nMagnetic Shock (Whiff)\t75 (30)\r\nMagnetic Shock (Iframes Used)\t125 ‧ 5x3 (60 ‧ 5x3)\r\nMagnetic Assault (Leap)\t40 ‧ 50 (35 ‧ 35)\r\nFalling Swings\t25 ‧ 80 ‧ 120 (20 ‧ 35 ‧ 75)\r\nMagnetic Pin\t1\r\nMagnetic Pin Finisher\t600\r\nMagnetic Pin Finisher [Feature]\t750\r\nReflect (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nReflect +1\t24\r\nReflect +2\t36\r\nReflect +3\t48\r\nReflect Up\t68\r\nPerfect Guard\t72\r\nPerfect Guard Up\t92\r\nStylish Up (Cannot Crit, Impact Damage, Normal sharpness multiplier)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesLBG
    {
        get
        {
            return "Melee\r\nMelee Attacks\t5･5･10\r\nShots\r\nLV1 Norm S.\t6\r\nLV2 Norm S.\t12\r\nLV3 Norm S.\t12xn\r\nLV1 Pierce S. (1 Hit)\t10x3\r\nLV2 Pierce S. (1 Hit)\t9x4\r\nLV3 Pierce S. (1 Hit)\t7x6\r\nLV1 Pierce S. (3 Hit)\t10x3\r\nLV2 Pierce S. (4 Hit)\t9x4\r\nLV3 Pierce S. (6 Hit)\t7x6\r\nLV1 Pellet S.\t5x3, Water 50\r\nLV2 Pellet S.\t5x4, Water 40\r\nLV3 Pellet S.\t5x5, Water 40\r\nLV1 Impact Pellet (Sigil)\t10(4)x2\r\nLV2 Impact Pellet (Sigil)\t9(4)x3\r\nLV3 Impact Pellet (Sigil)\t7(4)x5\r\nLV1 Crag S.\t3, Bomb 30, Fire 400\r\nLV2 Crag S.\t3, Bomb 40, Fire 600\r\nLV3 Crag S.\t3, Bomb 50, Fire 800\r\nLV1 Cluster S.\t(6 + Bomb 32 + Fire 20) x 3\r\nLV2 Cluster S.\t(6 + Bomb 32 + Fire 20) x 4\r\nLV3 Cluster S.\t(6 + Bomb 32 + Fire 20) x 5\r\nFire Shot\t1 + (Attack x 0.4 Fire)\r\nWater Shot\t(1 + (Attack x 0.2 Water)) x 3\r\nThunder Shot\t(1 + (Attack x 0.2 Thunder)) x 3\r\nIce Shot\t(1 + (Attack x 0.2 Ice)) x 3\r\nDragon Shot\t750 Dragon\r\nLV1 Recovery Shot\t1 (30 Healing)\r\nLv2 Recovery Shot\t1 (50 Healing)\r\nLV1 Poison\t10 (25 Status)\r\nLv2 Poison\t15 (50 Status)\r\nLV1 Para\t10 (25 Status)\r\nLV2 Para\t15 (50 Status)\r\nLV1 Sleep\t1 (25 Status)\r\nLV2 Sleep\t1 (50 Status)\r\nTranq Shot\t1 (80 Cap Value)\r\nPaint Shot\t1 (Paintball)\r\nDemon Shot\t1 (Attack +3)\r\nArmour Shot\t1 (Defense +6)\r\nAcid Shot\t1 (Raw Weakness +10)\r\nEle Acid Shot\t1 (Ele Weakness +10)\r\nStylish Up (No Criticals, Impact Damage)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesHBG
    {
        get
        {
            return "Shots\r\nLV1 Norm S.\t6x1\r\nLV2 Norm S.\t12x1\r\nLV3 Norm S.\t12xn\r\nLV1 Pierce S. (1 Hit)\t12\r\nLV2 Pierce S. (1 Hit)\t11\r\nLV3 Pierce S. (1 Hit)\t10\r\nLV1 Pierce S. (3 Hit)\t12x3\r\nLV2 Pierce S. (4 Hit)\t11x4\r\nLV3 Pierce S. (6 Hit)\t10x6\r\nLV1 Pellet S.\t5x3, Water 50\r\nLV2 Pellet S.\t5x4, Water 40\r\nLV3 Pellet S.\t5x5, Water 40\r\nLV1 Impact Pellet (Sigil)\t10x2 (8x2 KO)\r\nLV2 Impact Pellet (Sigil)\t9x3 (8x3 KO)\r\nLV3 Impact Pellet (Sigil)\t7x5 (8x5 KO)\r\nLV1 Crag S.\t3, Bomb 30, Fire 400\r\nLV2 Crag S.\t3, Bomb 40, Fire 600\r\nLV3 Crag S.\t3, Bomb 50, Fire 800\r\nLV1 Cluster S.\t6, Bomb 32, Fire 20x3\r\nLV2 Cluster S.\t6, Bomb 32, Fire 20x4\r\nLV3 Cluster S.\t6, Bomb 32, Fire 20x5\r\nFire Shot\t1 + (Attack x 0.5 Fire)\r\nWater Shot\t1 + (Attack x 0.25 Water) x 3\r\nThunder Shot\t1 + (Attack x 0.27 Thunder) x 3\r\nIce Shot\t1 + (Attack x 0.25 Ice) x 3\r\nDragon Shot\t900 Dragon x 6\r\nLV1 Recov\t1 (30 Healing)\r\nLv2 Recov\t1 (50 Healing)\r\nLV1 Poison\t10 (25 Status)\r\nLv2 Poison\t15 (50 Status)\r\nLV1 Para\t10 (25 Status)\r\nLV2 Para\t15 (50 Status)\r\nLV1 Sleep\t1 (25 Status)\r\nLV2 Sleep\t1 (50 Status)\r\nTranq Shot\t1 (80 Cap Value)\r\nPaint Shot\t1 (Paintball)\r\nDemon Shot\t1 (Attack +3)\r\nArmour Shot\t1 (Defense +6)\r\nCompression Shots\r\nCompressed Shot Motion\t0\r\nCompressed Elemental Shot\t0\r\nStylish Up (No Criticals, Impact Damage)\r\nStylish Up\t30";
        }
    }

    public static string GetGameMotionValuesBow
    {
        get
        {
            return "Melee\r\nMelee Attacks\t10･10･18\r\nShoryuken\t12x2･10x2･31･10\r\nShoryuken (Sigil)\t17x2･15x2･41･15\r\nShots\r\nRapid LV1\t14\r\nRapid LV2\t14･6\r\nRapid LV3\t14･6･5\r\nRapid LV4\t14･6･5･3\r\nScatter LV1\t4･6･4\r\nScatter LV2\t6･7･6\r\nScatter LV3\t4･6･6･6･4\r\nScatter LV4\t4･6･7･6･4\r\nPierce LV1\t7x3\r\nPierce LV2\t7x4\r\nPierce LV3\t7x5\r\nPierce LV4\t7x6\r\nUncharged Horizontal Rising Shot\t4\r\nUncharged Vertical Rising Shot\t40 (0.4x Raw, 1.0x Elemental)\r\nCharged Horizontal Rising Shot\t4 (1.0x Raw, 1.5x Elemental)\r\nCharged Vertical Rising Shot\t40 (1.0x Raw, 1.5x Elemental)\r\nBow Raw Sigil (Added to every hit of all motions)\r\nSigil Additional\t0\r\nArc Shots\r\nWide Pellet Arc\t18(3)x7\r\nNarrow Pellet Arc\t16x5\r\nExplosive Shell Arc\t15(30) 19 Bomb\r\nArrow Rain Arc (Cutting, Shot HB)\t12x7\r\nCrouch Shots\r\nSniper Shot Lv4\t12x5\r\nPiercing Sniper Shot Lv4\t10x5･25･10･5･4･2\r\nSniper Shot Lv5\t12x5\r\nPiercing Sniper Shot Lv4\t10x5･25･10･5･4･2\r\nStylish Up (No Criticals, Impact Damage)\r\nStylish Up\t30";
        }
    }

    public string GetGameMotionValues
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            return s.MotionValuesInfo switch
            {
                "Sword and Shield" => GetGameMotionValuesSnS,
                "Dual Swords" => GetGameMotionValuesDS,
                "Great Sword" => GetGameMotionValuesGS,
                "Long Sword" => GetGameMotionValuesLS,
                "Hammer" => GetGameMotionValuesHA,
                "Hunting Horn" => GetGameMotionValuesHH,
                "Lance" => GetGameMotionValuesLA,
                "Gunlance" => GetGameMotionValuesGL,
                "Tonfa" => GetGameMotionValuesTO,
                "Switch Axe F" => GetGameMotionValuesSAF,
                "Magnet Spike" => GetGameMotionValuesMS,
                "Light Bowgun" => GetGameMotionValuesLBG,
                "Heavy Bowgun" => GetGameMotionValuesHBG,
                "Bow" => GetGameMotionValuesBow,
                _ => GetGameMotionValuesSnS,
            };
        }
    }

    public string GetGameCompanionInfo
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            return s.CompanionOptionInfo switch
            {
                "Partner" => GetGamePartnerInfo,
                "Rasta" => GetGameRastaInfo,
                "Poogie" => GetGamePoogieInfo,
                "Halk" => GetGameHalkInfo,
                "Partnyaa" => GetGamePartnyaaInfo,
                _ => GetGamePartnerInfo,
            };
        }
    }

    public string GetGameCaravanInfo
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            return s.CaravanOptionInfo switch
            {
                "About" => GetGameCaravanAbout,
                "Skills" => GetGameCaravanSkills,
                "Quests" => GetGameCaravanQuests,
                "Pioneer" => GetGameCaravanPioneer,
                _ => GetGameCaravanAbout,
            };
        }
    }

    public static string GetGameCaravanAbout
    {
        get
        {
            return "Pallone Caravan\r\nThe Pallone Caravan is a Hub Area that you can enter from Mezeporta at HR17\r\nIf you talk to The Chief when you enter, you'll recieve a Caravan Gem\r\nPallone Carnival\r\n・Great Reduction Festival：Slightly increases the chance of obtaining quest rewards, and also increases the chance of obtaining items in emergency missions.\r\n・Great Bead Festival：Party points (PP) will increase 1.5 times more than normal\r\n・Great Recommendation：Reputation points will be increased by 1.5 times than usual.\r\n・Great Reward Festival：Greatly increases the chances of getting quest rewards\r\n\r\nFacilities\r\n・Hospitality Lounge\u3000※You can enter with the reputation of 「Hard Working Hunter」\r\n\u3000Lounge Bento(1000CP)can be purchased.\r\n\r\n・Guest House\u3000※You can enter with the reputation of 「Familiar Hunter」\r\n\u3000Inside is the Specialty Workshop, and Specialty Item Shop.\r\n・Guest House Floor 2\u3000※You can enter with the reputation of 「Tale Telling Hunter」\r\nCarnival Medicine：200000CP\r\n\u3000\u3000Generate a Great ***** listed above randomly. It is effective when you are the owner of the quest (applies to all participants). 2 hours\r\n\r\nReputation\r\n\u3000Caravan Quests gain recognition, instead of HRP\r\n\r\nName\tPoints\r\nTemporary Hunter\t0\r\nBarely a Hunter\t5940\r\nHard Working Hunter\t37088\r\nFamiliar Hunter\t121688\r\nTale Telling Hunter\u3000\t275648\r\nPositive Hunter\t689648\r\nMemorable Hunter\t1153328\r\nTrustworthy Hunter\t1799792\r\nAppreciated Hunter\t2492432\r\nWidely Trusted Hunter\t3185072\r\nReliable Hunter\t4180752\r\nSuccessful Hunter\t5176432\r\nExclusive Hunter\t6257456\r\nAspiring Legend\t8028848\r\nLegendary Hunter\t9926768\n\n" +
                "Hunter Jewel\r\nColor\r\n・There are eight colors of hunter beads: pink, brown, yellow, green, white, blue, light blue, and rainbow. Rainbow is a special color.\r\n・The color will change during regular maintenance (every Wednesday).\r\n\u3000\u3000※Apart from this, it may change to \"rainbow color\" at 12:00 noon\r\n・If it is rainbow, it will change to another color at 12:00 the next day.\r\n\r\n・In order to accumulate colors, clear route 101 and beyond.\r\n・Only the number of colors of the people who participated in the quest will be added, but rainbow will change to all colors +1\r\n\u3000※Since the effects of rainbow colors do not overlap, all colors remain +1 no matter how many rainbow colors are present.\r\n\r\n・Activation is required when the hunter jewel is strengthened beyond the second level.\r\n・Once the Activation period has passed, you will no longer be able to use the Hunter Jewel skill.(outdated?)\r\n\r\nSkill\r\n・Can activate up to 3 skills with a total cost of up to 10";
        }
    }

    public static string GetGameCaravanQuests
    {
        get
        {
            return "Caravan Quest\r\nCharacteristics\r\n・All materials obtained during the quest are converted into CP.\r\n※Items obtained by poogie will be kept as material.\r\n・There is an emergency mission during the quest, and if you complete it, you will get 500 CP or the following items.\r\n\u3000Common：Return Ball,Vitality Agent G,Sword Crystals,Monster Mats\r\n\u3000Great Reduction Festival：Gunlance Coolant,Guts Tkt,Adrenaline Potion,Starving Wolf Potion,L.Sword Spirit Drug\r\n\u3000Other than Great Reduction Festival：PoisonDefenceTkt,Sleep Defence Tkt,Para Defence Tkt,Emergency Bag\r\n・There is no disadvantage in clearing via a sub-target.\r\n・The quests are the same as those of the Normal Hunting.\r\n・The quests you can post are limited by the * rank you've cleared.Participation is open to everyone regardless of *rank。\r\n・If you return halfway through, you will receive the number of recognition, CP, etc. that you have earned so far.\r\n・Only ★0 will not get the hunter recognition.\r\nTension gauge (verification required)\r\n・By filling up the Tension Gauge, your attack and defense power will increase. Accumulate the gauge to the maximum and get 1.5 times atk.\r\n・During a strike, attacking an ally raises the tension gauge of the attacked side.\r\n・The amount of increase in the gauge is determined according to the Route difference. 1 hit increases attack and defense power by 1.01 times (up to half of the gauge).\r\n・Also, tension may increase depending on the route difference cleared at the start of the quest (up to half of the gauge)\r\n・Tension UP at the start of the quest + Tension UP during the quest will maximize the gauge.";
        }
    }

    public static string GetGameCaravanPioneer
    {
        get
        {
            return "Pioneering\r\n・You can develop a map by talking to the balloon cat in Parone Caravan.\r\n・By developing it, you will be able to acquire special items.\r\n\u3000Also, by posting a development quest, other hunters will be able to participate in the quest.\r\n\r\nPioneering Procedure\r\n１．Talk to the balloon cat and select a map.\r\n\u3000\u3000(When switching to another map, all developments up to that point will be reset.)\r\n２．Select an area and place a base (gathering, mining, insects, artifacts, defense). Two can be placed in one area.\r\n\u3000\u3000(Materials and CP required for installation are listed separately)\r\nSelecting Migration instead of Restart will keep your levels while migrating to a new map.\r\n３．The base is completed by completing a specified number of quests.\r\n\u3000\u3000(You can fail the quest. Abandoning is not allowed.)\r\n４．By hiring a hired cat, you can accelerate the development speed.\r\n５．After a certain period of time, bases begin to deteriorate. It can be updated just by talking to the balloon cat, and the maintenance fee is free. You can't go below Lv1.\r\n\r\nBase development speed\r\nLV1： Complete 20 quests\r\nLV2： Complete 50 quests\r\nLV3： Complete 100 quests\r\n\r\nEmployed cat effect\r\n\u3000\u3000\u3000\u30000～6000CP： No Change\r\n\u30007000～29000CP： 24 hours, 1.2 times adjustment to the number of required quests\r\n30000～59000CP： 24 hours, 1.5 times adjustment to the number of required quests\r\n60000CP～\u3000\u3000\u3000\u3000： 24 hours, 2.0 times adjustment to the number of required quests\r\n\r\nDevelopment progress\r\nLevel up according to the base LV of each area\r\n11～20： LV1 One flag at the base camp\r\n21～30： LV2 Two flags at the base camp\r\n31～40： LV3 Three flags at the base camp\r\n41～50： LV4 Four flags at the base camp\r\n51～60： LV5 Five flags at the base camp\r\n\r\nPioneer Quest\r\n・Quests can be changed in the public quest settings to change the targets to be defeated and the level of difficulty.\r\n・The specifications of the quest are the same as the caravan, and the color of the beads is added to the color of the participant.\r\n・Collecting from the base is not possible without going on a pioneering quest.\r\n・If a pioneering quest is left open to the public, other hunters can join the quest.\r\n\u3000\u3000(public settings will be canceled when you log out)\r\n・When the pioneer quest is accepted, when play time (regardless of success or failure) reaches a certain amount, you can receive a public reward from the balloon cat.\r\n\u3000\u3000(Before receiving the materials, you can set the quest for the materials you want and you will receive the materials for that quest.)\r\n・Pioneer quests are randomly selected from the hunter's quests according to the set HR and target monster conditions.\r\n・One worker cat is placed for each type of base in each area, if there is a monster, the cat will perform a support attack with rock throwing and throwing bombs.\r\n・For defensive bases, ballistas are launched from balloons. The number of ballistas fired increases as the base level increases.\r\n・Difficulty\r\n\u3000Weak: Rewards are halved (one piece per frame will be 0), physical strength and attack power are 60%\r\n\u3000Normal: normal rewards\r\n\u3000Strong: 1.5x reward (fractions are rounded down), 1.5x HP and ATK\r\n\u3000Strongest: 2x rewards, 2x health and attack power";
        }
    }

    public static string GetGameCaravanSkills
    {
        get
        {
            return "If your caravan gem is below level 8 you will have less pages. The skills will however be in the same order.\n\n" +
                "Page 1\n" +
                "Cafeteria Regular\n" +
                "Chance to not consume food when preparing buffs for a quest.\n\n" +
                "Negotiation\n" +
                "1/8th chance to get a 10%/15%/25% discount on buying things.\n\n"
                + "My Tore Celebrity\n" +
                "Garden managers affection goes up 1.5x/2x/3x usual values.\n\n"
                + "Gallery Celebrity\n" +
                "5000/7000/10000 extra Gallery Points on evaluations.\n\n"
                + "Garden Celebrity\n" +
                "1.2x/1.3x/1.5x items received from garden tools.\n\n" +
                "Recovery Items Up\n" +
                "Herb, Potion, Mega Potion and Lifepowder effect 1.1x. 100% Bitterbug and Antidote Herb effectiveness.\n\n" +
                "Blunt Striker\n" +
                "Bowgun Melee damage up (3.0x).\n\n" +
                "Courage\n" +
                "No inching upon being spotted by monsters.\n\n" +
                "Combination Technique [Small]\n" +
                "10% additional combination success rate.\n\n" +
                "Riser [Small]\n" +
                "1.5x iframes during the rising animation after taking a hit.\n\n" +
                "Page 2\n" +
                "Perfect Defense [Small]\n" +
                "Blocking within 3 frames of an attack hitting you will cause no stamina or sharpness decrease and prevent knockback and allow you to immediately evade after the block.\n\n" +
                "Lander\n" +
                "No recovery time after falling, no egg loss on falling.\n\n" +
                "Vine Superhero\n" +
                "No stamina is consumed while climbing.\n\n" +
                "Vine Master\n" +
                "Getting hurt while climbing will not cause you to fall\n\n" +
                "Art of Dancing\n" +
                "Using the 'Dance' action will give +10 attack for one minute. Uses the same buff slot as Power Seeds etc.\n\n" +
                "Combination Celebrity\n" +
                "Combining items has a chance to produce double the usual results quantity wise.\n\n" +
                "Combination Technique [Medium]\n" +
                "15% additional combination success rate.\n\n" +
                "Riser [Medium]\n" +
                "2.0x iframes during the rising animation after taking a hit.\n\n" +
                "Perfect Defense [Medium]\n" +
                "Blocking within 4 frames of an attack hitting you will cause no stamina or sharpness loss and prevent knockback and allow you to immediately evade after the block.\n\n" +
                "Elite Flame\n" +
                "Increases the Friendly Fire (heat up) meter over time instead of by friendly fire when on caravan quests.\n\n" +
                "Page 3\n" +
                "Mine Expert\n" +
                "Pickaxes are less likely to break after use.\n\n" +
                "Insect Expert\n" +
                "Bug nets are less likely to break after use.\n\n" +
                "(Recommended) KO Technique\n+" +
                "Increases stun damage dealt by 1.1x. Stacks with Sigil.\n\n" +
                "Combination Technique [Large]\n" +
                "20% additional combination success rate.\n\n" +
                "Riser [Large]\n" +
                "2.0x iframes during the rising animation after taking a hit.\n\n" +
                "Secret Healing Technique [Small]\n" +
                "1/12th chance of not consuming healing items when used. (Up to 5 times a quest)\n\n" +
                "(Recommended) Perfect Defense [Large]\n" +
                "Blocking within 4 frames of an attack hitting you will cause no stamina or sharpness loss and prevent knockback and allow you to immediately evade after the block. Perfectly timed blocks also cause a Reflect effect which deals 72 motion (no critical hits, no elemental).\n\n" +
                "Unstable Defender [Small]\n" +
                "90% reduction of damage and 20% chance of no damage while blocking\n\n" +
                "Rousing Attacker [Small]\n" +
                "Attacking a monster while you have 50 or lower health a 40% chance to cause you to regain 10 HP. Cannot trigger more than once every 10 seconds. Can trigger up to 10 times in a quest.\n\n" +
                "Revenge![Small]\n" +
                "After getting up from a hit there's a chance (1 x Health Loss % chance) to gain 25 attack, 50 defense and no minor knockback for 20 seconds. Counted as a Power Pill for terms of buff effects and does not overlap\n\n" +
                "Page 4\n" +
                "Hot Master\n" +
                "Grants the effects of Heat Cancel\n\n" +
                "Cold Master\n" +
                "Grants the effects of Cold Cancel\n\n" +
                "Prepared Stance\n" +
                "If you perform the gesture 應戰準備 <act20> for around 30 seconds the attack ceiling on your currently equipped weapon type increases for a fixed duration.\n\n" +
                "Shield Angel\n" +
                "Decreases the amount of damage taken when on support quests on Berserk Raviente.\n\n" +
                "Spear Angel\n" +
                "Increases the amount of damage dealt by Ballistae when playing support on Berserk Raviente.\n\n" +
                "Secret Healing Technique [Medium]\n" +
                "1/11th chance of not consuming healing items when used. (Up to 5 times a quest)\n\n" +
                "Unstable Defender [Medium]\n" +
                "90% reduction of damage and 25% chance of no damage while blocking\n\n" +
                "Rousing Attacker [Medium]\n" +
                "Attacking a monster while you have 50 or lower health a 40% chance to cause you to regain 10 HP. Cannot trigger more than once every 10 seconds. Can trigger up to 15 times in a quest.\n\n" +
                "Revenge! [Medium]\n" +
                "After getting up from a hit there's a chance (1.5 x Health Loss % chance) to gain 25 attack, 50 defense and no minor knockback for 20 seconds. Counted as a Power Pill for terms of buff effects and does not overlap\n\n" +
                "Weapon Art [Small]\n" +
                "Increases the True Raw of your equipped weapon by 1.01x of its base True Raw on all weapon types.\n\n" +
                "Page 5\n" +
                "Bonus Art\n" +
                "Food Effect is not lost after fainting.\n\n" +
                "Secret Healing Technique [Large]\n" +
                "1/10th chance of not consuming healing items when used. (Up to 5 times a quest)\n\n" +
                "Last Minute Ace [Small]\n" +
                "In the last 5 minutes of a quest you get 80% affinity but take 1.3x damage\n\n" +
                "Unstable Defender [Large]\n" +
                "90% reduction of damage and 50% chance of no damage while blocking\n\n" +
                "Rousing Attacker [Large]\n" +
                "Attacking a monster while you have 50 or lower health a 40% chance to cause you to regain 10 HP. Cannot trigger more than once every 10 seconds. Can trigger up to 20 times in a quest.\n\n" +
                "Revenge! [Large]\n" +
                "After getting up from a hit there's a chance (2 x Health Loss % chance) to gain 25 attack, 50 defense and no minor knockback for 20 seconds. Counted as a Power Pill for terms of buff effects and does not overlap\n\n" +
                "Last Minute Ace [Medium]\n" +
                "In the last 5 minutes of a quest you get 90% affinity but take 1.3x damage\n\n" +
                "Weapon Art [Medium]\n" +
                "Increases the True Raw of your equipped weapon by 1.025x of its base True Raw on all weapon types.\n\n" +
                "Wild Awakening\n" +
                "Combination of both Hot and Cold Master skills.\n\n" +
                "Instant Guard Stance\n" +
                "Combination of Weapon Art [Med] and Perfect Defense [Med]\n\n" +
                "Page 6\n" +
                "(Recommended) Shooting Rampage\n" +
                "Increases the True Raw of your equipped ranged weapon by 1.1x of its base True Raw. If using a bowguns your accuracy immediately after shooting is lowered by 1.5x.\n\n" +
                "Natural Recovery [Small]\n" +
                "Using the 'Sleep' gesture will cause your red health to refill up to 5 times.\n\n" +
                "Master Carver [Small]\n" +
                "While carving if you roll the top item in the carve table and it is below 51% you have a 1/10th chance of rerolling with that item removed from the carving pool up to a maximum of 10 times in a quest.\n\n" +
                "Last Minute Ace [Large]\n" +
                "In the last 5 minutes of a quest you get 100% affinity but take 1.3x damage\n\n" +
                "(Recommended) Weapon Art [Large]\n" +
                "Increases the True Raw of your equipped weapon by 1.05x of its base True Raw on all weapon types.\n\n" +
                "Decisive Hunter\n" +
                "Combination of Weapon Art [Med] and KO Technique\n\n" +
                "Natural Recovery [Medium]\n" +
                "Using the 'Sleep' gesture will cause your red health to refill up to 10 times.\n\n" +
                "Master Carver [Medium]\n" +
                "While carving if you roll the top item in the carve table and it is below 51% you have a 1/9th chance of rerolling with that item removed from the carving pool up to a maximum of 15 times in a quest.\n\n" +
                "Goddess of Luck [Small]\n" +
                "1/10th chance to take no damage on up to 5 hits in a quest (Stacks with Divine Protection, Diva Buff and Girly Charms).\n\n" +
                "Self-Defense\n" +
                "Combination of Weapon Art [Med] and Unstable Defender [Med]\n\n" +
                "Page 7\n" +
                "Natural Recovery [Large]\n" +
                "Using the 'Sleep' gesture will cause your red health to refill up to 15 times.\n\n" +
                "(Recommended) Master Carver [Large]\n" +
                "While carving if you roll the top item in the carve table and it is below 51% you have a 1/8th chance of rerolling with that item removed from the carving pool up to a maximum of 20 times in a quest.\n\n" +
                "Goddess of Luck [Medium]\n" +
                "1/9th chance to take no damage on up to 10 hits in a quest (Stacks with Divine Protection, Diva Buff and Girly Charms).\n\n" +
                "Goddess of Luck [Large]\n" +
                "1/8th chance to take no damage on up to 20 hits in a quest (Stacks with Divine Protection, Diva Buff and Girly Charms).";
        }
    }

    public static string GetGameGuildAbout
    {
        get
        {
            return "Create/Join a Guild\r\n・Guilds can be created at HR5 or higher\r\n・You cannot create a guild with the same name as another.\r\n・Creation of a guild and application for membership are carried out by the guild receptionist in Mezeporta.\r\n\r\nMaintaining a Guild\r\n・There are 4 or more characters of Hunter Rank (HR) 3 or higher who have played MHF within the past month in the guild.\r\n・Inactive guilds will be disbanded, checked at the time of maintanence each Wednesday.\r\n・If the conditions are not met at that point, the guild association will send mail to the guild leader.\r\n1st: Attention mail\r\n2nd: Warning mail\r\n3rd: Forced disband mail\r\n・The third time, the group is forcibly disbanded and a disbandment mail is sent to all guild members.\r\n\r\nChanging Guild Leaders\r\nIf the Guild Leader does not log in for 2 consecutive weeks, the second in command will become the new Guild Leader.\r\nThe third player in the guild list will then be made the new second in command.\r\n\r\nRestrictions on disbanding or leaving a guild\r\nGuild leader (disbanded): Cannot create or apply to join a guild for 5 days (120 hours) after disbandment.\r\nGuild member (kicked): Cannot create a guild or apply for membership for 3 days (72 hours) after being kicked out.\r\nGuild members (leave): cannot create a guild or apply for membership for 10 days (240 hours) after leaving a guild.\r\n\r\nAlliance\r\nAlliances can be joined by up to three guilds.\r\nOnce an alliance is formed, the use of alliance chat is enabled.\r\nRegistration for the Hunters' Festival will be on an alliance basis and all guilds will be on the same team.\r\n\r\nGuild Rank\r\nBonuses for increasing hunting party ranks will be available after each scheduled maintenance.\r\nHunting points (RP) will accumulate 1P after logging in for a total of 30 minutes. (15 minutes/1P at authorized NetCafes)\r\nOther RPs can be obtained through official hunting competitions.\r\n\r\nRank\tRP\tCapacity\tFeatures\r\n0\t0\t30\tJump from guild chat / guild list\r\n1\t24\t30\tAlliances, Motto, Guild icon\r\n2\t48\t30\tTent hall, Item box, Rasta, Change Room\r\n3\t96\t40\tGuild Shop, +10 members\r\n4\t144\t40\tGuild Quests, Meals\r\n5\t192\t40\tGuild Pugi +1, Store discount, Pet pugi\r\n6\t240\t40\tPugi shop, Change pugi, Feed pugi\r\n7\t288\t50\tSmall hall, Item box +1pg, +10 members, +10 stamps\r\n8\t360\t50\tAdd 4 types of clothes for Guild Poogie\r\n9\t432\t50\tAdd 2 types of clothes for Guild Poogie\r\n10\t504\t60\tLarge hall, Item box +1pg\r\n+10 members, Bonus menu, +10 stamps\r\n11\t600\t60\tAdd 2nd Guild Poogie\r\n12\t696\t60\tAdd 3rd Guild Poogie\r\n13\t792\t60\tLimited time quests, Tournament quests\r\n14\t888\t60\tG-rank quests\r\n15\t984\t60\tGuild Cooking\r\n16\t1080\t60\tAdventure Cat\r\n17\t1200\t60\tBonus menu slots, Hunting road quests\r\nWeekly Bonus\r\nDepending on the number of logins in the previous week, the following effects will be triggered on partnya\r\nThe guild leader can also spend RP to add more logins.\r\n\r\nGuild Rank\tEffect Name\tEffect\r\n10\tPartnya attack power UP\tAffects the attack power of partnyas during a quest.\r\n10\tPartnya Defense UP\tAffects the amount of damage done to partnyas during a quest.\r\n10\tPartnya Health UP\tAffects the frequency of partnyas withdrawaling during a quest.\r\n17\tPartnya Courage UP\tAffects the frequency of use of partnyas using strong attacks during a quest.\r\n17\tPartnya Reflexes UP\tAffects how often your partnya guards during quests.";
        }
    }

    public static string GetGameGuildPoogies
    {
        get
        {
            return "Poogie\r\nGuild store discount (from guild rank 5)\r\nThe discount is activated by successfully completing the \"pat on the head\" and \"it seems pretty happy\" responses a specified number of times.\r\n\r\n5 times successful\tGuild shop 5% discount\r\n10 times successful\tGuild shop 10% discount\r\n15 times successful\tGuild shop 15% discount\r\nThe effect disappears in any of [12 hours passed / logout / petting for a long time and getting angry] occur.\r\nPoogie Skill (Unlocked at Guild Rank 6)\r\nGiving food and \"seems to love it!\" will activate a skill according to Poogie's clothes with a probability. Only one type of skill can be activated.\r\nOnly the guild leader can purchase and change clothes.\n\n" +
                "Rank 6\n" +
                "Red & White (Poogie Thrift): Flute, Pickaxe and Bugnet 1/4 less likely to break (stacks with Whim)\n" +
                "Naked Emperor (Poogie Discount): Furniture store purchase price (Material/Zenny) 10% discount. Needs Wht Durable Fabricx45, Appropriate Partsx15\n" +
                "(Recommended) Soporific White (Poogie Taijutsu): Halves stamina consumption when evading and guarding. Needs Wht Durable Fabricx75, Flexible Medicinex22, Appropriate Partsx30\n" +
                "(Recommended) Black Green Clash (Poogie Status Attack): 1.125x attribute value for status attacks (Can be used with status skills). Needs Striped Fabricx45, Green Fabricx30\n\n" +
                "Rank 8\n" +
                "Silent Suit (Poogie Reward Technique): 1/8 chance that the next reward won't be given as a quest reward. In addition, the maximum reward frame will also be increased. Needs Wht Durable Fabricx30, Black Fabricx50, Appropriate Partsx20\n" +
                "Bewitching Pink (Poogie Defense): When attacked, 1/4 chance to reduce damage taken by 30% (Can be used with the Protection skill). Needs Wht Durable Fabricx20, Peach Fabricx45\n" +
                "Nostalgic Stripe (Poogie Escape Technique): Halves stamina when running away. Needs Striped Fabricx45, Blue Fabrix25\n" +
                "Soothing Sky (Poogie Transportation): Halves stamina consumption while running while transporting (Can be used with Marathon Runner skill). Needs Wht Durable Fabricx40, Blue Fabricx35, Appropriate Partsx10\n\n" +
                "Rank 9\n" +
                "Gentle Green (Poogie Trap Mastery): 100% success in combining pitfall and shock traps. Needs Green Fabricx35, Blue Fabricx20, Appropriate Partsx20\n" +
                "Restless Brown (Poogie Patience): When attacked, 1/6 chance of gaining super armor (no knockback). Needs Brown Fabricx30, Black Fabricx10, Appropriate Partsx10\n\n" +
                "Clothing material is RARE2 and can be put in the guild box.\r\n\r\nClothing Material\tProduction Materials\r\nWht Tough Fabric\tAnteka Peltx3、W.Velociprey Hidex2、Blangonga Peltx2\r\nAppropriate Parts\tSunstonex2、Sticky Caterpillarx1、Sm Monster Bonex1、Machalite Orex3\r\nStriped Fabric\tHigh Qual Peltx3、Striped Skinx2、Blk Rajang Peltx1、Carpenterbugx2\r\nGreen Fabric\tBuruku Hidex2、Rathian Scalex3、Gendrome Hidex3、Festi Tktx1\r\nFlexible Medicine\tBomb Arrowanax3、Firecell Stonex1、Fire Herbx5、Monster Fluidx2\r\nBlk Fabric\tRemobra Skin+x2、High Qual Peltx3、B.Gravios Shellx1\r\nPeach Fabric\tIoprey Hide+x1、Teostra Shlx1、W.Velociprey Hidex3\r\nBlue Fabric\tVelociprey Scalex3、B.Kut-Ku Scalex2、Rubbery Hidex1\r\nBrown Fabric\tGenprey Hidex4、Diablos Shlx1、Gluehopperx2";
        }
    }

    public string GetGameGuildInfo
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            return s.GuildOptionInfo switch
            {
                "About" => GetGameGuildAbout,
                "Guild Food" => GetGameGuildFood,
                "Guild Poogies" => GetGameGuildPoogies,
                _ => GetGameGuildAbout,
            };
        }
    }

    public static string GetGameRoadDureInfo
    {
        get
        {
            return "The Maximum Cost is 130.\n\n" +
                "Attack\n" +
                "Lv1: Small increase to attack.(+10)\n" +
                "Lv2: Increases attack.(+20)\n" +
                "Lv3: Medium increase to attack.(+30)\n" +
                "Lv4: Large increase to attack.(+50)\n" +
                "Lv5: Very large increase to attack.(+70)\n\n" +
                "Defense\n" +
                "Lv1: Defense +30\n" +
                "Lv2: Defense +50\n" +
                "Lv3: Defense +80\n" +
                "Lv4: Defense +110\n" +
                "Lv5: Defense +150\n\n" +
                "Health Recovery\n" +
                "Lv1: Red Health recovery speed is increased.\n" +
                "Lv2: Red health recovery speed is further increased.\n\n" +
                "Fire Res\n" +
                "Lv1: Fire Res +10\n" +
                "Lv2: Fire Res +15\n" +
                "Lv3: Fire Res +25\n\n" +
                "Water Res\n" +
                "Lv1: Water Res +10\n" +
                "Lv2: Water Res +15\n" +
                "Lv3: Water Res +25\n\n" +
                "Thunder Res\n" +
                "Lv1: Thunder Res +10\n" +
                "Lv2: Thunder Res +15\n" +
                "Lv3: Thunder Res +25\n\n" +
                "Ice Res\n" +
                "Lv1: Ice Res +10\n" +
                "Lv2: Ice Res +15\n" +
                "Lv3: Ice Res +25\n\n" +
                "Dragon Res\n" +
                "Lv1: Dragon Res +10\n" +
                "Lv2: Dragon Res +15\n" +
                "Lv3: Dragon Res +25\n\n" +
                "All Res\n" +
                "Lv1: All Res +5\n" +
                "Lv2: All Res +10\n" +
                "Lv3: All Res +15\n\n" +
                "Technical Skills\n" +
                "Starting Gm Up\n" +
                "Lv1: Increases starting Gm.(+1000Gm)\n" +
                "Lv2: Increases starting Gm significantly.(+3000Gm)\n\n" +
                "Hunting Road Points Up\n" +
                "Lv1: Increases Road Point earning slightly (+10%).\n" +
                "Lv2: Increases Road Point earning (+20%).\n" +
                "Lv3: Increases Road Point earning greatly (+40%).\n\n" +
                "Bonus Stages Up\n" +
                "Lv1: Increases the likelihood of getting Bonus Stages slightly. Stacks with other players on road.\n" +
                "Lv2: Increases the likelihood of getting Bonus Stages. Stacks with other players on road.\n\n" +
                "Resurrection Knowledge\n" +
                "Lv1: Increases the number of times you can faint on the road before failing (+1 Cart).\n\n" +
                "Advancement Knowledge\n" +
                "Lv1: Attack increases every 5 stages on the Road. Floor 6 will give +20 Attack while every 5th floor after will grant +10 Attack stopping after floor 26. Maximum buff of 60 Attack for all floors above 26.\n" +
                "Lv2: Attack increases every 5 stages on the Road. Floor 6 will give +40 Attack while every 5th floor after will grant +10 Attack stopping after floor 26. Maximum buff of 80 Attack for all floors above 26.\n" +
                "Lv3: Attack increases every 5 stages on the Road. Floor 6 will give +60 Attack while every 5th floor after will grant +10 Attack stopping after floor 26. Maximum buff of 100 Attack for all floors above 26.\n\n" +
                "Last Stand\n" +
                "Lv1: Increases affinity by +30% and Attack by +80 but causes you to have a single faint on Road regardless of other skills.\n" +
                "Lv2: Increases affinity by +50% and Attack by +120 but causes you to have a single faint on road regardless of other skills.\n\n" +
                "Duremudira Skills\n" +
                "Care\n" +
                "Lv1: Slightly increases speed of revivals and the amount of health left after being revived.\n" +
                "Lv2: Increases speed of revivals and the amount of health left after being revived.\n" +
                "Lv3: Greatly increases speed of revivals and the amount of health left after being revived.\n\n" +
                "Pharmacist\n" +
                "Lv1: Increases the number of revival items you can carry by 1.\n" +
                "Lv2: Increases the number of revival items you can carry by 2.\n" +
                "Lv3: Increases the number of revival items you can carry by 4.\n\n" +
                "Virus Protection\n" +
                "Lv1: Increases resistance to Deadly Poison slightly.\n" +
                "Lv2: Increases resistance to Deadly Poison.\n" +
                "Lv3: Increases resistance to Deadly Poison greatly.\n\n" +
                "Frost Protection\n" +
                "Lv1: Increases resistance to Powerful Frost slightly.\n" +
                "Lv2: Increases resistance to Powerful Frost.\n" +
                "Lv3: Increases resistance to Powerful Frost greatly.\n\n" +
                "Gatekeeper Offensive\n" +
                "Lv1: Increases attack when facing Duremudira by +50 true raw.\n" +
                "Lv2: Increases attack when facing Duremudira by +75 true raw.\n" +
                "Lv3: Increases attack when facing Duremudira by +100 true raw.\n" +
                "Lv4: Increases attack when facing Duremudira by +150 true raw.\n" +
                "Lv5: Increases attack when facing Duremudira by +200 true raw.\n\n" +
                "Gatekeeper Defensive\n" +
                "Lv1: Increases defense when facing Duremudira.\n" +
                "Lv2: Increases defense when facing Duremudira.\n" +
                "Lv3: Increases defense when facing Duremudira.\n" +
                "Lv4: Increases defense when facing Duremudira.\n" +
                "Lv5: Increases defense when facing Duremudira.\n\n" +
                "Tower Weapons\n" +
                "The best one is Blue Tower. You generally want the upgrades to be 1 into sharpness, max poison, 4 into element, rest into raw, with a Skill Slots Up sigil.\n\n" +
                "Duremudira\n" +
                "In terms of HP, Second District Duremudira starts at 150,000 Effective HP. Deal 51k to reach 2nd phase (10k True HP), then deal 83,333 (20k True HP) to reach 3rd phase, which has 100k hp left, for a total hunt of 284,333 EHP.";
        }
    }

    public static string GetGameZenithSkills
    {
        get
        {
            return "Skill Slots Up\n" +
                "Available Skill Slots go up by 1/2/3/4/5/6/7. Stacks with the +1 or +2 skill slots from having 3 or 5 standard G Rank pieces (including Z, ZF, ZY, ZX and ZP)\n\n" +
                "Flash Conversion\n" +
                "Flash Conversion Up+1: Increases attack based on your weapon's natural affinity (5 x √Base Affinity, always rounded down before addition). Example: 50% base affinity = 5 x √50 = 7.07 x 10 = 35. This only uses the true base affinity of your weapon. Sigils, Skills, SR Skills and the +5-10% from having above blue sharpness do not count towards the increase. (The sharpness bonus is always displayed\n" +
                "Flash Conversion Up+2: Increases attack based on your weapon's natural affinity (10 x √Base Affinity, always rounded down before addition). Example: 50% base affinity = 10 x √50 = 7.07 x 10 = 70. This only uses the true base affinity of your weapon. Sigils, Skills, SR Skills and the +5-10% from having above blue sharpness do not count towards the increase. (The sharpness bonus is always displayed\n\n" +
                "Stylish Assault\n" +
                "(Recommended) Stylish Assault Up+1: Boosts Attack by +20 per evade up to a maximum of +220. The duration of the buff resets every evasion. Your attack being overbuffed is indicated by the aura becoming yellow. Synergizes well with Stylish Up.\n" +
                "Stylish Assault Up+2: Boosts Attack by +40 per evade up to a maximum of +220. The duration of the buff resets every evasion. Your attack being overbuffed is indicated by the aura becoming yellow. Synergizes well with Stylish Up.\n\n" +
                "Dissolver Up\n" +
                "Lowers the Elemental Hitbox requirements for Dissolver. (15 Weakness down from 20). Unnecessary with Solid Determination.\n\n" +
                "Thunder Clad\n" +
                "Thunder Clad Up+1: Increases the active duration of Thunder Clad to 80 seconds (+20)\n" +
                "Thunder Clad Up+2: Increases the active duration of Thunder Clad to 120 seconds (+60)\n\n" +
                "Ice Age Up\n" +
                "Increases the maximum duration that the Ice Age aura can stay at a stage by +3 seconds. Only affects top tier stage (i.e. Stage 3 is 9>12 seconds but if it hits stage 2 that is 10 seconds for decay, not 13).\n\n" +
                "Hearing Protection\n" +
                "Hearing Protection Up+1: Increases Hearing Protection by one tier, allows blocking of Ultra Roars with Super High-Grade Earplugs.\n" +
                "Hearing Protection Up+2: Increases Hearing Protection by two tiers, allows blocking of Ultra Roars with High-Grade Earplugs.\n" +
                "Hearing Protection Up+3: Allows blocking of Ultra Roars with Earplugs.\n\n" +
                "Wind Res Up\n" +
                "Wind Res Up+1: Increases Wind Res by 1 tier, allows blocking of Ultra Wind Pressure with Violent Wind Breaker\n" +
                "Wind Res Up+2: Increases Wind Res by 2 tiers, allows blocking of Ultra Wind Pressure with Dragon Wind Breaker\n" +
                "Wind Res Up+3: Increases Wind Res by 3 tiers, allows blocking of Ultra Wind Pressure with Wind Res (Large)\n" +
                "Wind Res Up+4: Allows blocking of Ultra Wind Pressure with Wind Res (Small)\n\n" +
                "Quake Res\n" +
                "Quake Res Up+1: Increases Quake Res by 1 tier, allows blocking of Ultra Tremor with Quake Res+1\n" +
                "Quake Res Up+2: Allows blocking of Ultra Tremor with Quake Res+1.\n\n" +
                "Poison Res\n" +
                "Poison Res Up+1: Upgrades Poison Res by 1 tier, enables Super Poison Damage Halved with Poison Immunity. There is no immunity for Super Poison. Increases level of the hybrid skill Status Immunity but does not increase the skill granted by Assistance or Thunder Clad.\n" +
                "Poison Res Up+2: Super Poison Damage Halved. There is no immunity for Super Poison. Increases level of the hybrid skill Status Immunity but does not increase the skill granted by Assistance or Thunder Clad.\n\n" +
                "Paralysis Res\n" +
                "Paralysis Res Up+1: Upgrades Paralysis Res by 1 tier, enables Super Paralysis Halved with Paralysis Immunity. There is no immunity for Super Paralysis. Increases level of the hybrid skill Status Immunity but does not increase the skill granted by Assistance or Thunder Clad.\n" +
                "Paralysis Res Up+2: Super Paralysis Halved. There is no immunity for Super Paralysis. Increases level of the hybrid skill Status Immunity but does not increase the skill granted by Assistance or Thunder Clad.\n\n" +
                "Sleep Res\n" +
                "Sleep Res Up+1: Upgrades Sleep Res by 1 tier, enables Super Sleep Halved with Sleep Immunity. There is no immunity for Super Sleep. Increases level of the hybrid skill Status Immunity but does not increase the skill granted by Assistance or Thunder Clad.\n" +
                "Sleep Res Up+2: Super Sleep Halved. There is no immunity for Super Sleep. Increases level of the hybrid skill Status Immunity but does not increase the skill granted by Assistance or Thunder Clad.\n\n" +
                "Vampirism\n" +
                "Vampirism Up: Increases level of Vampirism by one. Increases leech rate to 100% if you already have Vampirism+2. Leeching with Vampirism+3 regains the same amount of health as Vampirism+2, the buff is purely to the leech chance and is not much of a buff compared to that of increasing Vampirism+1 to +2.\n\n" +
                "Drug Knowledge\n" +
                "Drug Knowledge Up: Status weapons always inflict 42% of their original status value (Up from 38%). Does not increase the amount of raw gained from the original status value beyond 1/4th. Normal status infliction rate is 33% chance of 100% status points.\n\n" +
                "Assistance\n" +
                "Assistance Up: Party members within the area of effect of Assistance get Status Immunity (Myriad). Immunity skill still only affects other hunters, does not affect the person with the skill active.\n\n" +
                "Bullet Saver\n" +
                "Bullet Saver Up+1: Upgrades Bullet Saver by 1 tier. If you have Bullet Saving Master, further increases the rate at which you save bullets or coatings to around 46.4%.\n" +
                "Bullet Saver Up+2: Further increases the rate at which you save bullets or coatings to around 46.4%.\n\n" +
                "Guard\n" +
                "(Recommended for Lance/Gunlance) Guard Up+1: Upgrades Guard by one level. If you already have Guard+2, decreases the amount of knockback, stamina loss and health loss while guarding and increases the size of the guard window. Lances and Gunlances only: Only applies when exceeding Guard+2, Guarding affects 360 degrees around you, rear hits still cause knockdown. Heavy and Ranged Guards can block attacks that were previously unblockable. Lance's Guard Meter fills faster (Extreme Style). Gunlances have lowered Wyvern Fire cooldowns. Synergizes well with Reflect Up, Obscurity Up and Rush Up.\n" +
                "Guard Up+2: Decreases the amount of knockback, stamina loss and health loss while guarding and increases the size of the guard window. Lances and Gunlances only: Only applies when exceeding Guard+2, Guarding affects 360 degrees around you, rear hits still cause knockdown. Heavy and Ranged Guards can block attacks that were previously unblockable. Lance's Guard Meter fills faster (3 Hits > 2 Hits per phial). Gunlances have lowered Wyvern Fire cooldowns (10s faster, 5s with Hiden). Synergizes well with Reflect Up, Obscurity Up and Rush Up.\n\n" +
                "Adaptation\n" +
                "Adaptation Up+1: Upgrades Adaptation by one level. If you already have Adaptation+2, increases the % of the adapted hitbox's value used to 90% for Cutting or Impact and 81% for Ranged Weapons.\n" +
                "Adaptation Up+2: Increases the % of the adapted hitbox's value used to 90% for Cutting or Impact and 81% for Ranged Weapons.\n\n" +
                "Encourage\n" +
                "Encourage Up+1: Upgrades Encourage+1 to Encourage+2. If you already have Encourage+2, adds party wide Marathon Runner and Stamina Recovery Up Large.\n" +
                "Encourage Up+2: Adds party wide Marathon Runner and Stamina Recovery Up Large.\n\n" +
                "Reflect\n" +
                "(Recommended) Reflect Up+1: Upgrades Reflect by 1 tier. Going beyond Reflect+3 increases the motion value of the Reflect+3 motion (48 > 68). Also buffs perfect guard reflects while active (72 > 92). The hit uses your current attack and sharpness values and inflicts impact based damage. Reflect motions cannot deal critical hits, inflict status damage or deal elemental damage. Consumes 1 sharpness on reflection hitting a monster. Does not benefit from Fencing+2 or utilize sword crystals if loaded. This skill can trigger on every part of any attacks which hit multiple times without any cooldown period. Does not buff the Perfect Guard reflect without actually having Reflect+3 active alongside Reflect Up. Synergizes well with Guard Up, Rush Up and Obscurity Up.\n" +
                "Reflect Up+2: Upgrades Reflect by 2 tiers. Going beyond Reflect+3 increases the motion value of the Reflect+3 motion (48 > 68). Also buffs perfect guard reflects while active (72 > 92). The hit uses your current attack and sharpness values and inflicts impact based damage. Reflect motions cannot deal critical hits, inflict status damage or deal elemental damage. Consumes 1 sharpness on reflection hitting a monster. Does not benefit from Fencing+2 or utilize sword crystals if loaded. This skill can trigger on every part of any attacks which hit multiple times without any cooldown period. Does not buff the Perfect Guard reflect without actually having Reflect+3 active alongside Reflect Up. Synergizes well with Guard Up, Rush Up and Obscurity Up.\n" +
                "Reflect Up+3: Increases the motion value of the Reflect+3 motion (48 > 68). Also buffs perfect guard reflects while active (72 > 92). The hit uses your current attack and sharpness values and inflicts impact based damage. Reflect motions cannot deal critical hits, inflict status damage or deal elemental damage. Consumes 1 sharpness on reflection hitting a monster. Does not benefit from Fencing+2 or utilize sword crystals if loaded. This skill can trigger on every part of any attacks which hit multiple times without any cooldown period. Does not buff the Perfect Guard reflect without actually having Reflect+3 active alongside Reflect Up. Synergizes well with Guard Up, Rush Up and Obscurity Up.\n\n" +
                "Stylish\n" +
                "(Recommended for evasion playstyle) Stylish Up: Successful evades cause your weapon to use no sharpness for a fixed number of attacks as well as regaining the usual sharpness from Stylish and releasing an Area of Effect attack with a motion value of 30. Amount of hits that do not consume sharpness varies based on the weapon type in use.\nDS: 5 hits.\nSnS, Tonfa: 4 Hits.\nLS, Lance, Swaxe F, Magnet Spike: 3 Hits.\nGS, Hammer, HH, GL: 2 Hits.\nUsing Consumption Slayer reduces this to 1 hit for everything except SnS and DS which have 2 hits. Stylish Up AoE Motion cannot deal critical hits, inflict status damage or deal elemental damage. The motion does not benefit from Fencing+2 nor does it utilize sword crystals if they are loaded. Synergizes well with Stylish Assault Up.\n" +
                "Vigorous\n" +
                "Vigorous Up: Multiplies attack by 1.15x when your HP is over 100. Adds +100 attack for Blademasters or +50 attack for Gunners if your HP bar is also completely filled.\n\n" +
                "Obscurity\n" +
                "(Recommended for parry/block playstyle) Obscurity Up: Reduces total attack steps to 6 for maximum buff and allows for sharpness recovery at maximum attack buff. Perfect guards go up three attack steps (i.e. 2 perfect guards is maximum attack buff) and recover additional sharpness while maxed. Recovering sharpness with Gunlance while in heat blade mode reduces the sharpness loss on deactivation (20 guard or 5 perfect guards negates all 100 sharpness loss).\nAttack Increase:\nSnS, Lance, GL, Tonfa: 70 / 140 / 210 / 240 / 270 / 300\nGS, Swaxe F, Magnet Spike: 50 / 100 / 150 / 175 / 200 / 225\nLS: 30 / 60 / 90 / 110 / 130 / 150\nSharpness Recovery:\nSnS: 4 / 12\nLance: 2 / 10\nGunlance: 5 / 20\nGS: 5 / 15\nSwaxe F: 4 / 10\nTonfa: 5 / 13\nLS: 5 / 11\nMagnet Spike: 5 / 10\n\n" +
                "Soul\n" +
                "(Recommended for HH) Soul Up: Soul can be applied by using items, attacks will not stagger other players.\nRed Soul: +100 Attack on both user and players struck. Zero stamina consumption for running with weapons unsheathed.\nBlue Soul: +200 Defense on both user and players struck. Health Recovery effects, removes most abnormal status effects (Zenith Blights cannot be removed.)\nThis Skill works with all sources of Soul meaning you can stack Blue Soul with Red Soul from Blazing Grace and buff both. Attack is a final addition that is always the stated value and completely unaffected by other skills and multipliers.\n\n" +
                "Rush\n" +
                "(Recommended) Rush Up: Adds a third stage to Rush that is activated from the 2nd stage by attacking or guarding. Strictly time limited and increases attack further and grants infinite stamina during its duration. Grants +70 true raw (Total +200) and has a duration of 30 seconds. Indicated by an intensified version of the current aura with additional lightning effects. Infinite stamina works with Combat Supremacy and Starving Wolf but does not refill, meaning you would need to activate it before the bar empties to have stamina for evading, etc. Synergizes well with Guard Up, Reflect Up and Obscurity Up.\n\n" +
                "Ceaseless\n" +
                "(Recommended) Ceaseless Up: Adds a third stage to Ceaseless with higher Affinity and additional Critical Multiplier. All stages require less hits to be reached and the skill goes down one level on timing out instead of losing all levels (e.g. stage 2 downgrades to stage 1 instead of no stages). Third stage is indicated by a red ceaseless aura instead of the standard white.\nFirst Phase: +35% Affinity, +0.1x Critical Multiplier\nSecond Phase: +50% Affinity, +0.15x Critical Multiplier\nThird Phase: +60% Affinity, +0.20x Critical Multiplier.\nEach weapon requires a different number of hits to progress in stages.\nSnS: 10s, 15/35/54 hits\nDS: 11s, 12/29/45 hits\nGS: 15s, 7/16/26 hits\nLS: 12, 11/29/47 hits\nHammer: 15s 6/21/36 hits\nHH: 14s, 8/24/40 hits\nLance, GL, Swaxe F: 12s, 10/27/44 hits\nTonfa: 11s, 11/27/43 hits\nMagnet Spike: 12s, 8/23/38 hits\nLBG: 11s, 27/74/120 hits\nHBG: 13s, 21/57/93 hits\nBow: 12s, 12/36/60 hits\nReflect and Stylish Up count towards these hit totals but Fencing+2's extra hit does not. Both affinity and multiplier stack with similar buffs (e.g. Issen+3 and second phase Ceaseless gives +70% Affinity and a multiplier of 1.65x)"
                ;
        }
    }

    public static string GetGameStyleRankInfo
    {
        get
        {
            return "You can equip your first skill by going into a weapon's Book of Secrets menu and select one of the Special Effect options followed by one of the skills above. After you hit GSR100 you will be able to equip two skills and up until that point you will be able to equip 1.\n\n" +
                "At HR5 you gain the basic Defense Skill, at HR6 you gain all the various Elemental Res skills and the first version of Sharpening Up and at HR7 you get access to Affinity Up and max Sharpening Up. All of the Res and Defense skills progress naturally as you rank up in G Style Rank with some having the requirement of GSR999 in the weapon or multiple weapons to be unlocked or maxed out.\n\n" +
                "Every GSR999\n" +
                "Passive Master\n" +
                "Causes any monster attacks that would normally leave you completely knocked down to be partially recovered from (i.e. you land on your feet instead of lying down and slowly getting up.)\n\n" +
                "Secret Technique\n" +
                "An ability that can be used once a day after 12:00 that deals massive damage after a long wind up animation. Bound to the Kick button or key. Increases attack after use for the rest of that quest's duration.\n\n" +
                "11x GSR999\n" +
                "Soul Revival\n" +
                "An ability that can be triggered once per quest that revives you after hitting 0 HP once and fills your health bar. Disabled with Determination. Unlocked on GSR999 weapons after you have 11 GSR999 total.\n\n" +
                "### Special Effects (G Style Rank)\r\n\r\n|GSR| Effect|\r\n|--|------|\r\n|0|Def+100, Ele Res+20, All Res+10, Affinity+20|\r\n|10|Def+1|\r\n|20|Fire Res+2|\r\n|30|Conquest Def+10|\r\n|40|Water Res+2|\r\n|50|Conquest Atk+2|\r\n|60|Def+1|\r\n|70|Conquest Def+10|\r\n|80|Thunder Res+2|\r\n|90|Def+1|\r\n|100|Conquest Atk+2, 2 special effects can be set|\r\n|110|Def+1|\r\n|120|Ice Res+2|\r\n|130|Conquest Def+10|\r\n|140|Dragon Res+2|\r\n|150|Conquest Atk+2|\r\n|160|Def+1|\r\n|170|Conquest Def+10|\r\n|180|All Res+1|\r\n|190|Def+1|\r\n|200|Conquest Atk+2|\r\n|210|Def+1|\r\n|220|Fire Res+2|\r\n|230|Conquest Def+10|\r\n|240|Water Res+2|\r\n|250|Conquest Atk+2|\r\n|260|Def+1|\r\n|270|Conquest Def+10|\r\n|280|Thunder Res+2|\r\n|290|Def+1|\r\n|300|Conquest Atk+2|\r\n|310|Def+1|\r\n|320|Ice Res+2|\r\n|330|Conquest Def+10|\r\n|340|Dragon Res+2|\r\n|350|Conquest Atk+2|\r\n|360|Def+2|\r\n|370|Conquest Def+10|\r\n|380|All Res+1|\r\n|390|Def+2|\r\n|400|Conquest Atk+2|\r\n|410|Def+2|\r\n|420|Fire Res+2|\r\n|430|Conquest Def+10|\r\n|440|Water Res+2|\r\n|450|Conquest Atk+2|\r\n|460|Def+2|\r\n|470|Conquest Def+10|\r\n|480|Thunder Res+2|\r\n|490|Def+2|\r\n|500|Conquest Atk+2|\r\n|510|Def+2|\r\n|520|Ice Res+2|\r\n|530|Conquest Def+10|\r\n|540|Dragon Res+2|\r\n|550|Conquest Atk+2|\r\n|560|Def+2|\r\n|570|Conquest Def+10|\r\n|580|All Res+1|\r\n|590|Def+2|\r\n|600|Conquest Atk+2|\r\n|610|Def+2|\r\n|620|Fire Res+2|\r\n|630|Conquest Def+10|\r\n|640|Water Res+2|\r\n|650|Conquest Atk+2|\r\n|660|Def+2|\r\n|670|Conquest Def+10|\r\n|680|Thunder Res+2|\r\n|690|Def+2|\r\n|700|Conquest Atk+2|\r\n|710|Def+2|\r\n|720|Ice Res+2|\r\n|730|Conquest Def+10|\r\n|740|Ice Res+2|\r\n|750|Conquest Atk+2|\r\n|760|Def+2|\r\n|770|Conquest Def+10|\r\n|780|All Res+1|\r\n|790|Def+2|\r\n|800|Conquest Atk+4|\r\n|810|Def+2|\r\n|820|Fire Res+2|\r\n|830|Conquest Def+10|\r\n|840|Water Res+2|\r\n|850|Conquest Atk+4|\r\n|860|Def+2|\r\n|870|Conquest Def+10|\r\n|880|Thunder Res+2|\r\n|890|Def+2|\r\n|900|Conquest Atk+4|\r\n|910|Def+2|\r\n|920|Ice Res+2|\r\n|930|Conquest Defense+10|\r\n|940|Dragon Res+2|\r\n|950|Conquest Atk+4|\r\n|960|Def+2|\r\n|970|Conquest Def+10|\r\n|980|All Res+1|\r\n|990|Def+2|\r\n|999|Passive Master, Conquest Atk+4|\r\n|x11 GSR999|Soul Revival, Conquest Atk Base 100, Conquest Def Base 300, G Rank Weapon unlock bonuses for conquest skills (+10 Def, +5 Atk each)|\r\n\r\n### GSR Weapon Unlock Bonus\r\n\r\n|Unlocks| Bonus|\r\n|--|------|\r\n|11|None|\r\n|12|Affinity+2, Ele Res+2, All Res+2, Def+10, Conquest Atk Base 100+5, Conquest Def Base 300+10|\r\n|13|Affinity+2, Ele Res+2, All Res+2, Def+10, Conquest Atk Base 100+5, Conquest Def Base 300+10|\r\n|14|Affinity+2, Ele Res+1, All Res+1, Def+10, Conquest Atk Base 100+5, Conquest Def Base 300+10|";
        }
    }

    public static string GetGameDivaInfo
    {
        get
        {// TODO: missing gems?
            return "Prayer Gems\nRinging Prayer Gem\n" +
                "Adds new items to the GCP store based on level.\n\n" +
                "Elegance Prayer Gem\n" +
                "Adds passive HP recovery to all quests.\n\n" +
                "Heavy Thunder Prayer Gem\n" +
                "Elemental damage increases based on level.\n\n" +
                "Windstorm Prayer Gem\n" +
                "Sharpness does not decrease with blademaster weapons. Works for 5, 10 or 20 quests depending on level during the prayer active window.\n\n" +
                "Cutting Edge Prayer Gem\n" +
                "Increases the amount of raw damage dealt by a cutting weapon by adjusting hitboxes to be weaker against the damage type.\n\n" +
                "Status Length Prayer\n" +
                "Increases the duration of status effects on monsters.\n\n" +
                "Rising Bullet Prayer Gem\n" +
                "Increases the amount of raw damage dealt by a ranged weapon by adjusting hitboxes to be weaker against the damage type.\n\n" +
                "Severing Power Prayer Gem\n" +
                "Tails can be cut with any damage type.\n\n" +
                "Powerful Strikes Prayer Gem\n" +
                "Increases affinity of all weapons based on the level of the song.\n\n" +
                "Protection Prayer Gem\n" +
                "Gives Divine Protection, Goddess' Embrace or Soul Revival based on level.\n\n" +
                "Mobilization Prayer Gem\n" +
                "Attack will go up based on the number of human hunters in a quest.\n\n" +
                "Unshakable Prayer Gem\n" +
                "Monsters cannot flee if in the same area as a hunter.\n\n" +
                "Blunt Prayer Gem\n" +
                "Increases the amount of raw damage dealt by an impact weapon by adjusting hitboxes to be weaker against the damage type.\n\n" +
                "Diva Questline\n" +
                "Unless rank is specified the monsters are any rank. Your partner needs to be PR81 to progress through these quests. Do the special quests for PRP and give it the HRP tickets you get from progressing by choosing the final option followed by the first option on your partner in the smith or your house. If you find yourself unable to progress you probably need to talk to one of the NPCs who gave you the task again (cats etc.). Be sure to also look for monster names in the text if you can't progress after killing one, you might be on a lower step than you thought. Complete Chapter 3 to unlock Diva Song.\n\n" +
                "Chapter 1\n" +
                "Part 1: Deliver 1 Thin Jack Mackerel (薄竹筴魚). Deliver 1 Lazurite Jewel (琉璃原珠)\n\n" +
                "Part 2: Hunt 1 White Monoblos. Return to the Diva Hall\n\n" +
                "Part 3: Talk to the Guild Mistress. Hunt 1 Yama Tsukami. Talk to the Legendary Rasta Edward (Lance User)\n\n" +
                "Part 4: Talk to the Guild Mistress. Hunt 1 Chameleos. Talk to the Legendary Rasta Edward\n\n" +
                "Part 5: Talk to the Guild Mistress. Hunt 1 Yama Tsukami. Return to the Diva Hall. Claim the items you need to deliver from the Hunter Challenge, you don't need to farm a million Kelbi.\n\n" +
                "Rewards: Diva Armour Materials. Items to deliver in Chapter 2 (Hunter Challenge Reward)\n\n" +
                "Chapter 2\n" +
                "Part 1: Deliver 30 Kelbi Horns (精靈鹿的角). Deliver 20 Chaos Shrooms (混沌茸). Deliver 5 Kirin Azure Horns (麒麟的蒼角). Items delivered above are returned to you\n\n" +
                "Part 2: Hunt 3 Cephadromes. Deliver 10 Dragon Seeds (屠龍果實). Hunt 2 Lao Shan Lungs. Return to the Diva Hall. Talk to the Legendary Rastas Edward and Frau (DS user)\n\n" +
                "Part 3: Talk to the Legendary Rasta Frau. Return to the Diva Hall. Hunt 1 Baruragaru. Return to the Diva Hall. Talk to the Legendary Rasta Frau. Return to the Diva Hall\n\n" +
                "Rewards: Diva HC Armour Materials" +
                "Chapter 3\n" +
                "Part 1: Hunt 1 Teostra. Return to the Diva Hall. Talk to the Legendary Rasta Frau\n\n" +
                "Part 2: Go to the Blacksmith. Return to the Diva Hall. Hunt 3 Rukodioras\n\n" +
                "Part 3: Hunt 1 Anorupatisu\n\n" +
                "Part 4: Hunt 1 Rebidiora\n\n" +
                "Rewards: Diva G Rank Weapon Materials. Diva Weapon Gem (1st Series). 5 Diva Song Gems (Hunter Challenge Reward). 5 Warm Honey Tea (Give the Diva as a gift then Hunter Challenge Reward). Completion of this Chapter unlocks the Diva Song Buffs. Cram her full of warm honey tea and fluffy cakes to max its effects. Your Discord RPC shows the current Bond when you are in the Diva Hall, the maximum is 999.\n\n" +
                "Chapter 4\n" +
                "Part 1: Hunt 1 Berukyurosu. Hunt 1 Doragyurosu\n\n" +
                "Part 2: Deliver 1 Saint Ore (純聖礦石). Hunt 1 Hyujikiki. Hunt 1 Giaorugu\n\n" +
                "Part 3: Speak to the Town Square Cats three times. Hunt 2 Gougarfs\n\n" +
                "Part 4: Talk to NPC in Blacksmith. Solo Hunt 1 Gurenzeburu\n\n" +
                "Part 5: Talk to Guild Mistress. Hunt 1 Pokaradon. Hunt 1 Midogaron. Talk to NPC next to Guild Hall entrance\n\n" +
                "Rewards: Diva HC Armour Materials\n\n" +
                "Chapter 5\n" +
                "Part 1: Hunt 1 Farunokku\n\n" +
                "Part 2: Hunt 2 Baruragaru (Return to Fountain between the two)\n\n" +
                "Part 3: Hunt 1 Rebidiora\n\n" +
                "Part 4: Hunt 1 Zerureusu\n\n" +
                "Rewards: Diva Weapon Gem (1st Series)\n\n" +
                "Chapter 6\n" +
                "Part 1: Hunt 1 Akantor\n\n" +
                "Part 2: Hire a partner if you don't have one and then talk to them in your house. Return to the Diva Hall. Talk to partner in house, return to Diva Hall.\n\n" +
                "Part 3: ※Partner must be at least PR31 to proceed. Hunt 1 G Rank Yian Kut-ku with partner present.\n\n" +
                "Part 4: ※Partner must be at least PR51 to proceed. Hunt 1 Pokaradon with partner present.\n\n" +
                "Part 5: ※Partner must be at least PR81 to proceed. Hunt 1 Midogaron with partner present. (Talk to partner in house and return to Diva Hall before leaving on quest)\n\n" +
                "Rewards: Diva Armour Materials\n\n" +
                "Chapter 7\n" +
                "Part 1: Talk to Blacksmith and return to Diva Hall. Hunt 1 Rebidiora\n\n" +
                "Part 2: Hunt 2 G Rank HC Gurenzeburu (Return to Fountain between the two)\n\n" +
                "Part 3: Hunt 1 Taikun Zamuza\n\n" +
                "Part 4: Hunt 1 Meraginasu\n\n" +
                "Rewards: Diva Weapon Gem (1st Series)\n\n" +
                "Chapter 8\n" +
                "Part 1: Speak to Blacksmith and return to Diva Hall\n\n" +
                "Part 2: Deliver 3 Grease Stone (白鳥石) and 1 Atarka Ore (亞達爾純礦石). You can mine the ores in the G Rank Flower Field or simply buy them for 235 GCP total. Hunt 1 Forokururu\n\n" +
                "Part 3: You need to craft the Prototype Tonfas at this point. Kill 3 Aptonoth in the preset quest\n\n" +
                "Part 4: Hunt 1 Yian Kut-Ku (Does not need to be with Tonfas)\n\n" +
                "Rewards: Ores spent in part 2 (Hunter Challenge Reward). Used to be ability to craft Tonfas.\n\n" +
                "Chapter 9\n" +
                "Part 1: Deliver 1 Teostra Miracle Wing (Supremacy Teo)\n\n" +
                "Part 2: Hunt 2 G Rank Velocidrome\n\n" +
                "Part 3: Hunt 1 Meraginasu\n\n" +
                "Part 4: Speak to Gin (Hammer Rasta)\n\n" +
                "Rewards: Diva G Rank Armour Materials\n\n" +
                "Chapter 10\n" +
                "Part 1: Talk to Guild Master. Hunt 1 Monoblos\n\n" +
                "Part 2: Hunt 1 Gou Lunastra\n\n" +
                "Part 3: Speak to the Guild Mistress\n\n" +
                "Part 4: Hunt 1 Anorupatisu (Preset Quest). ※ Everyone must use Tonfas for this mission . (Restricted equipment disables AI outside of Legendaries)\n\n" +
                "Rewards: Diva Weapon Materials (1st Series) (2 Gems with Hunter Challenge Reward)\n\n" +
                "Chapter 11\n\n" +
                "Part 1: Talk to Guild Mistress, Return to Diva Hall\n\n" +
                "Part 2: Capture 1 Forokururu\n\n" +
                "Part 3: Speak to Leila (Tonfa Legendary). Solo Hunt 1 Diorex. Speak to Leila. Return to the Diva Hall\n\n" +
                "Part 4: Hunt 1 Burst Species Meraginasu\n\n" +
                "Rewards: Diva Armour Materials\n\n" +
                "Chapter 12\n" +
                "Part 1: Hunt 1 G Rank Gold Rathian, talk to cats and return to Diva Hall\n\n" +
                "Part 2: Speak to Leila and return to the Diva Hall\n\n" +
                "Part 3: Hunt 1 Inagami\n\n" +
                "Part 4: Hunt 1 G Rank Inagami (Preset quest with set equipment, AI outside of Legendaries is disabled)\n\n" +
                "Rewards: Diva Weapon Gem (2nd Series)\n\n" +
                "Chapter 13\n" +
                "Part 1: Hunt 1 Giaorugu\n\n" +
                "Part 2: Hunt 1 G Rank Gravios\n\n" +
                "Part 3: Speak to Leila and return to the Diva Hall. Speak to the Blacksmith\n\n" +
                "Part 4: Hunt 1 G Rank Forokururu. Hunt 1 G Rank HC Rajang\n\n" +
                "Rewards: Diva HC Armour Materials\n\n" +
                "Chapter 14\n" +
                "Part 1: Hunt 1 Red Lavasioth (Training Quest on Black Quest NPC)\n\n" +
                "Part 2: Speak to Flora (SnS Legendary) and return to Diva Hall. Hunt 1 Hyujikiki\n\n" +
                "Part 3: Hunt 1 Inagami\n\n" +
                "Part 4: Deliver 3 Herbal Medicine G (中藥G). (Can be bought in Guild Hall for Guild Tix)\n\n" +
                "Rewards: Diva Weapon Gem (2nd Series)\n\n" +
                "Chapter 15\n" +
                "Part 1: Talk to Guild Master. Return to fountain. Hunt 1 G Rank White Espinas.\n\n" +
                "Part 2: Hunt 1 G Rank Baruragaru\n\n" +
                "Part 3: Hunt 1 G Rank Akura Jebia\n\n" +
                "Part 4: Hunt 1 Burst (G Rank) Garuba Daora\n\n" +
                "Rewards: Diva Armour Materials. Diva Weapon Gem (2nd Series)"
                ;
        }
    }

    public static string GetGameGuildFood
    {
        get
        {
            return "Guild Cooking\r\nGuild cooking is available at guild rank 15, it is a mini-game performed by up to four people.\r\nCooking has the following menu items, which can activate effects separate from armor skills.\r\n■The skill effect gained by cooking lasts for 90 minutes. However, if a new skill is obtained by cooking, the effect is overwritten.\r\n■Up to 6 dishes can be \"left over\". The leftover dishes will be stored for 1 hour.\r\nHow to cook\r\n[Select the menu]\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000※Note, however, that the selections here are only a guide for selecting ingredients.\r\n[Selecting] yellow ingredients are \"base ingredients\" and pink ingredients are \"Auxiliary\". These allow you to create dishes via the menu.\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000※If you select any other option, it will be a Guild's Yaminabe.\r\n[How to cook] By repeatedly pressing the confirm button, the cursor on the gauge will move to the right.\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000By hovering the cursor over the green \"Success Area\" or blue \"Great Success Area,\" a stamp will accumulate directly below the food meter.\r\nWhen the color of the stamp turns green, the dish is a success, and when it turns blue, it is a great success.\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000※The more people that participate, the higher the chance the dish will be a great success.\r\nChef Cat's Wisdom\r\nSecret of success\t10 tickets\tGreen area expanded.\r\nNo failure allowed\t10 tickets\tRed areas removed\r\nUltimate Success\t10 tickets\tBlue area gets doubled.\r\nCooking Technique\t10 tickets\tCursors return speed is slower\r\nSecret seasoning\t20 tickets\tResult will be one rank higher.\r\nMystery seasoning\t5 tickets\tResult is randomized\n\nThe order of the skills are G.Failure/Failure/Success/G.Success\n\n" +
                "Easiest way to cook is selecting Secret Seasoning from Chef's Wisdom before cooking. What it does is raise the success level by one. \n\n" +
                "Page 1\n" +
                "1: Explosive Rice [Snow Powder, Whole Vanilla, Wabisabi Wasabi, Deep Sea Chub] (Hunger Increased [Lg] / Health+30 / Rage+1 / Rage+2)\n\n" +
                "2: Pioneer's Meal [Goemon Frog, Rainbow Mint, Onion Sticks, Mimic Vines] (Hunger Increased [Lg] / Health +30 / Three Worlds Protection +2 / Three Worlds Protection +3)\n\n" +
                "2: Pioneer's Meal [Dangerous Melon, Demon Pepper, Onion Sticks, Mimic Vines] (Hunger Increased [Lg], Wind Pressure [Sm], Wind Pressure [Lg], Wind Pressure [Dragon])\n\n" +
                "3: Lucky Pancake [Star Pineapple, Dangerous Melon, Whole Vanilla, Mimic Vines] (Hunger Increased [Lg] / Health +10 / Good Luck / Great Luck)\n\n" +
                "4: Ultimate Sashimi [Deep Sea Chub, Blk and Wht Dragonfly, Ice Salmon, Whole Vanilla] (Hunger Increased [Lg], Earplugs, High Grade Earplugs, Super Ear Plugs)\n\n" +
                "5: Brother's BBQ [Monochrome Mushroom, Deep Sea Chub, Ice Salmon, Mimic Vines] (Hunger Increased [Lg], Caring +1, Caring +2, Caring +3)\n\n" +
                "6: Hot Claw Feast [Lava King Crab, Whole Vanilla, Dangerous Melon, Mimic Vines] (Hunger Increased [Lg], Health +20, Adrenaline +1, Adrenaline +2)\n\n" +
                "7: Medicinal Spirit [Ancient Algae, Dangerous Melon, Deep Sea Chub, Demon Pepper] (Hunger Increased [Lg], Wind Pressure [Sm], Wind Pressure [Lg], Wind Pressure [Dragon])\n\n" +
                "7: Medicinal Spirit [Rainbow Mint, Dangerous Melon, Deep Sea Chub, Demon Pepper] (Hunger Increased [Lg], Wind Pressure [Sm], Wind Pressure [Dragon], Violent Wind Breaker)\n\n" +
                "Page 2\n" +
                "8: World Fried Rice [Acid Pepper, Mimic Vines, Onion Sticks, Blk and Wht Dragonfly] (Hunger Increased [Lg], Health +20, Runner +1, Runner +2)\n\n" +
                "9: Goddess Dessert [Snow Kiwi, Dangerous Melon, Whole Vanilla, Magma Mango] (Hunger Increased [Lg], Health +20, Divine Protection, Goddess' Embrace)\n\n" +
                "10: Grilled Shellfish [Large Blly Shlfish, Ice Salmon, Whole Vanilla, Mimic Vines] (Hunger Increased [Lg], Health +20, Hunger Halved, Hunger Negated)\n\n" +
                "(Recommended) 11: Holy Seafood Banquet [Deep Sea Chub, Ice Salmon, Whole Vanilla, Mimic Vines] (Hunger Increased [Lg], Wide-Area +1, Wide-Area +2, Wide-Area +3)\n\n" +
                "(Recommended) 12: Energy Noodles [Mimic Vines, Onion Sticks, Blk and Wht Dragonfly, Whole Vanilla] (Hunger Increased [Lg], Health +10, Terrain [Sm], Terrain [Lg])\n\n" +
                "13: Hunter's Whim [Ice Salmon, Dangerous Melon, Mimic Vines, Deep Sea Chub] (Hunger Increased [Lg], Health +10, Whim, Divine Whim)\n\n" +
                "14: Fantasy Dumplings [Dangerous Melon, Magma Mango, Whole Vanilla, Mimic Vines] (Hunger Increased [Lg], Health +10, Paralysis Halved, Paralysis Negated)\n\n" +
                "Page 3\n" +
                "15: Crimson BBQ [Demon Pepper, Dangerous Melon, Magma Mango, Mimic Vines] (Hunger Increased [Lg], Health +10, Sleep Halved, Sleep Negated)\n\n" +
                "16: Sweet Wrap [Magma Mango, Star Pineapple, Dangerous Melon, Mimic Vines] (Hunger Increased [Lg], Health +10, Poison Halved, Poison Negated)\n\n" +
                "17: Dawn Toast [Whole Vanilla, Magma Mango, Dangerous Melon, Mimic Vines] (Hunger Increased [Lg], Health +10, Stun Halved, Stun Negated)\n\n" +
                "(Recommended) 18: Rainbow Soup [Onion Sticks, Snow Powder, Blk and Wht Dragonfly, Bright Grain] (Hunger Increased [Lg], All Res UP +5, All Res UP +10, All Res UP +20)\n\n" +
                "18: Rainbow Soup [Onion Sticks, Magma Mango, Whole Vanilla, Deep Sea Chub] (Hunger Increased [Lg], Fire Res +10, Fire Res +20, Fire Res +30)\n\n" +
                "18: Rainbow Soup [Onion Sticks, Dangerous Melon, Whole Vanilla, Deep Sea Chub] (Hunger Increased [Lg], Water Res +10, Water Res +20, Water Res +30)\n\n" +
                "18: Rainbow Soup [Onion Sticks, Ice Salmon, Whole Vanilla, Deep Sea Chub] (Hunger Increased [Lg], Ice Res +10, Ice Res +20, Ice Res +30)\n\n" +
                "18: Rainbow Soup [Onion Sticks, Demon Pepper, Whole Vanilla, Deep Sea Chub] (Hunger Increased [Lg], Thunder Res +10, Thunder Res +20, Thunder Res +30)\n\n" +
                "18: Rainbow Soup [Onion Sticks, Mimic Vines, Whole Vanilla, Deep Sea Chub] (Hunger Increased [Lg], Dragon Res +10, Dragon Res +20, Dragon Res +30)\n\n" +
                "(Recommended) 19: Vigor Salad [Taiko Olive, Miracle Herb, Ancient Algae, Whole Vanilla] (Hunger Increased [Lg], Health +30, Wide-Area +3, Herbal Medicine)\n\n" +
                "(Recommended) 20: Hearty Pie [Gutsy Meat, Onion Sticks, Bright Grain, Snow Powder] (Hunger Increased [Lg], Health +30, Encourage +1, Encourage +2)\n\n" +
                "21: Unity Buns [Taiko Olive, Bright Grain, Blk and Wht Dragonfly, Mimic Vines] (Hunger Increased [Lg], Health +30, Bond, Assistance)\n\n" +
                "Page 4\n" +
                "(Recommended) 22: Blast Steak [Gutsy Meat, Magma Mango, Snow Kiwi, Star Pineapple] (Blue Soul, Blue Soul, Incitement, Red Soul)\n\n" +
                "0: Guild's Yaminabe [Any, Any, Any, Any] (Hunger Increased [Lg], Random, Random, Random)";
        }
    }

    public static string GetGameSnSInfo
    {
        get
        {
            return "This weapon is for certain far more easier to operate than Dual Swords, it however also has notoriously long hitlag effects. It is also arguably the highest benefactor of Determination's effects.\n" +
                "It has some of the best paralysis weapons in the game, playing a large part in multiplayer road runs.\n" +
                "Hiden Skill Effects\nSnS obtains a larger 1.30x raw modifier rather than the usual 1.20x Blademaster weapons usually get. It can also use all elemental and status sword crystals, and has slightly faster movement speed.\nNotes\nThe slide attack has a few i-frames (~9 with E+2) and can do a 180° turning slash at the end.\r\nLong length is generally a flat upgrade with its reach. Very Long is usable if you're just going to spam roundslashes but may want some spacing management.\nSnS has a very lenient hit requirement to ready the Transcend burst attack.\nYou can technically manipulate a transcend burst to any element you want by using the sword crystal compatibility from the hiden skill.\r\nShield attacks do impact damage and can stun, but they use white sharpness values at best.\r\nThe vacuum slash has no hitlag or sharpness cost on the extra hit it adds to the roundslash. It isn't as a big deal today, but can be nice to have.\r\nUsing items unsheathed resets Rush.\r\nOf note, some time attack players have had success with dropping Fencing +2 for the sake of reducing hitlag.\r\nSnS's blocking isn't good, but the Perfect Defense caravan skill can technically be used with Obscurity Up to amass 300 raw in two blocks cleanly if you have good timing, keeping it until you take knockback.\r\nGalatine Z is the best overall SnS in the game when poison is not necessary.\n" +
                "Galatine is also a good weapon for Road. Outside of Road, you can use Blue Tower.\n\n" +
                "SnS Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nSlide Attack\tMotion value change (21 > 31)\t\t\t\r\nShield Attacks\tMotion value changes, but doesn't influence Extreme style attacks\t\t\t\r\nInfinite Slash\tMotion value change (25 > 28)\t\t\t\n\n" +
                "Skill Recommendations\t\r\nSkill\tNotes / Reasoning\r\nCore\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\r\nEvasion +2\tGreatly increases survivability in Frontier, also triggers evasion-reliant skills much more easily. Obtain it through Drawing Arts +2, Evasion Boost, Encourage, or wearing a piece of Nargacuga armor.\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\r\nRecommended\t\r\nDrawing Arts +2\tA compound skill that is perhaps the best Evasion skill for SnS with its stamina benefits, constant evasion can surprisingly drain it quickly.\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\r\nStylish (Up)\tSharpness restoration on avoiding attacks.\r\nRush (Up)\tGood source of raw if you stay unsheathed and do not use any items. Cannot get the Z skill on TW.\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\r\nAbnormality\tStellar compound skill when paired with poison. Also makes paralysis incredibly consistent for SnS.\r\nOther Skills\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\r\nLavish Attack\tProvides +100 true raw at the expense of sharpness. Is often paired with SnS's roundslash attack and Stylish.\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\r\nObscurity (Up)\tViable with a combination of Obscurity Up and the Perfect Guard caravan skill. However, this needs good timing and the boost is lost on knockback, but if executed well you can get +300 true raw on two perfect guards.\r\nElement Attack Up\tCan be potent, but the means of obtaining this skill are rare (the most reliable are hiden decorations and premium gear).\r\nEncourage (Up)\tThe user gets Horn Maestro and also grants party-wide Evasion +2 and KO immunity. The Z skill confers party-wide stamina buffs. You could run it on Road SnS but it will dig into your set potential significantly.\r\nNot Recommended\t\r\nCombat Supremacy\tToo difficult for SnS to keep up.\r\nDissolver (Up)\tVery little actual payoff. Obsolete with Determination.\r\nGuard skills\tReflect, Fortification +2/Guard +2, etc. You are not a lance.";
        }
    }

    public static string GetGameDSInfo
    {
        get
        {
            return "One of the best weapons in the game, but with a higher skill ceiling due to its unique sharpen motion. Compatibility with Vampirism has made it drastically more accessible to pick up, however.\r\nDuals have some of the fastest clear times against musous, competing with Tonfa.\r\nHiden Skill Effects\r\nAttacks in any Demon mode restore 3 stamina per hit. Compatible with Combat Supremacy.\r\nNotes\r\nLike SnS, long length is pretty much an upgrade and often a common find on modern weapons.\r\nToday, there is no reason to use any demonization that isn't Extreme Demon Mode. However, you cannot receive any healing other than from Vampirism in this state.\r\nEach sharpen in a combo adds 1.05x raw, stackable four times for up to a 1.20x raw bonus. The attacking evades in True/Extreme Demon modes continue this combo state.\n" +
                "You can't receive any healing when demonized, Vampirism leeching is the only exception. This is made much more apparent on road.\n" +
                "Dainsleif Z is the best DS in the game.\n" +
                "Sharpen 4 times to gain maximum buff. Don't drop the combo.\n\n" +
                "DS Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nFront Flip Slash\tMotion value change (11-15 > 16-22)\t\t\t\r\nRush Slash\tMotion value change (11-5 > 16-7) (14-6 > 21-9 demonized)\t\t\t\r\nSharpening Technique\tThe first sharpen within a combo counts as two sharpens for the multiplier\t\t\t\r\nMisc\t\t\t\t\r\nDS Length Up\tLength raised by one step at the cost of -25 raw, can stack with no extra raw loss\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nDrawing Arts +2\tBest Evasion +2 skill for DS due to the stamina benefits. Evasion Boost is sometimes used in time attack for consistency.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tLeech back HP even when demonized. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nAbnormality\tStellar compound skill when paired with poison.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tEspecially popular on DS. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nLavish Attack\tProvides +100 true raw at the expense of sharpness. It can work well with Stylish Up on harder fights that require constant evasion.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nElement Attack Up\tCan be potent, but the means of obtaining this skill are rare (the most reliable is premium gear).\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tDifficult to maintain as easily with Extreme Demon Mode.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tIt can technically be paired effectively with Rush Up on JP because DS Hiden restores stamina on every hit.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nDissolver (Up)\tVery little actual payoff. Obsolete with Determination.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameGSInfo
    {
        get
        {
            return "A weapon balanced around premium gear and Raviente, with standard content only getting harder for it. Still, it can hit really hard, but it's a matter of when it's allowed to do so.\r\nIronically the hardest weapon to play properly on Raviente, but a good GS player makes a world of difference.\r\nHiden Skill Effects\r\nFaster charging that stacks with Focus, and a wider window before the charged upswing attack overcharges down to Lv2. Timed parries can recover some sharpness, being a net gain with Sharp Sword +2.\r\nOther effects for non-Extreme styles have been truncated.\r\nNotes\r\nYou are a friendly fire liability.\r\nHold R1 before unsheathing to do a regular overhead attack, as you'll otherwise do a special wide slash (although it does combo into any charged slash). You can also do upswing charges and parries (Storm, Extreme) from a sheathed state.\r\nVery Long length is the most popular due to the most-used attacks being vertical slices and overall more leniency with keeping a distance. Long is plenty usable, though.\r\nCombat Supremacy is the Frontier equivalent of Critical Draw on GS, pairing very well with hit & run strategies with the stamina drain and 1.20x raw increase. Starving Wolf +2 w/ Rush is an alternative for unsheathed play.\r\nGS gets the best mileage out of Raviente power crystals. They do not work on the shining attack however, but at least they don't get used up.\r\nThe unsheathed running hop attack has a few i-frames at the start, but it's rarely used.\r\nWell-timed parries with GS Hiden and Obscurity Up greatly manage sharpness when using Lavish Attack.\r\nLarge amounts of hitstop is a valid way of avoiding some attacks.\n" +
                "If you have 30 power crystals with a Raviente GS and if the hunt isn't over by the time they're all used up, you are doing something wrong.\n" +
                "Z100 element is technically the best option, but needs good sigils + 1x GS Length Up for best results\n\n" +
                "GS Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nUpswing\tMotion value change (56 > 72), does not benefit charged upswings\t\t\t\r\nRotation Slash\tMotion value change (56 > 72)\t\t\t\r\nGuard Slash\tHeaven/Storm guard slash can block an extra attack, stacks up to two times\t\t\t\r\nMisc\t\t\t\t\r\nGS Length Up\tLength raised by one step at the cost of -25 raw, can stack with no extra raw loss\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nDrawing Arts +2\tThe stamina boosts make this source of Evasion an excellent pairing with Combat Supremacy playstyle and pairs\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tFaster charging for the weapon's strongest attack. Can be acquired with Gore Magala armor, but more reliably obtained with premium gear. Trained only exists on JP.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tBenefits from the GS hit & run playstyle.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tConfers large damage boosts towards the weapon's hardest hitting attacks. Also includes shining sword attack.\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tCan get up to +225 true raw by parrying. The Z skill boost reduces the amount of steps required and enables sharpness recovery at max buff.\t\t\t\t\t\t\t\t\r\nLavish Attack\tProvides +100 true raw at the expense of sharpness. The higher sharpness cost can be mitigated with GS Hiden and Obscurity Up.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nPerfect Guard\tTimed blocks proc a 72 MV Reflect attack and can immediately be rolled out of.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nGuard/Fortification +2\tAllows parrying to block more kinds of attacks. You can get Guard +2 with Uragaan armor. Guard Up has little discernible effect on parry weapons.\t\t\t\t\t\t\t\t\r\nCaring +3 / Trained +2\tDisables friendly fire through either direction, notably because the upswing charges are very disruptive. Can be obtained as a guild food skill. Trained only exists on JP.\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tWhile harder to implement, you can definitely invoke it in the GS playstyle.\t\t\t\t\t\t\t\t\r\nRush (Up)\tNot as good on GS with sheathing involved. Still, on parries it's a free +50 true raw. You could use this with Starving Wolf for an alternative to Combat Supremacy.\t\t\t\t\t\t\t\t\r\nStarving Wolf +2\tGrants +0.10x to the crit multiplier, +50% affinity and Evasion +2 if you are at 25 Stamina and do not have infinite stamina buffs.\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tYou're a GS.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tNot a good pairing, GS has issues with sustaining the buff even with shining.\t\t\t\t\t\t\t\t\r\nReflect +3\tGenerally not worth how hard it is to get alongside everything else. Try the Perfect Guard caravan skill instead.\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameLSInfo
    {
        get
        {
            return "Building off the 2nd gen iteration, Frontier's evolutionary path combines numerous anime tropes into one effective, but chuuni package. Strangely, it actually takes effort to play effectively in Frontier.\t\t\t\t\t\t\t\t\t\r\nLS is comparable to Swaxe in power, it arguably has a higher skill ceiling to use effectively but does not rely on parrying to sustain its moveset.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nSpirit meter consumption is halved, and the glowing meter attack buff is increased from 1.15x to 1.25x.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nThere is no spirit level mechanic like MH3 and newer. Fill up the bar completely to get a temporary raw bonus.\t\t\t\t\t\t\t\t\t\r\nLength is largely up to taste. Keep in mind LS has multiple kinds of slashes that go in all kinds of directions.\t\t\t\t\t\t\t\t\t\r\nLearning to use the Evasion Slash properly is good practice, and it can also be used to reset combos. The i-frames start at the beginning but don't last too long.\t\t\t\t\t\t\t\t\t\r\nPiercing stabs from parries are powerful, but require good positioning to hit with them.\t\t\t\t\t\t\t\t\t\r\nYou can do an upswing from rolling. Parry from sheathe has a different button combination (Triangle + Circle) than other weapons due to unsheahe > spirit slash.\t\t\t\t\t\t\t\t\t\r\nThe orange/white bar below the spirit meter builds up a stronger multi-hit attack that can be done halfway filled or when maxed out. It otherwise depletes when you sheathe or are forced into doing so (ie tremors).\t\t\t\t\t\t\t\t\t\n" +
                "LS receives the smallest raw boost from Obscurity, but it's still sizeable.\t\t\t\t\t\t\t\t\t\r\nIf you really want a Fade Slash back, just play Switch Axe instead.\t\t\t\t\t\t\t\t\t\n\n" +
                "Go Blue Tenrou for Poison, otherwise Raviente. Murakumo Z / Kusanagi-No-Tsurugi (NetCafe Premium) is also good.\n\n" +
                "LS Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nPiercing Stab\t\"Uncharged (24-12x2 > 32-16x2), Charged (24-12x5-30 > 32-16x5-40) MV changes\r\nDoes not benefit piercing stabs via parrying\"\t\t\t\r\nUpswing\tMotion value change (28 > 42)\t\t\t\r\nRetreating Sword\tEarth Fade Slash obtains i-frames\t\t\t\r\nBlink\tExtends teleport distance\t\t\t\r\nMisc\t\t\t\t\r\nLS Draw Strength\tUnsheathe attacks boosted by x1.50\t\t\t\r\nLS Length Up\tLength raised by one step at the cost of -25 raw, can stack with no extra raw loss\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nEvasion +2\tGreatly increases survivability in Frontier, also triggers evasion-reliant skills much more easily. Obtain it through Drawing Arts +2, Evasion Boost, Encourage, or wearing a piece of Nargacuga armor.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tCan get up to +150 true raw by parrying. The Z skill boost provides a different means of restoring sharpness.\t\t\t\t\t\t\t\t\r\nAbnormality\tGood compound skill when paired with poison.\t\t\t\t\t\t\t\t\r\nPerfect Guard\tTimed blocks do not take chip damage, proc a 72 MV Reflect attack, and can immediately be rolled out of.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nGuard/Fortification +2\tAllows parrying to block more kinds of attacks. You can get Guard +2 with Uragaan armor. Guard Up has little discernible effect on parry weapons.\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nStarving Wolf +2\tGrants +0.10x to the crit multiplier, +50% affinity and Evasion +2 if you are at 25 Stamina and do not have infinite stamina buffs.\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tFaster charging and meter gain, but generally not that useful after obtaining hiden. Can be acquired with Gore Magala armor, but more reliably obtained with premium gear. Trained only exists on JP.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tCan be used in charged thrust sets but is otherwise niche.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameHAInfo
    {
        get
        {
            return "Being much more focused on charged attacks in this game, Hammer sadly shares similar premium and Raviente problems as GS. It still has the best stun, but is also upstaged by Magspike in damage.\t\t\t\t\t\t\t\t\t\r\nHammer had been largely phased out by MS in the Ravi meta, but the reality is that it's much more consistent.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nWell-timed charges add an extra 1.30x raw bonus; the difference can be briefly seen on the guild card. This bonus does apply to every hit on the Lv3 infinite swing until the combo is over (the baseball swing finisher counts as part of the combo).\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nGolfswings are effectively obsolete, you're best charging an attack. Focus +2 is highly recommended.\t\t\t\t\t\t\t\t\t\r\nLength on Hammer is largely up to taste. Lv4-Lv5 charge attacks are pretty much all vertical so the moved hitbox isn't often a problem and can outright reach some hitzones on different monsters, but the Lv3 infinite is less cumbersome with shorter lengths.\t\t\t\t\t\t\t\t\t\r\nThree Worlds Protection is very useful to create openings. Try using Zenith cuffs for specific resists, depending on the hunt.\t\t\t\t\t\t\t\t\t\r\nUnlike mainline, every source of additional stun/KO damage you can grab is recommended (Caravan Skill + Stun Sigil) as they stack and contribute a lot of stun damage.\t\t\t\t\t\t\t\t\t\r\nBecause of the multiplied raw from timed charges with hiden, you should get an appropriate of overhead with attack ceiling from My Missions.\t\t\t\t\t\t\t\t\t\r\nYou can technically dodge some very low profile attacks with the leap from the Lv5 charge.\t\t\t\t\t\t\t\t\t\n\n" +
                "Blue Tower for Poison, Z100 otherwise.\n\n" +
                "HA Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nBaseball Swing\tMotion value change (100 > 130)\t\t\t\r\nUnsheathe Upswing\tMotion value change (32 > 48)\t\t\t\r\nCharging Movement\tNon-running charges have higher base movement speed, stacks three times\t\t\t\r\nMisc\t\t\t\t\r\nStun Value\tNot Hammer-specific, but important\t\t\t\r\nHA Length Up\tLength raised by one step at the cost of -25 raw, can stack with no extra raw loss\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tIncreases the charging rate for the weapon's most heavily used attacks. Can be acquired with Gore Magala armor, but more reliably obtained with premium gear. Trained only exists on JP.\t\t\t\t\t\t\t\t\r\nEvasion +2\tGreatly increases survivability in Frontier, also triggers evasion-reliant skills much more easily. Obtain it through Drawing Arts +2, Evasion Boost, Encourage, or wearing a piece of Nargacuga armor.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nUnaffected +3\tThis catch-all hearing/tremor/wind resist skill provides Hammer the extra opportunities it needs to deal damage with. Zenith resists are recommended.\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks. You may not feel the need to carry the Z skill.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tPowers up the weapon's strongest attacks.\t\t\t\t\t\t\t\t\r\nKO Technique\tBoosts stun damage. Stack it with the sigil effect.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tMainly only used in Raviente hunts as the stamina drain is mostly disruptive to Extreme Hammer play.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameHHInfo
    {
        get
        {
            return "The buffs are extremely powerful, but the actual weapon isn't. It does however have the benefit of being a free ticket to Raviente trains, due to the buffs being a large centerpiece for teams and also contributing KO damage.\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nNotes come out 34% faster.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nThe weapon uses a tweaked version of recital mode from Monster Hunter 2. The biggest difference is that notes are retained until you encore, play a debuff melody, or sheathe.\t\t\t\t\t\t\t\t\t\r\nAlways take up Encourage +2 for Horn Maestro and party-wide Evasion +2 . Encourage Up grants party-wide stamina benefits if you have it.\t\t\t\t\t\t\t\t\t\r\nEvery HH in the game has Attack Up Large, by virtue of Storm style's pink notes.\t\t\t\t\t\t\t\t\t\r\nChaining attacks into recitals plays the note faster than usual. If you were to start a melody with Purple, Hilt Stab > Recital is the fastest way to get your song going.\t\t\t\t\t\t\t\t\t\r\nHH has the lowest hit requirement of all weapons to ready a Transcend burst attack.\t\t\t\t\t\t\t\t\t\r\nHH is a good but also somewhat harsh learning tool in regards to fighting Raviente as part of a combat team, especially in regards to animation lock.\t\t\t\t\t\t\t\t\t\r\nIf you are using Cyan + Red notes, do not attempt to cross streams and go from AuL to DuL without encoring the former first if there are Adrenaline users. You will end up playing a Health Increase melody in the process, which will deactivate it.\t\t\t\t\t\t\t\t\t\n" +
                "Going the heal-only route with items is not a great idea because you will already be trying to juggle buffs and damage on a relatively slow weapon. It's not worth it.\t\t\t\t\t\t\t\t\t\r\nThe sonic bomb and debuff song motions can reliably apply the effects of the Red & Blue Soul skills onto other hunters.\t\t\t\t\t\t\t\t\t\r\nThe offerings of Soul Up are pretty reasonable, so it's not uncommon for HH users to take up both Red Soul (via Blazing Majesty) and Blue Soul for extra buffs.\t\t\t\t\t\t\t\t\t\n\n" +
                "Blue Tower for Poison, Z100 otherwise.\n\n" +
                "HH Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nMusical Attacks\tMotion value changes to all recital mode motions, all made roughly ~50% stronger\t\t\t\r\nBeatdown\tMotion value change on the hilt stab (16 > 27)\t\t\t\r\nNote Change (1, 2, 3)\tCan change the respective note's slot to the color listed on the sigil\t\t\t\r\nMisc\t\t\t\t\r\nStun Value\tNot HH-specific, but important\t\t\t\r\nSonic Bomb Range\tAlso extends the reach of the HH's sonic bomb effects\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nEncourage +2\tThe user gets Horn Maestro and also grants party-wide Evasion +2 and KO immunity. The Z skill confers party-wide stamina buffs. Effectively mandatory on HH.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nRed/Blue Soul Up\tAttack teammates or use sonics to provide them bonuses; Soul Up modernizes these skills to be useful. Blue Soul can be often found on Soul Up pieces and you can get Red Soul via Blazing Majesty +1/2.\t\t\t\t\t\t\t\t\r\nKO Technique\tBoosts stun damage. Stack it with the sigil effect.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nUnaffected +3\tThis catch-all hearing/tremor/wind resist skill can provide less interruptions when playing music.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tThe skill has low hit requirements for HH and it can be a nice source of extra damage.\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill, including the aforementioned Red Soul.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tHH doesn't attack as frequently, so the use case for this isn't exactly high priority.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tMainly only used in Raviente hunts as the stamina drain is mostly disruptive, especially with being unable to roll out of recital mode.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nAssistance (Up)\tEffectively superseded by Red/Blue Soul Up and is on fewer pieces You also have to be in range of other teammates for the buffs to apply.\t\t\t\t\t\t\t\t\r\nWide-Range +3\tIt's going to be a pain juggling damage dealing, buffs, debuffs, and items. Though HH in Frontier is more support oriented than mainline and that this can still be useful, never go full-in support.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameLAInfo
    {
        get
        {
            return "THE WALL. And the last guy to return from a trip on road, when you stop tripping them that is.\t\t\t\t\t\t\t\t\t\r\nIts best use is on hunting road for extra survival, or as part of cheese methods to beat certain fights.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nTake no chip damage from blocking any attack. Can chain up to four consecutive hops instead of three. +10 MV bonus to the final Normal/Upward/Skyward thrust in a combo.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nLong is the most common length on Zenith Lances for a reason. Very Long may have uses, but you may find it to be very unwieldly (and ridiculous).\t\t\t\t\t\t\t\t\t\r\nLance retains its cutting/impact hitzone adjustment modifier; if 0.72x of impact's hitzone is still superior to cutting; Lance uses impact's value as cutting damage. (Carapaceons are a good example of this)\t\t\t\t\t\t\t\t\t\r\nWith Lance Hiden, Guard/Fortification +2, and Guard Up, only a handful of attacks in the game cannot be blocked safely. This list mostly extends to things like most modern nukes, DOTs, Barioth applying snowman, etc.\t\t\t\t\t\t\t\t\t\r\nZenith-level hazards will always cause pushback if you're not using the heavy guard, guard advance, or running shield bash.\t\t\t\t\t\t\t\t\t\r\nThe shield bash while running takes zero pushback from guarding with it and has an innate Guard +2. The only equivalent to this from neutral is Storm's guard advance, which is much more rigid.\t\t\t\t\t\t\t\t\t\r\nThe ranged guard works by blocking the hit yourself for it to block for other players.\t\t\t\t\t\t\t\t\t\r\nBackhop thrusts return into guarding faster, you can also do them from neutral.\t\t\t\t\t\t\t\t\t\r\nActual time attack play mostly invokes an evasive playstyle, although reflects from the Perfect Guard caravan skill see usage and shield bashes still build Obscurity + shield phial charges.\t\t\t\t\t\t\t\t\t\n" +
                "Guard lancing can be a good tool to learn parrying with other weapons.\t\t\t\t\t\t\t\t\t\n" +
                "The Damage buff lasts 3 minutes, while the stamina buff lasts a few seconds minimum (maximum depends on stamina usage). You can only heal your teammates, they don't receive more attack and stamina.\n" +
                "With Guard+2 and Guard Up, you can strong guard Stygian Zinogre's nuke, Guanzorumu's nuke, HC Akantor's nuke, Zenith Hyujikiki's Super (Area-Guard), Sparkling Zerureusu's Landing, Conquest Fatalis's nuke, Rajang's Nuke, Zenith Rukodiora's Impale, Harudos' Nukes, Shagaru Magala's Nuke, Elzelion's Phase Transition, Uragaan's Super (need good angle), Taikuns' Super Slam, Zenith Gravios' Super (block backwards), Duremudira's Charge Attack, 3rd Phase Duremudira's Spin into Ice attack, Arrogant Duremudira's Aerial Spin.\n\n" +
                "Blue Tenrou for Poison, Z100 Affinity otherwise.\n\n" +
                "LA Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nShield Attack\tMotion value change (3-25 > 4-37)\t\t\t\r\nCharge Finisher\tMotion value change (50 > 75)\t\t\t\r\nTech Change\t\t\t\t\r\nDrill Charge\tEarth-only attack change\t\t\t\r\nMisc\t\t\t\t\r\nDraw Strength\tUnsheathe attacks boosted by x1.50\t\t\t\r\nLance Length Up\tLength raised by one step at the cost of -25 raw, can stack with no extra raw loss\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback. Requires perfect guards and evasion to advance reliably.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nEvasion +2\tLance does lend itself well to an evasion playstyle. It is reasonable to drop any source of this skill if you require space for a guard-focused set.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\tNote that Lance can run evasion and guard playstyles. It is common to mix elements of the two somewhat for buffs but it is best to focus on one or the other due to constraints.\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tCan get up to +300 true raw by guarding. The Z skill boost provides a decent means of restoring sharpness on guard sets especially. Should be taken on either playstyle.\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. You could skip this if you're playing a guard-oriented set.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks. Recommended for evasion-focused play\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nAbnormality\tGood compound skill when paired with poison or paralysis.\t\t\t\t\t\t\t\t\r\nFortification +2\tGrants Guard +2 and stamina buffs. Guard Up in Frontier is a Z skill, granting Lance 360° blocking; it only trips the player if they are hit from behind.\t\t\t\t\t\t\t\t\r\nReflect +3 (Up)\tWith the Reflect Up Z skill, every blocked monster attack sets off a 68 MV hit. It benefits from raw and deals impact damage, but it cannot crit or use element/status.\t\t\t\t\t\t\t\t\r\nPerfect Guard\tTimed blocks do not take chip damage, proc a 72 MV Reflect attack, and can immediately be dodged out of. If you are using Reflect 3 + Up, reflect damage from perfect guards are boosted to 92 MV.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nUnaffected\tCan create some more attacking opportunities and quality of life, but Lance has other reliable means to deal with hazards and this competes for precious skill points and slots.\t\t\t\t\t\t\t\t\r\nAdrenaline\tLance Hiden negates chip damage entirely from blocking monster attacks, so it may be safer to run outside of unblockable damage sources. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tCan be used with charged slash combos and evasion slashes, but is otherwise niche.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tIt only boosts the charged stab.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tVery harsh on Lance.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tJust block everything.\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameGLInfo
    {
        get
        {
            return "With shelling not being that useful today, GL's main shtick is being a more aggressive Lance, utilizing the Heat Blade feature and blast dashes for forward mobility.\t\t\t\t\t\t\t\t\t\r\nVery comparable to the regular Lance in survivability, it's mostly up to taste.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nBoth Wyvern Fire and Heat Blade cooldowns are halved, and the latter starts up faster. Shelling capacity is increased, the amount depends on the weapon's shelling type.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nHeat Blade grants numerous benefits; a second hitbox on every physical attack, extended reach, and there is no sharpness loss until it wears off for -100 units. On activation, it also restores a bit of sharpness.\t\t\t\t\t\t\t\t\t\r\nIf the weapon has status, Heat Blade doesn't inflict extra. But if it doesn't have an element, it will use the higher raw heat blade motion value.\t\t\t\t\t\t\t\t\t\r\nAlthough GL can get similar guarding benefits as Lance, it cannot negate chip damage which will make blocking certain attacks potentially lethal, especially on Adrenaline.\t\t\t\t\t\t\t\t\t\r\nHit requirements for multiple skills and transcend are easier to meet with Heat Blade activated.\t\t\t\t\t\t\t\t\t\n" +
                "Recoil/Gentle Shot enables GL to evade after shelling.\t\t\t\t\t\t\t\t\t\r\nConsumption Slayer can be freely abused during Heat Blade's uptime, and can be offset with Obscurity Up's sharpness recovery.\t\t\t\t\t\t\t\t\t\n\n" +
                "Blue Tenrou for Poison, Z100 Affinity otherwise.\n\n" +
                "GL Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nRush Thrust\tMotion value change (38 > 57)\t\t\t\r\nHorizontal Slash\tMotion value change (34 > 51), does not affect Extreme dash followup\t\t\t\r\nMisc\t\t\t\t\r\nShell Change\tShelling type changes to the one displayed on the sigil\t\t\t\r\nShelling Level Up\tUpgrade shelling by one level, can stack up to the maximum of Lv9\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback. Requires perfect guards and evasion to advance reliably.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nEvasion +2\tIt is reasonable to drop any source of this skill if you require space for a guard-focused set, but this will come at the expense of few i-frames on the blast dash.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tCan get up to +300 true raw by guarding. The Z skill boost provides a decent means of restoring sharpness on guard sets especially, which GL can use to mitigate the sharpness drop from Heat Blade expiring in advance.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tBuilds up incredibly fast on GL. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nLavish Attack\tProvides +100 true raw and the sharpness penalty is negated if Heat Blade is activated. However, it may be harder to keep up sharpness on road.\t\t\t\t\t\t\t\t\r\nAbnormality\tGood compound skill when paired with poison or paralysis.\t\t\t\t\t\t\t\t\r\nFortification +2\tGrants Guard +2 and stamina buffs. Guard Up in Frontier is a Z skill, granting GL 360° blocking; it only trips the player if they are hit from behind.\t\t\t\t\t\t\t\t\r\nReflect +3 (Up)\tWith the Reflect Up Z skill, every blocked monster attack sets off a 68 MV hit. It benefits from raw and deals impact damage, but it cannot crit or use element/status.\t\t\t\t\t\t\t\t\r\nPerfect Guard\tTimed blocks do not take chip damage, proc a 72 MV Reflect attack, and can immediately be dodged out of. If you are using Reflect 3 + Up, reflect damage from perfect guards are boosted to 92 MV.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tMuch easier to utilize with Heat Blade.\t\t\t\t\t\t\t\t\r\nUnaffected\tCan create some more attacking opportunities and quality of life, but GL has other reliable means to deal with hazards and this competes for precious skill points and slots.\t\t\t\t\t\t\t\t\r\nAdrenaline\tGood user of this skill, but chip damage from guard always poses a risk. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tGood source of raw, but might be difficult to find room for.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. The most notable for GL is Artillery God, boosting shelling and WF damage but their usage is almost absent at this point.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tCan be used with charged slash combos and evasion slashes, but is otherwise niche.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nRecoil / Gentle Shot\tCan evade after shelling, Gentle Shot also bolsters shell capacity by one. Unfortunately shelling isn't really worth it.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tVery harsh on GL.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tJust block everything.\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameTOInfo
    {
        get
        {
            return "The Tonfa is a weapon that attacks primarily with airdashes. It could also fight on the ground.\t\t\t\t\t\t\t\t\t\r\nEffectively the strongest weapon in the game, relative to effort taken.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nThe combo and EX meters are both extended to have a sixth segment each. The former technically augments Tonfa's combo multiplier with an additional 1.10x raw, up to a maximum of 1.60x.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nDue to how the combo meter inflates raw, Tonfa requires a much higher My Mission attack ceiling than most weapons!\t\t\t\t\t\t\t\t\t\r\nLong mode is the default and does more KO damage. Short mode was reworked into being centered around meter gain, but it has a ton of hitlag and isn't really worth using outside of the dash attack for meter.\t\t\t\t\t\t\t\t\t\r\nGuardpoints do exist on some attacks, but it's not that practical to invoke them. Iron Arm is also fairly tough to get on modern sets.\t\t\t\t\t\t\t\t\t\r\nThe Ryuuki finisher tops up meters of all nearby Tonfa players. It can also be triggered by Tonfa-wielding hunter NPCs. These bursts also grant a fair few other benefits, but most are pretty lightweight in function at this point.\t\t\t\t\t\t\t\t\t\r\nTo keep your combo meter, just hit or evade things. Generally, the timer runs slower if the monster is going through a lengthy attack when you can't or shouldn't be hitting them.\t\t\t\t\t\t\t\t\t\n" +
                "EX Evasion doesn't cost stamina, only meter.\t\t\t\t\t\t\t\t\t\r\nEX Pursuit (charged jab + meter) benefits from Gunnery/Artillery (obtainable via Blazing Majesty), Charge Attack Up, and Focus, but is quite expensive compared to airdash spam.\t\t\t\t\t\t\t\t\t\r\nLike SnS, two well-timed blocks with the Perfect Defense caravan skill alongside Obscurity Up can amass 300 raw until you get knocked back.\t\t\t\t\t\t\t\t\t\n\n" +
                "Blue Tower for Poison, Z100 Affinity otherwise.\n\n" +
                "TO Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nEX Meter\tSlightly increases EX meter gain\t\t\t\r\nCombo Timer\tThe combo timer decreases more slowly before meter is lost\t\t\t\r\nMisc\t\t\t\t\r\nStun Value\tNot Tonfa-specific, but still relevant as an impact weapon\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nEvasion +2\tGreatly increases survivability in Frontier, also triggers evasion-reliant skills much more easily. Obtain it through Drawing Arts +2, Evasion Boost, Encourage, or wearing a piece of Nargacuga armor.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nAbnormality\tStellar compound skill when paired with poison.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tPairs well with EX Evasion spam on the ground as they do not cost stamina. On JP it is more commonly paired with Rush Up and used in the air if possible.\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. For Tonfa, this can boost the damage from EX Pursuits significantly due to the Artillery God portion.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tViable with a combination of Obscurity Up and the Perfect Guard caravan skill. However, this needs good timing and the boost is lost on knockback, but if executed well you can get +300 true raw on two perfect guards.\t\t\t\t\t\t\t\t\r\nStarving Wolf +2\tGrants +0.10x to the crit multiplier, +50% affinity and Evasion +2 if you are at 25 Stamina and do not have infinite stamina buffs. Pairs well with Combat Supremacy.\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tFocus can be used with Storm EX Pursuit attacks, otherwise effectively a luxury. Trained only exists on JP, and also includes Martial Arts +2 which can help sustain airtime.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tIncreases the damage of the EX Pursuit and the charged overhead.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nLavish Attack\tYour sharpness will get murdered.\t\t\t\t\t\t\t\t\r\nIron Arm +2\tTechnically boosts Tonfa's guardpoints by 7 frames, but the skill is very rare.\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameSAFInfo
    {
        get
        {
            return "In a strange alternate reality, Swaxe gained proper defensive options and helpful things like getting meter from axe hits.\t\t\t\t\t\t\t\t\t\r\nThis is a very consistent weapon if you can reliably parry attacks to sustain phial energy.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nWild Swings have halved stamina drain, axe attacks gain more meter, and attacks using the phial use less meter. Additionally, successful parries (in Extreme style) reward a temporary 1.05x bonus to raw, indicated by a glowing arm effect.\t\t\t\t\t\t\t\t\t\r\nThe means for receiving the 1.05x boost differs between styles.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nConsider using Focus +2 if you lack Switch Axe Hiden.\t\t\t\t\t\t\t\t\t\r\nA power phial doesn't increase raw per se, it boosts the motion values of phial attacks by roughly 10% instead. It's still considered the best kind, but is found on fewer Swaxes.\t\t\t\t\t\t\t\t\t\r\nElemental phials are currently the most common phial type on Zenith weapons, and in some cases do still provide a massive boost.\t\t\t\t\t\t\t\t\t\r\nA source of Guard +2 is much more important on Swaxe than its comtemporaries LS and MS, because the weapon relies on parries to sustain meter and not just Obscurity stacks.\t\t\t\t\t\t\t\t\t\r\nWith hiden, it takes two parries to fill the meter and activate light sword mode. If the Swaxe active feature is one, it only takes one.\t\t\t\t\t\t\t\t\t\r\nThe elemental discharge from Earth can still be done in Extreme, just tap the other button after the initial thrust.\t\t\t\t\t\t\t\t\t\n" +
                "Swaxe's healing from the guard sigil is useful to get around attacks that cause chip damage on parrying, especially on Adrenaline levels of HP.\t\t\t\t\t\t\t\t\t\r\nAs Swaxe's unsheathed running uses the phial instead of stamina, you may find it easier to use skills like Starving Wolf or Combat Supremacy.\t\t\t\t\t\t\t\t\t\r\nThe second stage of Rush negates all meter drain while running.\t\t\t\t\t\t\t\t\t\r\nSome monsters may start fights doing two hits that can be parried in succession to start you in lightsword right away. If you have a Prayer Swaxe with the buff active, you only need to parry one hit.\t\t\t\t\t\t\t\t\t\n\n" +
                "Blue Tenrou for Poison (Can change phial type, best overall is Power), Z100 Raw otherwise. Francisca Z is also nice, but held back by the stun phial.\n\n" +
                "SAF Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nStunning Blast\tAdds stun values on elemental discharge and absorption release\t\t\t\r\nGuard\tParry hitbox slightly increased(?) and successful parries heal 15 HP\t\t\t\r\nCharge Movement Spd\tCan move faster while charging\t\t\t" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback. Builds up very fast on Swaxe.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nEvasion +2\tGreatly increases survivability in Frontier, also triggers evasion-reliant skills much more easily. Obtain it through Drawing Arts +2, Evasion Boost, Encourage, or wearing a piece of Nargacuga armor.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW.\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tCan get up to +225 true raw by parrying. The Z skill boost provides a different means of restoring sharpness.\t\t\t\t\t\t\t\t\r\nGuard/Fortification +2\tAllows parries to block more kinds of attacks, vital for meter in certain hunts (ie Z Hypnoc) or allows for more opportunities in general. Can get Guard +2 with Uragaan armor. Guard Up doesn't do much for SAF at all.\t\t\t\t\t\t\t\t\r\nAbnormality\tGood compound skill when paired with poison.\t\t\t\t\t\t\t\t\r\nPerfect Guard\tTimed blocks do not take chip damage, proc a 72 MV Reflect attack, and can immediately be rolled out of.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tVery useful before hiden but fairly redundant afterwards outside of charge attack-based sets. Can be acquired with Gore Magala armor, more reliably obtained with premium gear. Trained only exists on JP.\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tLegitimate playstyle with Swaxe. However, the evasion slashes and meter-based unsheathed running become the only reliable means of dodging attacks.\t\t\t\t\t\t\t\t\r\nStarving Wolf +2\tGrants +0.10x to the crit multiplier, +50% affinity and Evasion +2 if you are at 25 Stamina and do not have infinite stamina buffs.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tCan be used with charged slash combos and evasion slashes, but is otherwise niche.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameMSInfo
    {
        get
        {
            return "Magspike is to Frontier what Charge Blade was to MH4U. Although it hasn't put Hammer out of a job completely, you can take this thing on any content you want.\t\t\t\t\t\t\t\t\t\r\nThe weapon sees heavy usage on Raviente trains.\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nThe marker does not disappear unless you faint, and base movement speed is increased by 1.2x. Successfully using i-frames on evade motions grants a small raw boost (1.03x) and better meter recovery (1.5x).\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nFollow these instructions for starting and going through the questline to unlock Magnet Spikes, and how to use them.\t\t\t\t\t\t\t\t\t\r\nThe pin attack applies the damage to where you have placed a marker on a monster, you can use this to break certain parts faster.\t\t\t\t\t\t\t\t\t\r\nPinning reliability depends on who the current area host is. If you are not the host, there is a chance that it can fail. NPCs are tied to their host player in this context.\t\t\t\t\t\t\t\t\t\r\nPay attention to whether you are running unsheathed or not, since it can limit your attack options.\t\t\t\t\t\t\t\t\t\r\nFalling attacks via magnetic attraction have both generous motion values and i-frames.\t\t\t\t\t\t\t\t\t\r\nThe basic cutting attacks all have a very large window between each of them, which can be roll cancelled or delay attacks.\t\t\t\t\t\t\t\t\t\r\nCutting's guard motion is effectively a guardpoint attack, it can chain into the Finishing Slash, a magnetic dodge if you need more intangibility, or another guard attack.\t\t\t\t\t\t\t\t\t\r\nImpact's guard has forced fixed knockback and the rebounding attack is similar to but doesn't chain into any meter attacks like Magnetic Assault does, although it can be roll cancelled and still has i-frames.\t\t\t\t\t\t\t\t\t\n" +
                "There is officially a second control bindings menu for the weapon, but only the approach cancel is binded by default for PSP controls in that menu.\t\t\t\t\t\t\t\t\t\r\nMagspike lacks an elemental multiplier for Charge Attack Up. The skill will only boost the impact charged strike, although by a large degree.\t\t\t\t\t\t\t\t\t\r\nIt can be practical to skip out on a source of Evasion +2 completely, due to the copious amount of innate i-frames.\t\t\t\t\t\t\t\t\t\n\n" +
                "Z100 Status is best in slot.\n\n" +
                "MS Sigil Effects\t\t\t\t\r\n\tThere are no Magnet Spike-specific sigil effects outside of UL.\t\t\t\r\nMisc\t\t\t\t\r\nStun Value\tNot Magspike-specific, but still relevant in impact mode\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon. Good raw multiplier, super earplugs, and a myriad of benefits. Does not take up a skill slot if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40) or Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback. Builds up very fast on MS.\t\t\t\t\t\t\t\t\r\nSword God +2\tCombines Sharpness +1, Fencing +2, and Razor Sharp +2 into one excellent compound skill.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts), going higher only increases leech rate.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Use the Z skill to boost raw gains further.\t\t\t\t\t\t\t\t\r\nStylish (Up)\tSharpness restoration on avoiding attacks.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Builds up quickly in cutting mode.\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tCan get up to +225 true raw by parrying. The Z skill boost provides a different means of restoring sharpness.\t\t\t\t\t\t\t\t\r\nAbnormality\tStellar compound skill when paired with poison.\t\t\t\t\t\t\t\t\r\nPerfect Guard\tTimed blocks do not take chip damage and proc a 72 MV Reflect attack.\t\t\t\t\t\t\t\t\r\nEvasion +2\tMS's meter attacks already have a generous amount of i-frames, but this can be a nice luxury otherwise.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nGuard/Fortification +2\tAllows parrying to block more kinds of attacks. You can get Guard +2 with Uragaan armor. Guard Up has little discernible effect on parry weapons.\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tVery useful before hiden for meter, but effectively a luxury afterwards.\t\t\t\t\t\t\t\t\r\nCharge Attack UP +2\tBolsters the MV of Impact mode's charged swing attack dramatically. Does not grant it extra elemental damage.\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tPopular in Frontier due to the availability of -60 HP food, Evasion +2, and overall more dangerous but predictable monsters. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tProvides a 1.15x boost to raw if your HP is 100 or greater. Vigorous Up provides +100 true raw at maximum HP. Its usefulness varies a lot and may outright be skipped on JP depending on your other skills.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 30> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill.\t\t\t\t\t\t\t\t\r\nIce Age (Up)\tGrants a DOT AoE around your character, pairs great with poison and grants other benefits. Useless Z skill.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tUsable, but stamina has to be managed.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nAbsolute Defense\tTakes priority over guarding.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameLBGInfo
    {
        get
        {
            return "In MHF, rapid fire and element take a backseat as raw and the 'Just Shot' take precedence. The easier bowgun to get into and also have results with.\t\t\t\t\t\t\t\t\t\t\r\nIronically, no Raviente combat team is complete without a Raviente LBG, thanks to their exclusive Acid S. The role however does require you to know what you are doing.\t\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\t\r\nA 'Perfect Shot' window is added to the Just Shot meter which deals more damage. Ammo has a chance of not bouncing off monsters, and items can be used unsheathed like SnS. \t\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\t\r\nAs a Gunner weapon, LBG can meet the requirements of various skills extremely quickly.\t\t\t\t\t\t\t\t\t\t\r\nLong barrels don't provide a raw bonus. Instead it is a shot speed increase, which may screw up timing with pierce ammo hits.\t\t\t\t\t\t\t\t\t\t\r\nAs Rapid Fire doesn't really have a place in modern Frontier, newer guns don't come with it. You can still force it with sigils, though.\t\t\t\t\t\t\t\t\t\t\r\nPierce S is the workhorse raw ammo type used.\t\t\t\t\t\t\t\t\t\t\r\nIt may be easier to stick with first-person camera options if you are having trouble tracking ammo when using Just Shots, with the >clip display moved under the crosshairs.\t\t\t\t\t\t\t\t\t\t\n" +
                "Like SnS, using items unsheathed with LBG hiden resets Rush.\t\t\t\t\t\t\t\t\t\t\r\nSpacing increases the size of LBG Hiden's Perfect Shot window when the buff activates.\t\t\t\t\t\t\t\t\t\t\r\nConsider building sets with and without Combat Supremacy. Thunder Clad provides good mobility, but some monsters may call for evasion.\t\t\t\t\t\t\t\t\t\t\r\nAcid S applies a hitzone debuff, which is why they are extensively against Raviente (because the monster is largely three hitboxes)\t\t\t\t\t\t\t\t\t\t\r\nCombined with Caring +3, LBG is a common pick on Raviente support teams to clean up trash mobs fast with Pellet S.\t\t\t\t\t\t\t\t\t\t\n\n" +
                "Z100 Raw Acid is BIS.\n\n" +
                "LBG Sigil Effects\t\t\t\t\r\nRapid Fire\t\t\t\t\r\nRapid Fire Add\tCan fire X shot type as rapid fire\t\t\t\r\nSuper RF Add\tCan fire X shot type as super rapid fire\t\t\t\r\nRapid Fire Immunity\tDisables rapid fire of X shot type\t\t\t\r\nTech Change\t\t\t\t\r\nJ Gauge\tJust Shot's sweetspot window increased\t\t\t\r\nMisc\t\t\t\t\r\nBlunt Bullets\tPellet shots use impact damage\t\t\t\r\nExplosion\tCluster shots may detonate into large explosions\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\t\r\nZenith Armor Piece\tNot a formal skill, but wearing at least one Z/ZF/ZY/ZX or ZP piece grant an innate critical distance bonus. This is effectively compulsory and provides a large damage boost.\t\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon but massively powerful for Gunners especially. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable and Critical Shot/Precision +2 redundant.\t\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback. Relies on evasion to build stacks.\t\t\t\t\t\t\t\t\t\r\nSteady Hand\tCombines Normal/Pierce/Pellet Up in one skill. In JP, a second rank provides the hitzone buff of Critical Shot/Precision +2 but it is redundant with Determination.\t\t\t\t\t\t\t\t\t\r\nCritical Shot / Precis +2\tGrants +5 to hitzones if the monster is in critical distance. Obsolete with Determination.\t\t\t\t\t\t\t\t\t\r\nLoading\tYou will want either Mounting or Gentle Shot for the +1 to capacity. Either one will suffice on a Raviente LBG.\t\t\t\t\t\t\t\t\t\r\nSpacing\tLowers critical distance but attacks within it build up an invisible meter to further boost damage and grant mobility skills. LBG's Perfect Shot window increases.\t\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity. Builds up incredibly quickly on guns.\t\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts),  this builds up very quickly with guns.\t\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tIncredibly safe skill for LBG to take in many matchups, effectively being a free 1.20x raw boost.\t\t\t\t\t\t\t\t\t\r\nEvasion +2\tMight be wasted on Combat Supremacy sets outside of JP's Rush Up if you can obtain the stamina buff before the bar runs dry. Obtain it through Drawing Arts +2 or wearing a piece of Nargacuga armor.\t\t\t\t\t\t\t\t\t\r\nBullet Saver (Up)\tHave a chance to not consume ammo on firing, up to 46% with the Z skill.\t\t\t\t\t\t\t\t\t\r\nReload / Mounting\tUsed to 'fix' reload speed. 'Fast' is needed to reload Pierce 2 quickly and 'Very Fast' for Pierce 3. Mounting +2 and +3 require less points on JP.\t\t\t\t\t\t\t\t\t\r\nRecoil / Gentle Shot\tUsed to 'fix' recoil levels. 'Small' is needed to fire Pierce 3 effectively. The +3 versions of both skills require less points on JP.\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well. Try using it on sets without Combat Supremacy.\t\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed (and do not use any items with LBG hiden). Cannot get the Z skill on TW, but it can halt stamina drain from Combat Supremacy on JP. Builds up quickly on guns.\t\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW. Builds up quickly on guns.\t\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\t\r\nAdrenaline\tSomewhat easier to run this on guns than on Blademaster weapons. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill, especially with Pierce.\t\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. These vary greatly but it can be an alright quality of life skill.\t\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad and other Gunner skills. Obsolete with Determination.\t\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\t\r\nAmmo Combiner\tYou could also take multiple Books of Combos and Caravan skills instead. Can be obtained with Uragaan armor.\t\t\t\t\t\t\t\t\t\r\nShiriagari\tThis skill can squeeze some extra raw out but may be contested by other skills especially. Often employed with skill fruits in time attack.\t\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\t\r\nRapid Fire\tLittle practical use as Rapid Fire capabilities are effectively absent in the current meta.\t\t\t\t\t\t\t\t\t\r\nAuto-Reload / Sniper\tUnlike MHFU, this effectively makes your gun unusable.\t\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tGunners are significantly more fragile, of which you might as well run Adrenaline if you have run a skill dependent on HP. The benefit from the Z skill is also halved.\t\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameHBGInfo
    {
        get
        {
            return "Frontier's HBG sets itself apart from mainline with the ability to use compressed and charged shots, on top of laser beams. An acquired taste.\t\t\t\t\t\t\t\t\t\t\r\nRaviente HBGs are a huge investment, but also have an incredible amount of attack that enables them to keep up in trains as a potential GS replacement.\t\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\t\r\nInnate Evade Extender. The power of Element and Status shots are increased by 1.2x, while Melee, Crag, Cluster inflict (more) KO. Compression shots can be perfectly-timed for extra damage, and Heat Beams deal 1.2x more damage.\t\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\t\r\nAs a Gunner weapon, HBG can meet the requirements of various skills extremely quickly.\t\t\t\t\t\t\t\t\t\t\r\nOne use of compressed shots is on faster targets. Mitigating recoil on these requires more work than usual.\t\t\t\t\t\t\t\t\t\t\r\nExtreme style's heat gauge doesn't require any item management. Blast dodges use its meter and have a ton of i-frames, and can go into a run or roll after landing.\t\t\t\t\t\t\t\t\t\t\r\nCharging shots can be used to save ammo, works best with compressed Normal 3 and Element.\t\t\t\t\t\t\t\t\t\t\n" +
                "Spacing increases the size of HBG Hiden's 'perfect' window for compression shots when the buff activates.\t\t\t\t\t\t\t\t\t\t\r\nBullet Saver factors in every shot used in a single compression shot salvo. All saved ammo is still retained in the >clip and can be fired off.\t\t\t\t\t\t\t\t\t\t\r\nRaviente HBG bomb shots are kinda shitty, nobody uses them for those (just the retarded statline will do).\t\t\t\t\t\t\t\t\t\t\r\nThe shield addon doesn't benefit from Obscurity.\t\t\t\t\t\t\t\t\t\t\n\n" +
                "Z100 Affinity is the highest raw weapon in all of Monster Hunter\n" +
                "Has seemingly not so good performance on Musous.\n\n" +
                "HBG Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nHeat Cannon\tAdds the heat beam functionality, co-exists with Extreme's blue beam\t\t\t\r\nMisc\t\t\t\t\r\nBlunt Bullets\tPellet shots use impact damage\t\t\t\r\nExplosion\tCluster shots may detonate into large explosions\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\t\r\nZenith Armor Piece\tWearing at least one Z/ZF/ZY/ZX or ZP piece grant an innate critical distance bonus. This is effectively compulsory and provides a large damage boost.\t\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon but massively powerful for Gunners especially. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, Elemental Exploiter, and any source of Adrenaline +2 in one skill. Makes Guts unusable and Critical Shot/Precision +2 redundant.\t\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\t\r\nSteady Hand\tCombines Normal/Pierce/Pellet Up in one skill. In JP, a second rank provides the hitzone buff of Critical Shot/Precision +2 but it is redundant with Determination.\t\t\t\t\t\t\t\t\t\r\nCritical Shot / Precis +2\tGrants +5 to hitzones if the monster is in critical distance. Obsolete with Determination.\t\t\t\t\t\t\t\t\t\r\nLoading\tYou will want either Mounting or Gentle Shot for the +1 to capacity. A Raviente HBG will take the latter.\t\t\t\t\t\t\t\t\t\r\nSpacing\tLowers critical distance but attacks within it build up an invisible meter to further boost damage and grant mobility skills. HBG's perfect compression window increases.\t\t\t\t\t\t\t\t\t\r\nEvasion +2\tGreatly increases survivability in Frontier, also triggers evasion-reliant skills much more easily. Obtain it through Drawing Arts +2, Evasion Boost, or wearing a piece of Nargacuga armor.\t\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity. Builds up incredibly quickly on guns.\t\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts),  this builds up very quickly with guns.\t\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\t\r\nBullet Saver (Up)\tHave a chance to not consume ammo on firing, up to 46% with the Z skill. It rolls for every round in a compression shot.\t\t\t\t\t\t\t\t\t\r\nReload / Mounting\tUsed to 'fix' reload speed. 'Fast' is needed to reload Pierce 2 quickly and 'Very Fast' for Pierce 3. Mounting +2 and +3 require less points on JP.\t\t\t\t\t\t\t\t\t\r\nRecoil / Gentle Shot\tUsed to 'fix' recoil levels. High investment is required for less to no recoil on compressed shots. The +3 versions of both skills require less points on JP.\t\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW, but it can halt stamina drain from Combat Supremacy on JP. Builds up quickly on guns.\t\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW. Builds up quickly on guns.\t\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\t\r\nFocus/Trained +2\tCharged shots can be useful against fast monsters and other matchups, but this skill might be difficult to fit on. Trained only exists on JP.\t\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well.\t\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\t\r\nAdrenaline\tSomewhat easier to run this on guns than on Blademaster weapons. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill, especially with Pierce.\t\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. For HBG the Artillery God portion can boost heat beam damage.\t\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get with a high point requirement as well. Pairs very well with Thunder Clad and other Gunner skills. Obsolete with Determination.\t\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\t\r\nAmmo Combiner\tYou could also take multiple Books of Combos and Caravan skills instead. Can be obtained with Uragaan armor.\t\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tDifficult to use, especially as you won't be able to roll out of blast dodges.\t\t\t\t\t\t\t\t\t\r\nShiriagari\tThis skill can squeeze some extra raw out but may be contested by other skills especially. Often employed with skill fruits in time attack.\t\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\t\r\nAuto-Reload / Sniper\tUnlike MHFU, this effectively makes your gun unusable.\t\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tGunners are significantly more fragile, of which you might as well run Adrenaline if you have run a skill dependent on HP. The benefit from the Z skill is also halved.\t\t\t\t\t\t\t\t\t\r\nObscurity (Up)\tIt doesn't work with the shield addon.\t\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameBowInfo
    {
        get
        {
            return "The Bow in Frontier continues to do what it has been doing since the 2nd generation and shoots things with charged shots. Deceptively powerful.\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\nHiden Skill Effects\t\t\t\t\t\t\t\t\t\r\nPower Coatings are boosted to x1.7 with Gou/G-Rank/Evolution Bows. Arc shots can be fired at Lv2 charge and arrows cannot bounce.\t\t\t\t\t\t\t\t\t\r\nNotes\t\t\t\t\t\t\t\t\t\r\nAs a Gunner weapon, Bow can meet the requirements of various skills extremely quickly.\t\t\t\t\t\t\t\t\t\r\nBow has numerous aiming modes in the options, but ultimately it is left with 2nd generation-style controls. With that said, while charging you can press Square (or X) to dodge.\t\t\t\t\t\t\t\t\t\r\nWhile aiming, you can see the critical distance ranges of your shot.\t\t\t\t\t\t\t\t\t\r\nHolding down L1 while releasing a shot does a coatingless shot, not consuming nor granting the benefits of the loaded coating. This technique is based off a glitch in MH2.\t\t\t\t\t\t\t\t\t\r\nRapid shot types are recommended. Spread is powerful, but awkward. Pierce works well as a Lv3 charge.\t\t\t\t\t\t\t\t\t\r\nCrouching shots are awkward to use, but with the piercing shot sigil they can inflict status well.\t\t\t\t\t\t\t\t\t\n" +
                "As Sniper's critical distance bonus is wrapped into both Steady Hand +2 and Determination, just using Auto-Reload is fine.\t\t\t\t\t\t\t\t\t\n\n" +
                "Z100 Pierce-Rapid is best overall, can use special impact coatings.\n\n" +
                "Bow Sigil Effects\t\t\t\t\r\nTech Boost\t\t\t\t\r\nRising Dragon Bow\tMotion values boosted on Shoryuken attack (Heaven style and newer)\t\t\t\r\nTech Change\t\t\t\t\r\nPiercing Shot\tFully charged crouching shots (Storm/Extreme) turn into piercing shots\t\t\t\r\nMisc\t\t\t\t\r\nArc Shot Change\tChanges arc shot behavior to the type displayed on the sigil\t\t\t\n\n" +
                "Skill Recommendations\t\t\t\t\t\t\t\t\t\r\nSkill\tNotes / Reasoning\t\t\t\t\t\t\t\t\r\nCore\t\t\t\t\t\t\t\t\t\r\nZenith Armor Piece\tWearing at least one Z/ZF/ZY/ZX or ZP piece grant an innate critical distance bonus. This is effectively compulsory and provides a large damage boost.\t\t\t\t\t\t\t\t\r\n(Weapon) Hiden\tSecond best skill for any weapon but massively powerful for Gunners especially. Does not take up a skill slot on JP if a hiden cuff is used.\t\t\t\t\t\t\t\t\r\nDetermination\tBest in slot skill in all of Monster Hunter. Supersedes Issen, Critical Eye, Exploit Weakness, and any source of Adrenaline +2 in one skill. Makes Guts unusable and Critical Shot/Precision +2 redundant.\t\t\t\t\t\t\t\t\r\nStrong Attack\tUp to +150 true raw with Strong Attack +5 (40). JP has access to Strong Attack +6 (50).\t\t\t\t\t\t\t\t\r\nIssen +1/2/3\tCritical hit multiplier. Difficult to obtain higher levels of it on Zenith equipment outside of JP, but still very potent if you lack Determination.\t\t\t\t\t\t\t\t\r\nFurious\tVery powerful skill that grants successive raw, element, status, and affinity bonuses. Stacks are lost on getting hit, not just knockback.\t\t\t\t\t\t\t\t\r\nSteady Hand\tCombines Normal/Pierce/Pellet Up in one skill. In JP, a second rank provides the hitzone buff of Critical Shot/Precision +2 but it is redundant with Determination.\t\t\t\t\t\t\t\t\r\nCritical Shot / Sniper\tGrants +5 to hitzones if the monster is in critical distance. Sniper provides this bonus and Auto-Reload. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nMounting\tYou will want the skill for an extra charging level.\t\t\t\t\t\t\t\t\r\nSpacing\tLowers critical distance but attacks within it build up an invisible meter to further boost damage and grant mobility skills. Bow's charge times are made shorter.\t\t\t\t\t\t\t\t\r\nDrawing Arts +2\tGreat to have, not only it provides Evasion +2 but it also serves as a stamina skill for Bow.\t\t\t\t\t\t\t\t\r\nThunder Clad\tFill up a meter for a bonus against hitzones, additional movement speed, and basic status immunity. Builds up quickly on Bow.\t\t\t\t\t\t\t\t\r\nVampirism (Up)\tDramatically increases sustainability with health leeching and also adds some attack. You do not need more than Vampirism +1 (10pts),  this builds up very quickly on Bow.\t\t\t\t\t\t\t\t\r\nFlash Conversion (Up)\tGet +30% affinity, and all excess affinity is converted into extra raw. May be dropped by some players. Bad Z skill outside of Affinity-inclined Raviente weapons.\t\t\t\t\t\t\t\t\r\nSkill Slots Up\tZ skill that boosts the amount of maximum skill slots you can have by one (you have 12 by default with a full set of G-Rank armor). You can get up to +7 for a maximum of 19 skill slots, but this competes with other Z skills.\t\t\t\t\t\t\t\t\r\nRecommended\t\t\t\t\t\t\t\t\t\r\nAuto-Reload / Sniper\tBow does not use Focus in Frontier but rather shortens charge times with this skill instead. Sniper also includes the benefits of Critical Shot, but so does Determination. Auto-Reload is easier to gen into sets.\t\t\t\t\t\t\t\t\r\nLavish Attack\tAdds a multiplier bonus to attack when using any type of coating at the cost of two coatings used up per shot.\t\t\t\t\t\t\t\t\r\nStylish Assault (Up)\tFree +100 true raw on successful dodging and for effectively playing well.\t\t\t\t\t\t\t\t\r\nRush (Up)\tGood source of raw if you stay unsheathed. Cannot get the Z skill on TW, but it can halt stamina drain from Combat Supremacy on JP. Builds up quickly on Bow.\t\t\t\t\t\t\t\t\r\nCeaseless (Up)\tRepeat Offender on crystal meth with up to +50% affinity and even a higher critical hit multiplier. Cannot get the Z skill on TW. Builds up quickly on Bow.\t\t\t\t\t\t\t\t\r\nOther Skills\t\t\t\t\t\t\t\t\t\r\nUnaffected\tCatch-all hearing/tremor/wind resist skill. Can create some more attacking opportunities and quality of life, but min-maxing may involve avoiding this skill. Can be paired with Z skill equivalents for Zenith-grade resists.\t\t\t\t\t\t\t\t\r\nAdrenaline\tSomewhat easier to run on Bow than on Blademaster weapons. Get it through guild food, Buchigire, Blazing Majesty +2, or Determination.\t\t\t\t\t\t\t\t\r\nPoint Breakthrough\tSuccessive attacks on a single hitzone provide a bonus. Not always a consistent skill, especially with Pierce.\t\t\t\t\t\t\t\t\r\nBlazing Majesty\tCompound skill with a myraid of benefits. For HBG the Artillery God portion can boost heat beam damage.\t\t\t\t\t\t\t\t\r\nExploit Weakness\tBehaves like MH3U/MH4U Weakness Exploit but at 35> hitzones. Powerful raw skill, but hard to get. Pairs very well with Thunder Clad and other Gunner skills. Obsolete with Determination.\t\t\t\t\t\t\t\t\r\nExpert / Critical Eye\tAlternate source of pre-Determination affinity, but affinity itself is not difficult to get with other sources.\t\t\t\t\t\t\t\t\r\nStatus Immunity \tNiche or outright unnecessary for anything at a given point. Shagaru Magala armor is an easy means of obtaining this, but it comes with bad resistances and low defense in its stead.\t\t\t\t\t\t\t\t\r\nAmmo Combiner\tYou could also take multiple Books of Combos and Caravan skills instead. Can be obtained with Uragaan armor.\t\t\t\t\t\t\t\t\r\nCombat Supremacy\tCan only work on JP with Rush Up. Stamina is vital for the weapon to work at all.\t\t\t\t\t\t\t\t\r\nBullet Saver (Up)\tHave up to a 46% chance with the Z skill to not use up coatings. It does not ignore coating usage entirely for Lavish Attack,\t\t\t\t\t\t\t\t\r\nShiriagari\tThis skill can squeeze some extra raw out but may be contested by other skills especially. Often employed with skill fruits in time attack.\t\t\t\t\t\t\t\t\r\nNot Recommended\t\t\t\t\t\t\t\t\t\r\nVigorous (Up)\tGunners are significantly more fragile, of which you might as well run Adrenaline if you have run a skill dependent on HP. The benefit from the Z skill is also halved.\t\t\t\t\t\t\t\t\r\nEncourage +2\tHard to reliably obtain for Gunners.\t\t\t\t\t\t\t\t\r\nFocus / Trained +2\tIt doesn't do anything here.\t\t\t\t\t\t\t\t";
        }
    }

    public static string GetGameStatusEleBloatInfo
    {
        get
        {
            return "Status Immunities\n" +
                "Kirin: Paralysis\n" +
                "Lao-Shan Lung: Poison, Paralysis, Sleep\n" +
                "Yian Garuga: Poison" +
                "\n" +
                "Shen Gaoren: Poison, Paralysis, Sleep\n" +
                "Yama Tsukami: Poison, Paralysis, Sleep\n" +
                "Espinas**: Poison\n" +
                "Akura Vashimu**: Poison, Paralysis, Sleep\n" +
                "Akura Jebia: Poison, Paralysis, Sleep\n" +
                "Berukyurosu: Paralysis, Sleep\n" +
                "Pariapuria: Poison, Paralysis, Sleep\n" +
                "Raviente: Paralysis, Sleep, Blast\n" +
                "Doragyurosu*: Paralysis, Sleep\n" +
                "Baruragaru**: Poison, Paralysis\n" +
                "Inagami**: Sleep\n" +
                "Duremudira: Poison, Paralysis, Sleep\n" +
                "Varusaburosu: Sleep\n" +
                "Gasurabazura**: Poison\n" +
                "Yama Kurai: Poison, Paralysis, Sleep\n" +
                "Keoaruboru: Sleep\n\n" +
                "Subspecies and Zeniths are covered where applicable.\n" +
                "Raviente is immune to normal poison effects. But support can invoke a DOT effect if slayers have poison. This is considered lowest priority and is never invoked until the final phase, if it can be fired off at all.\n" +
                "A seldom few monsters are immune to Magnet Spike pin, notably Zenith Rukodiora, Musous and some siege monsters. Those otherwise immune to paralysis or traps interestingly still have paralyzed animations.\n" +
                "*Doragyurosu is immune to these statuses only in G Rank, and also as a Zenith.\n" +
                "**Is also part of Zenith content, and their same immunities apply.\n\n" +
                "Combo Elements\n" +
                "Blaze (Varusaburosu) : 70% Fire / 70% Dragon.\n" +
                "Light (Zerureusu): 70% Fire / 70% Thunder.\n" +
                "Lightning Rod (Rebidiora): 70% Thunder / 70% Dragon.\n" +
                "Tenshou (Shantien): 30% Fire / 100% Water / 70% Thunder\n" +
                "Okiko (Disufiroa): 80% Fire / 40% Dragon / 805 Ice\n" +
                "Black Flame (Fatalis): 50% Fire / 150% Dragon\n" +
                "Music (Poborubarumu): 100% Water / 100% Ice\n" +
                "Dark (Meraginasu): 80% Dragon / 80% Ice\n" +
                "Crimson Demon (Crimson Fatalis): 150% Fire / 50% Dragon\n" +
                "Wind (Garuba Daora): 80% Thunder / 80% Ice\n" +
                "Sound (Diva): 100% Water / 100% Dragon\n" +
                "(Recommended) Burning Zero (Elzelion): 125% Fire / 125% Ice\n" +
                "Emperor's Roar (White Fatalis): 150% Thunder / 50% Dragon\n\n" +
                "Combo elements are equivalent to having 2-3 of them but at a percentage of your elemental power value. For visual effects, the element that deals the most damage will take precedence (Gunlance's Heatblade Mode alternates). With Transcendence bursts, there is a hierarchy on which elements take precedence first. Fire is at the top. Negative elemental hitzones will still apply their raw penalty normally if the \"wrong\" element is involved. Owning Determination will brute force all elements involved, using the weapon's Dissolver modifier.\n\n" +
                "Bloat Multipliers\n" +
                "Sword and Shield, Dual Swords: 1.4x\n" +
                "Great Sword, Long Sword: 4.8x\n" +
                "Hammer, Hunting Horn: 5.2x\n" +
                "Lance, Gunlance: 2.3x\n" +
                "Tonfa: 1.8x\n" +
                "Switch Axe F, Magnet Spike: 5.4x\n" +
                "Heavy Bowgun, Light Bowgun, Bow: 1.2x";
        }
    }


    public string GetGameWeaponInformation
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            return s.WeaponTypeInfo switch
            {
                "Sword and Shield" => GetGameSnSInfo,
                "Dual Swords" => GetGameDSInfo,
                "Great Sword" => GetGameGSInfo,
                "Long Sword" => GetGameLSInfo,
                "Hammer" => GetGameHAInfo,
                "Hunting Horn" => GetGameHHInfo,
                "Lance" => GetGameLAInfo,
                "Gunlance" => GetGameGLInfo,
                "Tonfa" => GetGameTOInfo,
                "Switch Axe F" => GetGameSAFInfo,
                "Magnet Spike" => GetGameMSInfo,
                "Light Bowgun" => GetGameLBGInfo,
                "Heavy Bowgun" => GetGameHBGInfo,
                "Bow" => GetGameBowInfo,
                "Element/Status/Bloat" => GetGameStatusEleBloatInfo,
                "Melee Stats" => GetGameMeleeStats,
                "Bowgun Stats" => GetGameBowgunStats,
                "Bow Stats" => GetGameBowStats,
                "Active Feature" => GetGameActiveFeature,
                "Sword Crystals" => GetGameSwordCrystalSkills,
                "Types" => GetGameWeaponTypesInfo,
                "Sharpness" => GetGameSharpnessInfo,
                "Damage Formula" => GetGameDamageInfo,
                "Unlocks" => GetGameWeaponUnlocks,
                _ => GetGameStatusEleBloatInfo,
            };
        }
    }

    public static string GetGameWeaponUnlocks
    {
        get
        {
            return
@"Q: How do I unlock Tonfas/SA-F/Magnet spike?

A: Tonfas and SA-F are unlocked as soon as you enter G rank, all you have to do is make one of either weapon, equip it and then speak to the Guildmaster to unlock the styles for it.

Magnet spike has its own quest line to unlock:
1. Talk to the smithy
2. Talk to Graham (The magnet spike Rasta in the Rasta bar)
3. Talk to the apprentice cat at the entrance of Mezeporta
4. Go back and talk to Graham
5. Talk to the G rank quest receptionist and there should be a quest to hunt G Rank Gougarfs
6. Talk to Graham again then the combiner (by the store)
7. Hunt the Rebidiora at the top of the quest list
8. Talk to Graham, the Smithy, Graham again, then the Road quest receptionist
9. Post the GHC Rukodiora and kill it as hard as you can.
10. Talk to Graham and then the Smithy

After all that you’ve unlocked magnet spike! You should get a material to make the “Protospike α”";
        }
    }

    public static string GetGameMeleeStats
    {
        get
        {
            return "Attacking Monsters\u3000【Melee Weapons】\r\n●\u3000Weapon Multiplier\r\nWeapons\tMultiplier\r\nGS/LS\t4.8\r\nSnS/DS\t1.4\r\nHammer/Hunting Horn\t5.2\r\nLance/Gunlance\t2.3\r\nTonfa\t1.8\r\nSwitch Axe\t5.4\r\nMagnet Spike\t1.2\r\nBowgun/Bow\t1.2\r\nThe weapon multiplier is a number that represents the inherent strength of the weapon and is the basis for calculating the damage to the monster from the attack.\r\nThe attack power shown in the status is this weapon multiplier multiplied by a certain factor.\r\nThe multipliers for each weapon are shown in the table to the left.\r\n※The weapon multiplier can be obtained by dividing the attack power shown in the status by the multiplier shown on the left\r\n\r\nAny increase in attack power due to food effects, item use, etc. will be added to this weapon multiplier by a fixed value.\r\nTherefore, in terms of status, the amount of increase in attack power depends on the type of weapon.\r\n\r\nEx：Bloodred Drgn Sword （Weapon Multiplier：170）\u3000170×4.8＝816 Attack Power\u3000816÷4.8＝170\r\n\r\n(In some cases, the statuses may not be accurately divisible back due to rounding differences when displaying statuses.\r\nEx：Ophion BladeLV5（Weapon Multiplier：154）\u3000154×4.8＝739.2＝739 Attack Power\u3000739÷4.8＝153.96\r\n\r\n●\u3000Power of Motion Values\r\nThe same weapon can have different power depending on how it is used to attack.\r\nThe weapon multiplier multiplied by the power of each action as a percentage is the base power.\r\nThe power of each action is described on a separate page. (Untranslated section of damage Calc)\r\nEx：Bloodred Drgn Sword In the case of a vertical slash (power: 28), the basic power is 170 x 28% = 47.6 = 47\r\n\r\nAlso, depending on the type of weapon, the power will be corrected.\r\nEx：Blackbelt Sword JumpSlash（Power：18)\u3000150 x 18% x correction 125% = 33.75 = 33 is the base power\r\n\r\n●\u3000Affinity Rate\r\nAffinity rate is the probability of a critical hit when attacking.\r\nOn a critical hit, if the affinity rate is greater than 0%, the base power is multiplied by 1.25; for negative values, the base power is multiplied by 0.75.\r\n\r\nIf the hit rate is a positive value greater than 0%, the hit rate will fluctuate depending on the sharpness level.（後述：切れ味倍率）\r\nAffinity rate on the status page is displayed as fixed, but it actually depends on the level of sharpness during a quest.\r\nIn addition, a crit will not be triggered when the sharpness level is 2 (yellow) or lower.\r\n※Skill: When the plus value of the affinity rate is obtained by the expert skill, it will also be affected by the slashing ability level. (However, this does not apply when the positive value is obtained by starving wolf or SP compensation.\r\n\r\nEx：Bloodred Drgn Sword If a critical hit is made with a step-in slash\r\n\u3000\u3000 170×28%×1.25＝59.5＝59 is the base power\r\n\r\n●\u3000Additional damage by elements\r\nWhen attacking with weapons with normal elements (fire/water/lightning/dragon/ice), element damage is added in addition to normal damage.\r\nComposite elements include multiple elements in one weapon. ○ Element strengthening skills are applied to each.\n\n" +
                "The element value shown in the status is the actual element value multiplied by 10, which is 1/10 of the actual element value when calculating the damage.\r\n\r\nWhen a strengthening skill such as \"Fire Attack\" is activated, the element value will be increased.\n\n" +
                "●\u3000Abnormal Status Attacks\r\nWhen attacking with a weapon with an abnormal status attribute (poison, paralysis, sleep), there is a 1/3 chance of an attack with the abnormal attribute added.\r\nThe attribute values shown in the status are multiplied by 10 times the actual attribute values, and are calculated as 1/10 of the actual value when accumulating.\r\nIf the Status Attack Up skill is activated, the attribute value is multiplied by 1.125.\r\nIn the case of dual attributes (normal element + abnormal status), damage from the normal attribute is added even if the abnormal attribute is activated.\r\nIf the accumulation exceeds the resistance value, the monster will fall into the status condition.\r\nWhen the condition is removed after a certain period of time, the monster becomes more resistant to that attribute, thus increasing its resistance value.\r\nThe accumulated value decreases over time.\r\n\r\nWhen a monster is poisoned, the amount of poison accumulated returns to zero, but it will accumulate again if a poison attack is performed while the monster is poisoned.\r\nThe amount of accumulation stops at the current resistance value -1, but the amount of accumulation does not decrease during the poisoned state.\r\nThis accumulated amount remains unchanged even after the poison state is removed, so the monster can be continuously poisoned by performing a poison attack.\r\n\r\nIn the case of paralysis, no accumulation occurs even if a paralyzing attack is performed during paralysis. After the paralyzed state is over, the accumulation will start from 0 again.\r\n\r\nImpact attacks cause the monster to accumulate a stun value, and when the resistance level is exceeded, the monster is stunned for a certain period of time.\n\n" +
                "●\u3000Sharpness Modifier\r\nMotion\tSharpness: yellow or lower\r\nCorrection\tSharpness: Green or higher\r\nCorrection\r\nBeginning of Swing\t0.3x\t1.0x\r\nNormal Hit\t1.0x\t1.0x\r\nEnd of Swing\t0.5x\t1.0x\r\nThe sharpness modifier value is the one that modifies the power depending on which motion of the attack action hits.\r\nIf the sharpness level is 2 (yellow) or less, as shown in the left table, the power will decrease at the beginning and end of the swing of the weapon.\r\nIf the sharpness level is 3 (green) or higher, any motion will be 1.0x.\r\n（GS／LS Normal middle hit will be 1.05 instead of 1.0）\n\n" +
                "●\u3000Hit Motion\r\nWhen you attack a monster, it will perform one of the following motions: \"bounced,\" \"normal,\" \"slow motion,\" or \"hit stop\"\r\nThis motion is judged based on the hitzone values.\r\n(A hit is not repelled because the damage dealt is low, it is repelled because it cut a hard part of the monster)\r\n\r\nFormula：Part Value×Bounced Multiplier×Sharpness Modifier x Weapon Correction\r\n\r\nGauge\tBounce\tNormal\tSlow Motion\tHit Stop\r\nBlue or Lower\t23 or less\t24+\t45+\t100+\r\nWhite or Higher\t16 or less\t17+\t31+\t70+\r\nSlow Motion, Number of frames generated at hit stop （30 frames = 1 second)\n\n" +
                "Events that occur while a hit stop is in progress.\r\n・Natural recovery of HP, suspension of reduction due to terrain, poison, etc.\r\n・Natural recovery of stamina, stop depletion by motion\r\n・Stopped hunting horn song duration\r\n・Stopped reduction of the effect time of items with timed effects\r\n・No Damage\n\n" +
                "No. of Frames\tSlow Motion\tHit Stop\r\nGS\t6\t10\r\nLS\t2\t3\r\nLS：\r\nKai blade Slash / Penetration 1 hit\t1\t2\r\nSnS\t2\t4\r\nDS\t1\t3\r\nDS（Demon Dance)\t2+1x8+1\t3+1x8+3\r\nDS（True Demon Dance)\t1x3,1*3\t1x3,1*3\r\nHammer\t4\t7\r\nHammer：rotate 1hit\t1\t2\r\nHH\t4\t8\r\nLance\t2\t5\r\nGunlance\t2\t5\r\nBow(Arrow Cut)\t2\t5\r\nBow(Shoriyuken)\tFixed 9+11+1x3+2\n\n" +
                "●\u3000Increased Damage Dealt\n" +
                "Condition\tMultiplier\r\nDuring Paralysis\t1.1x\r\nImpersonating a Rock (Basarios)\t1.1x\r\nPitfalls (Only between falling\r\nand starting to struggle)\t1.1x\r\nSleep (first attack only)\t3.0x\n\n" +
                "If you attack a monster in a certain state, the damage dealt will increase as shown above.\r\n(Only non-element damage increases, element damage does not change)\r\nWhen caught in a shock trap, it is not paralyzed but shocked, so it does not increase.\r\nIf you put it in a pitfall in an undiscovered state, the effect time will be extended by about 1.7 times. In addition, when angry, it will be about 0.7 times. (Priority is given to undiscovered cases)\r\n(Rajang always stays at 6 seconds even if he is asleep when you lay down the trap)\r\nIf the monster is weak and sleeping, it is the same as when put to sleep. But, the monster's HP is gradually recovering.\r\n(except for Espinas' status at the beginning of the quest)\n\n" +
                "●\u3000Damage reduction due to part values and overall defense rate\r\nMonsters have a set part value, which reduces damage.\r\nNormal damage and element damage are each reduced by the part value.\r\nIn addition, monsters have an overall defense rate that is separate from their part values, which reduces the total damage value.\r\nFurthermore, there are monsters whose overall defense rate is corrected when they become enraged.\r\n\r\nEx：A vertical slash to the head of Rathian (total defense rate: 0.9) with a green sharpness Bloodred Drgn Sword\r\n\u3000\u3000 170×28%×1.125×70%＝37.485＝37\r\n\u3000\u3000 48 Dragon×35%＝16.8＝16\r\n\u3000\u3000 （37＋16）×90%＝47.7＝47\r\n\u3000\u3000 47 total damage\r\n\r\n●\u3000Increased player attack power\r\nThere are many ways to increase a player's offensive power, but they can be broadly divided into three groups.\r\n\u3000\u3000１：Activation of skills that increase attack power, possession of items that increase attack power (Powercharm, Powertalon)\r\n\u3000\u3000２：Up due to food effects, use of demon drug and mega demon drug\r\n\u3000\u3000３：Use of power seeds, power pills, and demon flute\r\n\r\nGroup 1 will not lose potency even if you run out during a quest.\r\nGroup 2 is effective all the time during the quest, but loses its effectiveness when you faint.\r\nGroup 3 loses potency after a certain amount of time.\r\n\r\nGroups 2 and 3 do not overlap in their respective effects within the group. In the event of overlap, the one with the greater effect takes precedence regardless of the order.";
        }
    }

    public static string GetGameBowgunStats
    {
        get
        {
            return "Attacking Monsters\u3000【Bowgun】\r\n●\u3000Weapon Multiplier\n" +
                "Weapons\tMultiplier\r\nGS/LS\t4.8\r\nSnS/DS\t1.4\r\nHamer/Hunting Horn\t5.2\r\nLance/Gunlance\t2.3\r\nBowgun/Bow\t1.2\n\n" +
                "The weapon multiplier is a number that represents the inherent strength of the weapon and is the basis for calculating the damage to the monster from attacks.\r\nThe attack power shown in the status is this weapon multiplier multiplied by a certain factor.\r\nThe multipliers for each weapon are shown in the table to the left.\r\n※The weapon multiplier can be obtained by dividing the attack power shown in the status by the multiplier shown on the left\r\n\r\nAny increase in attack power due to food effects, item use, etc. will be added to this weapon multiplier by a fixed value.\r\nThus, in terms of status, the amount of increase in attack power depends on the type of weapon.\n\n" +
                "In the case of bowguns, the power varies depending on the bullet.\r\nThe base power is the weapon multiplier multiplied by the power of the bullet as a percentage.\r\nCrag Shots explode after impact, while Clust Shots scatter their small bombs.\r\nThe explosion that occurs after landing is fixed damage and is not reduced by part values, but the fire attribute part is reduced by part values.\r\nIf the recovery shot hits a monster, it will not recover. (Damage 1 will be incurred.)\r\n\r\nThe power of heavy bowguns with compressed reloads, etc., depends on the number of bullets loaded.\r\n\u3000※The impact damage is non-elemental and fixed, but the explosion damage after the impact of the Crag Shot changes depending on the number of bullets.\r\n\r\nEx：When shooting LV2 normal bullets with a grenade launcher\u300090×12%＝10.8＝10 is the base power\n\n" +
                "●\u3000Bullet Reload Speed\n" +
                "Reload Speed Value\tMotion\r\n4 or less\tVery Fast （62）\r\n５ ～ ７\tNormal （78）\r\n8 or more\tVery Slow （103\n\n" +
                "The speed at which bullets are reloaded into the bow gun is divided into three stages as shown in the table on the left, depending on the result of the following formula.\r\n\r\n\u3000\u3000\u3000Reload Speed Value ＝ Reload value of bullets － (Reload modifier for bowgun + modifier from loading speed skill)\r\n\r\nThe value in parentheses in the Motion column is the number of frames required to reload. (30 frames = 1 second)\n\n" +
                "Bowgun reload modifier\r\nReload\tModified Value\r\nVery Slow\t0\r\nSlow\t1\r\nNormal\t2\r\nFast\t3\r\nVery Fast\t4\n\n" +
                "Modified value by loading speed skill\r\nSkill\tModified Value\r\nReload Speed-1\t-1\r\nNo Skill\t0\r\nReload Speed+1\t1\r\nReload Speed+2\t2\r\nReload Speed+3\t3\n\n" +
                "●\u3000recoil when fired\r\nRecoil Value\tMotion\r\n０ ～ ８\tSmaller （26）\r\n９ ～ １０\tMedium （56）\r\n１１ ～ １４\tVery Large （72）\n\n" +
                "The recoil generated when a bullet is fired is divided into three stages as shown in the table on the left, depending on the result of the following formula.\r\n\r\n\u3000\u3000\u3000Recoil Value ＝ Recoil value of the bullet - (bowgun recoil modifier + recoil reduction skill modifier)\r\n\r\nThe value in parentheses in the Motion column is the number of frames required for the recoil to subside. (30 frames = 1 second)\r\n\r\nNote that when the rapid fire skill is activated, the two modifiers are set to 0, and the recoil value of the bullet becomes the firing recoil value as it is.\r\nTherefore, most bullets will recoil more than without the skill.\n\n" +
                "Bowgun recoil modifier\r\nRecoil\tModified Value\r\nVery Large\t0\r\nLarge\t1\r\nMedium\t2\r\nSmall\t3\n\n" +
                "Modified by recoil reduction skill\r\nSkill\tModified Value\r\nNo Skill\t0\r\nRecoil Reduction+1\t2\r\nRecoil Reduction+2\t4\n\n" +
                "●\u3000Basic power modification by distance\r\nThe basic power of the Norm S, Pierc S, and Crag S varies with the distance after firing.\r\n\r\n\u3000\u3000Normal bullets change 1.5x ⇒ 1.0x ⇒ 0.8x ⇒ 0.5x after firing\r\n\u3000Pierce bullets and Clust bullets change after firing: 1.0x ⇒ 1.5x ⇒ 1.0x ⇒ 0.8x ⇒ 0.5x\r\n\u3000*1.7x instead of 1.5x for heavy bowguns\r\n\r\nThe graph below shows changes in power over time. Depending on the bow gun and processing level, etc.\r\nThere is a difference in bullet velocity, so the graph below is corrected for bullet velocity.\r\nThe faster the bullet speed, the more the graph is compressed horizontally, and the slower it is, the more it expands.\r\nBy the way, many heavy bowguns shoot faster than light bowguns.\r\n\r\nDue to the correction, when viewed in terms of time, the faster the bullet speed, the shorter the time period during which high power is exerted,\r\nFrom a distance perspective, the distance at which high power is exerted is almost the same regardless of the bowgun or processing level.\r\nTherefore, as long as you know the distance, you can use any bowgun to demonstrate high power at the same distance.\r\n\r\nExcept for the three bullets listed above, the basic power does not change with distance.\r\nThe power of a pellet S does not change with distance, but the number of hits does.\r\nThe various attribute bullets also do not change their attribute values with distance.\n\n" +
                "●\u3000Basic power/attribute value correction by skill\r\nSkill\tEffect\r\nNormal S UP\tNormal bullets are 1.1 times more powerful\r\nPierce S UP\tPierce bullets are 1.1 times more powerful\r\nClust S UP\tClust bullets are 1.3 times more powerful\r\nSniper\tAuto-Reload and +5 to weakness value within critical distance\r\n○Elemental Attack Up\tIncreases the power of each elemental bullet\r\nStatus Attack Up\tThe attribute value of the status bullet is 1.125x\n\n" +
                "When the skills shown in the left table are activated, the power and attribute values of the bullets are increased.\n\n" +
                "●\u3000Affinity Rate\r\nAffinity rate is the probability of a critical hit when attacking.\r\nOn a critical hit, if the affinity rate is greater than 0%, the base power is multiplied by 1.25; if it is a negative value, the base power is multiplied by 0.75.\r\n\r\n●\u3000Hit Effects\r\nThe bowgun's hit effect varies depending on the change in power after firing.\r\nThe effect becomes smaller in the order of 1.5x ⇒ 1.0x ⇒ 0.8x ⇒ 0.5x.\r\n\u3000\u3000※1.7x instead of 1.5x for Heavy Bowgun\r\n(Since it is not affected by the damage dealt, even parts that receive little damage may have a large effect.)\r\n\r\n●\u3000Increased damage dealt\r\nCondition\tMultiplier\r\nParalized\t1.1x\r\nImpersonating a Rock\u3000（Basarios）\t1.1x\r\nPitfall\u3000（Only from the time they fall\r\nto the time they start struggling)\t1.1x\r\nSleep\u3000（First Shot Only）\t3.0x\n\n" +
                "If you attack a monster in a certain state, the damage dealt will increase as shown on the left table.\r\n(Only non-element damage increases, element damage does not change)\r\nWhen caught in a shock trap, it is not paralyzed but shocked, so it does not increase.\r\nIf you put it in a pitfall in an undiscovered state, the effect time will be extended by about 1.7 times. In addition, when angry, it will be about 0.7 times. (Priority is given to undiscovered cases)\r\n(Rajang always stays at 6 seconds even if he is asleep when you lay down the trap)\r\nIf the monster is weak and sleeping, it is the same as when put to sleep. But, the monster's HP is gradually recovering. (except for Espinas' status at the beginning of the quest)\n\n" +
                "●\u3000Damage reduction due to part values and overall defense rate\r\nMonsters have a set part value, which reduces damage.\r\nNormal damage and element damage are each reduced by the part value.\r\nIn addition, monsters have an overall defense rate that is separate from their part values, which reduces the total damage value.\r\nFurthermore, there are monsters whose overall defense rate is corrected when they become enraged.\r\n\r\nEx：When a normal bullet LV2 hits the head of Rathian (overall defense rate: 0.9) at a critical distance with a grenade launcher\r\n\u3000\u3000 90×6%×1.5×50%＝4.05＝4\r\n\u3000\u3000 4×90%＝3.6＝3 Damage\r\n\r\n●\u3000Increased player attack power\r\nThere are many ways to increase a player's offensive power, but they can be broadly divided into three groups.\r\n\u3000\u3000１：Activation of skills that increase attack power, possession of items that increase attack power (Powercharm, Powertalon)\r\n\u3000\u3000２：Up due to food effects, use of demon drug and mega demon drug\r\n\u3000\u3000３：Use of power seeds, power pills, and demon flute\r\n\r\nGroup 1 will not lose potency even if you run out during a quest.\r\nGroup 2 is effective all the time during the quest, but loses its effectiveness when you faint.\r\nGroup 3 loses potency after a certain amount of time.\r\n\r\nGroups 2 and 3 do not overlap in their respective effects within the group. In the event of overlap, the one with the greater effect takes precedence regardless of the order.";
        }
    }

    public static string GetGameBowStats
    {
        get
        {
            return "●\u3000Arrow Power\n" +
                "There are three types of bow arrows: rapid, spread, and pierce, each ranging from lv. 1 to lv. 4.\r\n\u3000\u3000Rapid: 1 to 4 arrows are shot simultaneously at a point. The higher the firing level, the more arrows are shot.\r\n\u3000\u3000Spread: Shoots multiple arrows in a radial pattern. The higher the spread level, the more arrows are released.\r\n\u3000\u3000Pierce: Shoots arrows that pierce. The higher the pierce level, the greater the number of hits.\r\nThere are four types of bow arcs: Wide, Narrow, Bomb, and Slicing.\r\n\u3000\u3000Wide: Arrows have higher attack power than usual\r\n\u3000\u3000Narrow: higher than usual accumulation value of abnormal status\r\n\u3000\u3000Bomb: high stun value and damage ignores part defense\r\n\u3000\u3000Slicing: calculated by bullet quality, but attacks with the Slicing effect\r\nThe type of bow determines which type/level can be used.\r\nThe arrow power for each type is shown in the table on the left.\r\nThe base power is the weapon multiplier multiplied by the power of the arrow as a percentage.\r\n\r\nAttaching a power coating to an arrow increases its power by 1.5 times.\r\n(1.6 times for gou weapons, 1.7 times for gou weapons when Bow Demon is activated,\r\n\u3000(1.7x for 2 or more parts of Tenran Armor and a HC weapon in the Gou/HC quests)\r\nWhen an explosive coating is attached to an arrow, the stun value accumulates, calculated by the strike quality\n\n" +
                "●\u3000Charge Multiplier\n" +
                "Even with arrows of the same type/level, the base power will change depending on the charging stage.\r\nBelow shows the basic power multiplier for each charge stage.\r\n\u3000(Charge stage 4 can only be used when the Loading UP skill is activated.)\n\n" +
                "Charge Stage\tBasic Power Multiplier\tElement Multiplier\tStatus Multiplier\r\n1\t0.4 x\t0.5 x\t0.5 x\r\n2\t1.0 x\t0.75 x\t1.0 x\r\n3\t1.5 x\t1.0 x\t1.0 x\r\n4\t1.8 x\t1.125 x\t1.0 x\r\nAura 4\t1.0 x\t1.0 x\t1.0 x\r\nAura 5\t1.125 x\t1.1 x\t1.1 x\n\n" +
                "●\u3000Basic power/element value correction by skill\n" +
                "The power and attribute values of arrows are increased when the skills shown below are activated.\n\n" +
                "Skill\tEffect\r\nRapid UP\tPower of rapid arrows 1.1x\r\nPierce UP\tPower of pierce arrows 1.1x\r\nSpread UP\tPower of spread arrows 1.3x\r\n○Elemental Attack Up\tPower up for each element\r\nStatus Attack Up\tIf the attribute value of each coating is 1.125x\n\n" +
                "●\u3000Affinity Rate\r\nAffinity rate is the probability of a critical hit when attacking.\r\nOn a critical hit, if the affinity rate is greater than 0%, the base power is multiplied by 1.25; for negative values, the base power is multiplied by 0.75.\r\n\r\n●\u3000Additional damage by elements\r\nAttacking with a weapon with normal elements (Fire/Water/Thunder/Dragon/Ice) adds element damage in addition to normal damage.\r\nCompound elements include multiple elements in one weapon. ○ element strengthening skills are applied to each.\n\n" +
                "●\u3000Loading a Coating\n" +
                "If you attach a status attribute (poison, paralysis, sleep) coating to an arrow, you can shoot an arrow with the status attribute added.\r\nWhen the status attribute coating is attached, the normal element attached to the bow is nullified.\r\nAttribute values are shown in the table on the left. If you shoot multiple arrows, an attribute value will be added to each one.\r\nUnlike melee weapons, it is always triggered.\r\n\r\nThe attribute value of the status coating is multiplied by 1.125 when the condition attack enhancement skill is activated.\r\nIn addition, the attribute value is halved only at the accumulation stage 1. In the case of Poison Coatings, it will be 1.5 times higher than charge level 3.\r\n\r\nIn the case of explosive coatings, the damage is unattributed and deals a fixed value of damage.\r\n\r\nIn the case of an impact coating, the stun value is halved for charge 1.\r\nDepending on the distance, the stun value varies from 0.7x, 1.0x, and 0.8x.\n\n" +
                "●\u3000Bow Melee Attacks\r\nDescribed Elsewhere\r\n\r\n●\u3000Hit Effects\r\nBow hit effects vary depending on the change in power after firing.\r\nThe effect becomes smaller in the order of 1.5x ⇒ 1.0x ⇒ 0.8x ⇒ 0.5x.\r\n(Since it is not affected by the damage dealt, even parts that receive little damage may have a large effect.)\n\n" +
                "●\u3000Increased damage dealt\n" +
                "If you attack a monster in a certain state, the damage dealt will increase as shown on the left table.\r\n(Only non-element damage increases, element damage does not change)\r\nWhen caught in a shock trap, it is not paralyzed but shocked, so it does not increase.\r\nIf you put it in a pitfall in an undiscovered state, the effect time will be extended by about 1.7 times. In addition, when angry, it will be about 0.7 times. (Priority is given to undiscovered cases)\r\n(Rajang always stays at 6 seconds even if he is asleep when you lay down the trap)\r\nIf the monster is weak and sleeping, it is the same as when put to sleep. But, the monster's HP is gradually recovering. (except for Espinas' status at the beginning of the quest)\n\n" +
                "Condition\tMultiplier\r\nParalized\t1.1x\r\nImpersonating a Rock\u3000（Basarios）\t1.1x\r\nPitfall\u3000（Only from the time they fall\r\nto the time they start struggling)\t1.1x\r\nSleep\u3000（First Shot Only）\t3.0x\n\n" +
                "●\u3000Damage reduction due to part values and overall defense rate\r\nMonsters have a set part value, which reduces damage.\r\nNormal damage and element damage are each reduced by the part value.\r\nIn addition, monsters have an overall defense rate that is separate from their part values, which reduces the total damage value.\r\nFurthermore, there are monsters whose overall defense rate is corrected when they become enraged.\r\n\r\n●\u3000Increased player attack power\r\nThere are many ways to increase a player's offensive power, but they can be broadly divided into three groups.\r\n\u3000\u3000１：Activation of skills that increase attack power, possession of items that increase attack power (Powercharm, Powertalon)\r\n\u3000\u3000２：Up due to food effects, use of demon drug and mega demon drug\r\n\u3000\u3000３：Use of power seeds, power pills, and demon flute\r\n\r\nGroup 1 will not lose potency even if you run out during a quest.\r\nGroup 2 is effective all the time during the quest, but loses its effectiveness when you faint.\r\nGroup 3 loses potency after a certain amount of time.\r\n\r\nGroups 2 and 3 do not overlap in their respective effects within the group. In the event of overlap, the one with the greater effect takes precedence regardless of the order.";
        }
    }

    public static string GetGameSwordCrystalSkills
    {
        get
        {
            return "The Sword Crystal skill doubles the consumption of the sharpness gauge; one is consumed per hit.\r\nStatus ailments hits will always trigger.\n\n" +
                "Bomb Sword consumes 3 times the sharpness gauge, while other sword crystal skills consume twice as much. 1 consumed per hit.\r\nThe base power used in calculating the damage of bomb sword is calculated by Motion Value x Bomb sword lvl. (The weapon's attack power is irrelevant.)\r\nElement sword crystal are fixed in terms of status.";
        }
    }

    public static string GetGameWeaponTypesInfo
    {
        get
        {
            return "Weapons\r\nSP Weapons\r\nFor HR5 and G-rank quests, weapon multiplier +10 and +20% affinity.\r\n\r\nHC Weapons\r\nFor hardcore quests, melee weapons will have their sharpness gauge increased by 1 level. Bowguns/bows will have a +40% affinity boost.\r\n\r\nMaster Mark\r\nThe weapon can be Unsheathed and Sheathed 20% faster. Skill: Equivalent to Weapon Handling\r\n\r\nEvolution Weapons\r\nEvolution Weapons（Melee）\r\nPower Sword Crystals can be set. While equipped, the weapon multiplier is 1.2x, and the consumption of the sharpness gauge is 4x.\r\n\r\nEvolution Weapons（Bow）\r\nCan be equipped with an impact coating. The attack becomes a stun attribute and the damage calculation is calculated based on the hit quality. Melee accumulates 2 stun and 4 for arrows.\r\n\r\nEvolution Weapons（Light Bowgun）\r\nAcid S and Dragon Acid S can be loaded. Each bullet has a +10 part quality effect on the part where it hits, which lasts for 30 seconds. Boss monsters only\r\n\r\nGou Weapons\r\nGou Weapons（Melee）\r\nIf your HP and stamina are at maximum, your attacks will not bounce.\r\n\r\nGou Weapons（Bow）\r\nCan equip explosive coatings. While using the explosive coatings, the original attribute will be overwritten and will not be activated.\r\nAll explosove coating damage is fixed damage, and only the overall defense rate is affected.\r\nWeapons are quicker to deploy and retract.\r\nThe effect of the power coating is changed to 1.6 times.\r\n\r\nGou Weapons（Light Bowgun）\r\n・rapid-fire\r\nIf the user presses the \"fire\" key during rapid-fire, the next rapid-fire will be started in succession. (Each press of the fire key consumes ammunition.)\r\nIn that case, the number of rounds fired in rapid fire increases by one round for each additional input. (In the case of normal bullets, 5 rounds -> 6 rounds -> 7 rounds -> ...)\r\nThe recoil after firing changes with the number of times the weapon is fired rapidly, and naturally, the more times the weapon is fired, the more the recoil. (The recoil can be reduced to some extent with recoil reduction).\r\n\u3000For example, with recoil reduction +2, up to 4 shots will have small recoil, 5 or 6 shots will have medium recoil, and the 7th and subsequent shots will have large recoil.\r\n\u3000\u3000\u3000\u3000\u3000\u3000With recoil reduction +1, up to four shots will have medium recoil, and the fifth and later shots will have Large recoil.\r\n\r\nGou Weapons（Heavy Bowgun）\r\n・Heat Beam\r\nIt can be used by attaching the \"Heat Beam\" to a Gou type heavy bowgun.\r\nEach time a bullet is fired, an \"public temperature gauge\" accumulates, and when it reaches its maximum, a \"heat beam\" can be fired.\r\n\u3000：public Temperature Gauge\r\n\u3000・Base 100/minimum 40/maximum 200. Increases by 10 each time you hit with a heat bomb. Or +10 with Warm Oil, -10 with Cooling Oil.\r\n\u3000・The gauge fills up every time you shoot a bullet. After firing the bullet, reduction starts in 3 seconds, and the gauge decreases by 1 per second.\r\n\u3000・After charging is complete, the gauge will flash. Reduction starts 5 seconds after charging is complete.\r\n\u3000・If you reload the heat bomb, it will stop reducing. (Reloading itself is possible even if not accumulated)\r\n\u3000・1 Hit Fixed 22 + 10 fire damage. 6 Hits per 10 gauge (damage is affected by overall defense ratio).\r\n\u3000\u3000\u3000(Gunnery: 24 + fire 10, Artillery Expert: 26 + fire 10, Artillery God: 28 + fire 10 / HBG Tech [Gun Sage] is multiplied by 1.2 for both fixed and fire)\r\n\u3000\u3000\u3000(For initial gauge: 100 ÷ 10 × 6 × 22 = 1320 + fire 600)\r\n\u3000\u3000\u3000※2 Hit limit for small monsters?\n\n" +
                "Tenran / Supremacy (G-rank Supremacy) Weapon\r\nIf you equip 2 or more pieces of Tenran armor and go to a quest for a gou rank/supremacy rank (G-rank),\r\nMelee weapons will have their sharpness gauge increased by 1 level. Bow guns/bows will have their multiplier at critical distance increased by +0.3.\r\nIn addition, for bows, the effect of the power coating changes to 1.7";
        }
    }

    public static string GetGameArmorTypesInfo
    {
        get
        {
            return "Armor Types\r\nSP Armor\r\nIt does not have unique skills, but it is possible to equip SP decorations. You can also equip accessories with slot 1.\r\nDefense power is basically the same for all, but SP armor that uses Gou material has high defense power.\r\nFor quests with HR 100 or higher, if you equip even one part, your defense will be +100.\r\nHiden Armor\r\nEach weapon has an armor that can be created with SR 300 or more, and even if you have the materials, you cannot produce it if you do not have enough SR.\r\nIf you have both White Hiden and Red Hiden at LV7, you can activate Twin Hiden.\r\nG-class hiden armor\r\nSame as Hiden Armor\r\nGou Armor\r\nWhen equipped, the activated skill will be increased by one rank under certain conditions. (Only those with higher level skills)\r\n\r\nConditions for changing the rank of the activated skill\r\nNo. of Parts Equipped\tHP\tStamina\r\n1 Part\t100%\tMax value 26+\r\n2 Parts\t90%+\tMax value 26+\r\n3 Parts\t83%+\tMax value 26+\r\n4 Parts\t76%+\tMax value 26+\r\n5 Parts\t70%+\tMax value 26+\r\nTenran Armor\r\nIn addition to the effects of gou armor, the following effects are also available in the gou/HC/g-rank quests.\r\n・In the case of Gou/Tenran/HC weapons, the weapon multiplier increases with the number of parts equipped.\r\n\u3000In addition, the rarity limit when equipping a secret book is lowered by one per equipped part.\r\n・Equipping 2 or more pieces will change the performance of Tenran/HC Weapons.\r\nEffect when equipping Gou/Tenran/HC weapons\r\nNo. of Parts Equipped\tWeapon multiplier\tRarity limit for SR\r\n1 Part\t15\t-1\r\n2 Parts\t30\t-2\r\n3 Parts\t45\t-3\r\n4 Parts\t60\t-4\r\n5 Parts\t80\t-5\r\nSupremacy Armor\r\nIn addition to the effects of the Gou/Tenran armor, the Gou/HC/G-rank quests will also include the following\r\n・In the case of the gou/tenran/HC type weapons, the attribute and condition value will be increased by 2% per part.\r\nIn addition, the skill rank up conditions will be relaxed.\r\n\r\nCondition for changing rank of activated skill\r\nThe strength limit will be relaxed by 33% for one part of the supremacy armor and by 6% for one part of the tenran armor.\r\nEx：\r\nIn the case of 2 parts of supremacy + 3 parts of tenran: 100 - 33x2 - 6x3 = 16%+ of HP\r\nG Supremacy Armor\r\nSupremacy armor effects will be activated when you are equipped with a Gou/Tenran/HC/G rank supremacy weapon.\r\nBurst Armor\r\nThe effect of Supremacy Armor will be activated when you are equipped with Gou/Tenran/HC/G-Rank Supremacy/Burst Weapons.\r\nThe skill rank-up condition has been removed, and the skill will always be upgraded.\r\nOrigin Armor\r\nThe effects of Burst armor will be activated when you are equipped with Gou/Tenran/HC/G-rank supremacy/Burst/Origin Weapons.\r\n※However, bowgun/bows are applicable to all weapon types.\r\n・Equipping 1 or more pieces will change the performance of Tenran/HC/G-rank supremacy/Burst/Origin Weapon.\r\n・Weapon multiplier increases by 20 for 1 part, 110 for 5 parts.\r\n・Increases element value and abnormal status value by 3% per part.\r\nAlways has skill rank up.\r\nG Rank Armor\r\nIf you strengthen your GF to LV7, you can refine it into decos.\r\nEach part has a defense correction of (own GR - armor GR) x 20\r\n\u3000※In GR quests, the damage is calculated by subtracting (quest * level - 1) x 150 from the defense, regardless of the armor type.\r\nSkill slots +1 for 3 or more parts; skill slots +2 for 5 parts.\r\n・Weapon multiplier +30 when equipping 3 or more parts in G level quests.\r\nＨＣ Armor\r\nWhen equipped in HC Quest, it will restore your HP to maximum under certain conditions.\r\nHowever, HP must be at least 50%.\r\n\r\nConditions\r\nNo. of Parts Equipped\tMax Stamina\tRecovery\r\n1 Part\t150\t8 seconds for 1 HP\r\n2 Parts\t125+\t4 seconds for 1 HP\r\n3 Parts\t100+\t2 seconds for 1 HP\r\n4 Parts\t75+\t1.5 seconds for 1 HP\r\n5 Parts\t50+\t1 seconds for 1 HP\r\nSafeguard Armor\r\nWhen equipped in HC Quest, it will restore your HP to maximum under certain conditions.\r\n・In HC/Supremacy/G-rank quests, defense power is +20 for each part.\r\n・Damage is reduced in HC/Supremacy/G-rank quests after SR100. (Priority given to Halk Pot)\r\nConditions\r\nNo. of Parts Equipped\tDamage Reduction\r\n1 Part\t10%\r\n2 Parts\t17%\r\n3 Parts\t24%\r\n4 Parts\t27%\r\n5 Parts\t30%\r\nG Safeguard Armor\r\nWhen equipped in HC quests, your HP recovers to the maximum under certain conditions.\r\nEach part has a defense correction of (own GR - armor GR) x 20\r\n\u3000※In the GR quest, the damage is calculated by subtracting (quest * level - 1) x 150 from the defense, regardless of the armor type.\r\nSkill slots +1 for 3 or more parts; skill slots +2 for 5 parts.\r\n・Weapon multiplier +30 when equipping 3 or more parts in G level quests.";
        }
    }

    public static string GetGameActiveFeature
    {
        get
        {
            return "Active Feature Weapon\r\nSnS\tIncreases the effect of elemental attacks and status ailments by 1.2 times\r\nDS\tReduced the speed at which the HP bar decreases when in true demon mode.\r\nStamina gauge decrease speed is reduced to 1/2 when in demon mode (equivalent to marathon runner). When marathon runner is activated, it is reduced to 1/3.\r\nGS\t+100% Critical Rate when using an unsheathe attack\r\nLS\tAttack power increase when spirit gauge is full increased from +10 to +40\r\nHammer\tAccumulation value of stun is increased by 1.5 times\r\nHunting Horn\tMelody duration doubled\r\nLance\tIncreases performance when guarding by one level\r\nReflect+3\r\nGunlance\tThe non-attribute portion of shelling damage including wyvern fire is increased by 1.5 times.\r\nTonfa\tAll Ryuuki effects buffed\r\nHead: KO duration 20s>30s\r\nTail: Bleeding 1.5x damage\r\nBody:Sharpness return 15>20 Gunner Attack Up 25>50\r\nSwitch Axe\tIncrease Phial Meter Recovery by primary recovery mechanic(reloading or Guarding)\r\nMagnet Spike\tIncreases \"Magnetic Bind\" finisher multiplier by 1.25x(600MV -> 750MV)\r\nAlso, cutting damage to the tail in slash mode is increased by 1.1x\r\nStun value in impact mode is increased by 1.3x\r\nLight Bowgun\t+0.1x multiplier at critical distance\r\nEffect of status ailment attacks increased by 1.2 times\r\nHeavy Bowgun\t+0.1x multiplier at critical distance\r\nBow\tCharge time reduced to 85%";
        }
    }

    public static string GetGameItemInfo
    {
        get
        {
            return "Item\tEffect\r\nSonic Bomb\tProduce a Sound(Impactful)\r\nGook Cracker\tProduce a Sound(Non Impactful)\r\nGook Cracker\r\n(Amulet)\r\nProduce a Sound(Impactful)\r\n??? Doll\tKickable\r\n??? Doll\r\n(Rarely when using Gook Amulet)\r\nBig Kick\r\nGook Fireworks\t3 Shots in the air\r\nGook Fireworks\r\n(Amulet)\r\n5 Shots in the air\r\nRaw Meat\tCan be eaten\r\nOnly some monsters\r\nFlash Bomb\tFlash condition, increased hate\r\nFlute\r\n(HC quest not allowed)\r\nIncreased Hate\r\nGook Whistle\tReduce Hate\r\nGook Whistle\r\n(Amulet)\r\nReduce Hate(Large)\r\nDung Bomb\tMove a Monster\r\n(Only while unseen)\r\nSmoke Bomb\r\n(HC quest not allowed)\r\nHard to spot\r\nSupressing Ball\tRemoves Hate\r\nWeapon\tPower\tNotes\r\nBallista\t100\t2 strikes per 1 hit\r\nCannon\t250\t\r\nDragonator(Fortress）\t255×4 Applied\t10 Min Cooldown\r\nDragonator(Town）\t255×2 Applied\t10 Min Cooldown\r\nDragonator(Schrade）\t255×4x\t10 Min Cooldown\r\nCastle Gate(Schrade）\t255\tOnly Once\r\nBombs are 1.5 times more powerful with the skill \"Bomber\"\r\nThe bomber skill applies to the \"installer\" and the detonation can be done by anyone.\r\nIn addition, a stun value of 2 is added to barrel bombs other than attribute/status abnormal type barrel bombs.\r\n\r\nThe ballista and cannon vary in power with the skill \"Gunnery\".\r\n\r\nBoomerangs have a 1/8 chance of being lost.\r\nSkill \"Throwing Distance UP\" avoids boomerange loss of 1/8\r\n\r\nStrong Arm +1 increases damage of thrown items by 1.1x\r\nStrong Arm +2 further increases thrown item damage by 1.3x\r\n\r\nThrowing Knife +1 allows you to throw 3 knives at once with 1 knife\r\nThrowing Knife +2 allows you to throw 5 knives at once with 1 knife";
        }
    }

    public static string GetGameSharpnessInfo
    {
        get
        {
            return "Decreased Sharpness／Recovery\r\nAction\tAmt\r\nNormal Attack\t-1\r\nBounced\t-2\r\nGS Guard\t-1～-10\r\nSonic Wave (HH)\t-10\r\nShelling(Normal)\t-3\r\nShelling(Long)\t-4\r\nShelling(Spread)\t-4\r\nWyvern Fire\t-20\r\nSuper Wyvern Fire\t-24\r\nKnife Mackerel\t150\r\nLg Knife Mackarel\t200\r\nMini-Whetstone\r\n(Supplies)\r\n100\r\nWhetstone\t150\r\nG Whetstone\t200\r\nHi-Speed Whetstone\t75\r\nFast USe\r\nHi-Qual Whetstone\r\n(Caravan Only)\r\n400\r\nPerfect Whetstone\t400\r\nFast USe\r\nExpert Whetstone\t150\r\nAffinity +10 for 1 minute\r\nSkill Points：Razor Sharp\" and \"Blunt Edge\" affect sharpness by these amounts\r\n\r\nRazor Sharp+2\tSharpness loss is halved where applicable, 50% chance of any sharpness loss being completely negated.\r\nRazor Sharp+1\tDecrease by half (rounded down, minimum 1)\r\nBlunt Edge\tSharpness loss is doubled.\r\nSkill Points：\"Slothful Sharpening\" activated by the sharpening skill affects the sharpening speed\r\n\r\nSpeed Sharpening\tSharpen with a single stroke of a whetstone.\r\nSlothful Sharpening\tSharpening duration is doubled.";
        }
    }

    public static string GetGameMonsterSizeInfo
    {
        get
        {
            return "Monster Size\r\nThe monster size is determined by the basic size and variable limit width set for each quest.\r\nBasic size % × fluctuation value % (within fluctuation limit range)\r\n\r\nThe basic size is set for each quest, and is expressed in % inside.\r\nThis is multiplied by the randomly determined variation value % (varying up and down around 100), and the resulting % multiplied by the size of the monster at 100% determines the size.\r\n\r\nThe variation table is first selected from one of the following seven types.\r\n\r\n\r\nFluctuation Range\tChance\r\nTable 1\t75％～79％\t1/32\r\nTable 2\t80％～84％\t1/32\r\nTable 3\t85％～94％\t2/32\r\nTable 4\t95％～105％\t19/32\r\nTable 5\t106％～115％\t6/32\r\nTable 6\t116％～120％\t2/32\r\nTable 7\t121％～125％\t1/32\r\nOnce a variation table is selected, the variation values within that table are randomly determined.\r\nHowever, a variation limit range is set in the quest data, which is modified to fall within the following range\r\n(One of the following five types. Which limit width depends on the quest)\r\n\r\nPattern\tWidth Limit\r\n0\t100％ Fixed\r\n1\t100％～105％\r\n2\t95％～110％\r\n3\t87％～118％\r\n4\t80％～125％\r\nIt may be +10% or -10% depending on some conditions. Those conditions are unknown. (Continuous hunting, good time of day? Come on big guy!)";
        }
    }

    public static string GetGameRewardInfo
    {
        get
        {
            return "About quest rewards\r\n●\u3000How to view the reward frame\n" +
                "・The top three rows (red boxes) are the basic reward slots you will receive when you achieve the main target.\r\n・The four left frames in the middle row (green frames) are the reward frames you will receive for achieving Sub-Target A.\r\n・The four right frames in the middle row (blue frames) are the reward frames you will receive when you achieve Sub-Target B.\r\n・The bottom two rows (white frame) are the reward frames received for achieving capture and site destruction.\r\n・The bottom three right frames (yellow frames) are, from left to right, NetCafe rewards, premium SR book, and regular SR book frames.\r\n\r\n※Attention\r\nIf more than one of the same monster the target, the reward for capturing and destroying parts of the monster will only be for one monster.\r\nIn addition, the following monsters will be added to the basic reward frame depending on their status\r\n・Additional rewards for making Pariapuria vomit\r\n・Additional reward for tripping White Espinas while in flame circle.\r\n・Additional reward for hunting HC Red Rajang\r\n・HC tickets, souls of each weapon\r\n\r\n２．Legendary Poogie Reward Frame\n\n" +
                "・The three slots on the first tier (red slots) are the boss monster's stripped reward slots.\r\n・The second row of four frames (white frames) are the collection reward frames for that location and season.\r\n\r\n※Attention\r\nThe maximum number of stripped reward slots is 3, even if you hunt multiple boss monsters.\n\n" +
                "※Attention\r\n・When Guild Poogie's \"Poogie Reward Technique\" is activated, the basic reward frame may exceed the upper limit and all 3 rows (24 frames) may become reward frames.\r\n・When a PT (2-4 people) of the same hunting party goes on a quest with different street name titles set for each other, the \"Street Name Title Skill (equivalent to Luck)\" will be activated with a certain probability. (Priority is given to the \"Great Luck\" skill.)\r\n・If you have a Super Lucky Charm, a Large Lucky Charm, or have purchased the Assist Course, it has the same effect as the Great Luck Skill.\r\n・Some quest rewards have fixed slots, in which case only the specified amount of reward slots will appear, regardless of luck skill, etc.\n\n" +
                "●\u3000Obtaining Reward Materials\r\nOnce the number of frames has been determined, reward materials will be picked via RNG from a list according to the probability, and will be placed in order from the first frame.\r\nQuest rewards may have a fixed item, and always only the first slot will have a fixed item, not a RNG.\r\nFinally, each reward is sorted by item code.\r\nAdditional rewards for hunting HC Red Rajang, HC tickets and souls for each weapon will be added afterwards.\r\n\r\nIf you have the following charms, the number of materials obtained will be increased.\r\n・Super Lucky Charm：There is a 50% chance that the materials obtained will be doubled.\r\n・Large Lucky Charm：There is a 15% chance that the materials obtained will be doubled.\r\n\r\nThe \"Super Lucky Charm\" and the \"Large Lucky Charm\" can be used together. When they are used together, the \"Super Lucky Charm\" will activate the \"Super Luck\" skill, and then the \"Large Lucky Charm\" will activate the \"Great Luck\" skill.\r\n\r\n※Attention\r\n・In the case of practice quests, when \"no items\" is selected, the drawing ends there and no further drawings will be made.\r\n・Some quests have a fixed quota of rewards, in which case you only get the specified number of rewards, regardless of your charms, skills, etc.\n\n" +
                "Daily Special\r\nThe following benefits are available once a day (updated at 12:00) in the \"Daily Special\" section of the General Shop.\r\nThe maximum N-point limit is 60,000 P. Others are overwritten each time they are received.\r\n\r\nN points\tTrial course: 1 point\r\nHunter Life Course: +3 points\r\nExtra course: +1 point\r\nPoint Bonus Rights\t3x\r\nGet Halk Pot\t5x\r\nDaily Quest Rights\t1x";
        }
    }


    public static string GetGamePartnerInfo
    {
        get
        {
            return "Partner\r\nYou can register at the \"Rasta Tavern\" which can be entered from the entry zone in Mezeporta Square.\r\nAccept the quest and bring back materials.\r\nRequirements\r\n\u30001.HR2+\r\n\u30002.Purchased House Expansion 1\r\nRegistering a Partner\r\nTalk to a Legendary Rasta and select \"Partner\" to start partner registration.\r\nThe weapon type of the Legend Rasta you spoke to at that time becomes the partner's initial weapon type.\r\n\u3000(Other weapon types can be acquired by talking to the corresponding Legend Rasta for every PR10.)\r\nOnce you have selected your gender and personality, your partners appearance will be selected in your House.\r\nYou can change your partners appearance at the beauty salon in the plaza, just like a hunter.\r\n\r\nNPC accompany priority\r\nLegendary＞Partner＞Temporary Rasta＞Normal Rasta＞Fosta\r\n\r\nPartner Equipment\r\nThe hunter can \"gift\" equipment to the partner so that they can equip it.\r\nMy Set registration, decos, and Sigils on equipment must be removed before they can be gifted.\r\n\u3000(The gifted equipment will disappear from the hunter's Equipment box and will be stored in the partner's exclusive equipment box.)\r\nEven after the gift is given, equipment can be upgraded by selecting the partner at the blacksmith.\r\n\r\nArmor\r\n・Defense is the same for each type regardless of LV\r\n・No skills or special effects\r\nWeapons\r\n・Weapon power is the same for each type\r\n・Element value is 0, status attacks such as para or sleep are transferred\r\n\u3000(The abnormal status value is halved for tonfa)\r\n・No special effects\n\n" +
                "Partner Growth\r\nBy having them accompany you on quests, your PR (partner rank) and \"weapon proficiency\" will increase.\r\nPR leveling is the same as HR. Obtained PRP calculation is as follows\r\nNormal Quest\r\n・HRP obtained×0.4=PRP\r\n・HRP obtained×0.2=Weapon proficiency\r\nG-rank quest\r\n・GRP obtained×1.2=PRP\r\n・GRP obtained×0.7=Weapon proficiency\r\nWhen the proficiency of one weapon is at 999, the weapon multiplier gains a +5 bonus, which is also applied to other weapons.";
        }
    }

    public static string GetGameRastaInfo
    {
        get
        {
            return "Trusted Hunter(Rasta)\r\nCan be registered at the \"Rasta Tavern\", which can be entered from the entry and free zones after HR2.\r\n\u30001.Talk to the \"Rasta Receptionist\" and receive an order for \"Hunting an Yian Kut-Ku while logged in.\"\r\n\u30002.Hunt one Yian Kut-Ku. Any quest is fine.\r\n\u30003.Head to the \"Rasta Tavern\" again and talk to the \"Rasta Receptionist\".\r\n\u30004.Receive \"Hunter Knife\" and you will complete \"rasta registration\". Weapon will appear in the empty space in the BOX!\r\n\r\nTrial Rasta\r\nFree trial rasta. You can use it as many times as you want, but you need to make a contract for each quest.\r\nNote that guild contribution points are not accumulated by having them accompany you on quests.It's just a sample.\r\n\r\nTemporary Rastas\r\nFrom the status screen of other hunters, you can make a temporary contract by selecting \"Show sub menu\" > \"Confirm rasta\" > \"Temporary rasta\".\r\nThe contract will continue until you log out.\r\n\r\nLegendary Rastas\r\nA contract can be signed by purchasing assist course. Contracts are made by talking to each legendary rasta at the \"Rasta Tavern.\"\r\nLegendary rastas can be replaced at any time.\r\n\r\nLegendary rasta can participate in all quests except hunter dojo (excluding solo quests, of course).\r\nAs a bonus, great luck is always in effect.\r\nA Legendary Poogie accompanies you and brings you items during quests (better than regular Poogie)\r\nIn addition, one reward screen has been added after the quest ends, and you can randomly obtain materials for gathering, mining, and fishing.\r\nOn that screen, when you hunt a large monster, you can get 3 additional frames of carving materials.\r\n(In the case of HC quests, HC materials are also included.)\r\nContract Rasta\r\nThere are two types of contracts: “accompany contract” and “relief contract”.\r\nHunter B\t←(I want you to come on a quest)←\tHunter A\t\tAccompanying contract (one person only)\r\n→(I'm going on a quest.)→\t\tRelief contract (can contract with up to 3 people)\r\nThe \"accompaniment contract\" made by Hunter A becomes a \"relief contract\" from the perspective of Hunter B.\r\nThe contract should have both characters facing each other. (Additional account characters as the main, is not allowed.)\r\n・Contract Procedure\r\n\u30001.The person making the \"relief contract\" sits in the \"relief contract\" chair.(a chair with a backrest).\r\n\u30002.The person making the \"accompanying contract\" sits in the \"accompanying contract seat.\" (round chair)\r\n\u30003.If both parties select \"Yes\" on the contract screen, the contract will be completed.\r\n\u30004.The party who took the \"Accompanying Contract\" needs to set \"Rasta Accompaniment\" in the \"Rasta Reception\" section.\r\n※If you do not set \"rasta accompany\", Rasta will not come during the quest and you will not accumulate guild contribution points, so be careful not to forget to set it※\n\n" +
                "Rasta Accompaniment\r\nIf the \"Rasta Accompaniment\" option is set in \"Rasta Reception\", a rasta will accompany you during the quest.\r\n※If you do not set \"rasta accompany\", Rasta will not come during the quest and you will not accumulate guild contribution points, so be careful not to forget to set it※\r\nAcoompany Priority\r\n1：Legendary Rasta\r\n2：Temporary Rasta\r\n3：Rasta (including Instructor Rasta and Secret Instructor)\r\n4：Fosta\r\n\r\n◎Notes\r\n・If the maximum number of participants in the quest is reached, Rastas will not join you.\r\n\u3000If quest is set to four and three hunters join, one of someone's rastas will be chosen at random.\r\n\u3000If quest is set to two and two hunters join, Rastas will not join.\r\n\u3000Rasta will not appear with Single-person, item restrictions, weapon and armor restrictions, combat maneuvers, solo only, etc.\r\n・Compensation for quests is also shared with rasta\r\n\u3000Example: If you clear Counterattack Solo (with 2 or more participants) with a Rasta, the reward will be halved. (The rasta portion will be lost.)\r\n\u3000※The reward money that disappeared does not go to Rasta, it just disappears.\r\n・If you don't want to lose rewards, just reduce the number of quest participants. (No issues with GRP, amount does not change)\r\n\r\nGuild Contribution Points\r\n・1 point will be added to you and your Rasta companion hunter for every minute you participate in a quest.\r\n・If you set \"rasta to accompany\", you can get points even if the rasta does not actually participate in the quest.\r\n・Points will not be awarded if:\r\n\u3000\"Rasta accompany\" is not set\r\n\u3000Retire or fail the quest\r\n\r\nObtaining materials from a rasta\r\n・When your rasta accompanies another hunter on a quest, he/she brings back materials from the monster he/she hunted.\r\n・Materials brought back by the Rasta can be obtained from Rasta's receptionist at the Rasta Tavern, \"Receive materials\".\r\n・You can receive it twice a day (once from 0:00 to 11:59, once from 12:00 to 23:59). Stores up to 5\r\n\r\nCancelling\r\nCancellation is done by talking to the \"Rasta Reception\".\r\n※Cancellation becomes possible after one week from the signing of the contract.\r\n\r\nForced Cancellation\r\nWhen 5 maintenances have passed since the last quest accompaniment\r\n※Note that if you go on a quest with the hunter you contracted from, you are not considered to have \"accompanied\" his rasta.\r\n\u3000But since you can go on a quest where the other hunter is not there, forced cancellation does not happen often.\n\n" +
                "Rasta Enhancement\r\n・The same settings as yours are used for appearance. (Cannot be changed)\r\n・Weapons and armor can be equipped with items that you own.\r\n・It is possible for you and Rasta to be equipped with the same weapons and armor.\r\n※However, items equipped by Rasta cannot be upgraded or sold.\r\n\r\nUnlocking Weapon Types\r\n・Unlock Procedure\r\n\u30001.Select \"Release rasta function\" → \"Unlock weapon type\" → \"Hunting order required for release\"\r\n\u30002.Select the target monster from the \"Hunting order list\" and accept the order. (only one at a time)\r\n\u30003.Hunt the target monster, regardless of whether it is a high or low rank monster.\r\n\u30004.Select a weapon from \"Release rasta function\" → \"Unlock weapon type\" → \"Select weapon type to release\"\r\n\u3000\u3000※The number of target monsters cleared is the number of weapon types that can be opened. Any of them can be opened in any order.\r\n\r\nTarget monster\r\n・Berukyurosu\r\n・White Hypnoc\r\n・Kamu Orugaron\r\n・Nono Orugaron\r\n・Orange Espinas\r\n・Akura Vashimu\r\n・Pariapuria\r\n・Rajang\r\n・Volganos\r\n・Rathian\r\nWeapon LV and Armor LV release\r\n・Release Procedure\r\n\u30001.Select \"Release Rasta Functions\" and then \"Release Armor LV.\"\r\n\u3000\u3000Guild loyalty UP: By releasing it, you can draw out attack power and attribute attack power\r\n\u3000\u3000Guild reliability UP: By releasing it, you can draw out defense power and the number of skill activations\r\n\u30002.Pay guild contribution points according to open LV\r\n\r\nOpen Lv\tGCP\r\n1\tUnlocked\r\n2\t200GP\r\n3\t300GP\r\n4\t500GP\r\n5\t500GP\r\n6\t700GP\r\n7\t700GP\r\n8\t800GP\r\n9\t900GP\r\n10\t1000GP\r\n11\t1100GP\r\n12\t1200GP\r\n13\t1200GP\r\n14\t1200GP\r\n15\t1200GP\r\n16\t1200GP\r\n17\t1200GP\r\n18\t1200GP\r\n19\t1200GP\r\n20\t1200GP\n\n" +
                "Behavior During Quests\r\n・If they take more than a certain amount of damage, they will temporarily leave the quest and return to the quest after a certain amount of time has passed.\r\n\u3000\u3000※If they leave, it will not be included in the death count.\r\n・Rastas HP is affected by Bentos\r\n・Action priority (just a guess)\r\n\u3000１．Received a status ailment attack: Use an item or remove it quickly\r\n\u3000\u3000※Snowball and crystals are removed quickly. Poison and stench use items.\r\n\u3000\u3000※Only while under stench, the rastas may use deodorant balls on hunters (low priority).\r\n\u3000\u3000※May cancel hunter's earplugs and stunned status\r\n\u3000２．Boss monster in the area: Attack the boss monster\r\n\u3000\u3000※Throw paintball first (will not throw if rasta has psychic skills)\r\n\u3000\u3000※They cannot defeat boss monsters （will be stuck at about 10 HP)\r\n\u3000\u3000※Prioritize attacking medium-sized monsters.\r\n\u3000\u3000※don't attack monsters while they are sleeping\r\n\u3000３．Boss monster attacks: Guard if it is possible to guard (performance is lower than auto guard)\r\n\u3000４．Miscellaneous small monsters nearby: Attack the miscellaneous small monsters.\r\n\u3000５．There is a monster corpse nearby: Carve it\r\n\u3000６．Damage was taken. Attacked. Stamina decreased: use potions, whetstone, and cooked meat.\r\n\u3000７．Hunter moves: Moves by automatically tracking the hunter\r\n\u3000\u3000※When the hunter moves to another area, they use a nearcaster and move together. (Area movement is top priority)\r\n\u3000\u3000※Drink a hot drink / cool drink when moving to the area (if it is required)\r\n\u3000\u3000※if the hunter moves to a place where it can't be followed, it will warp to the hunter's location with a nearcaster.\r\n\r\n・If you continuously attack Rasta, it will perform the action \"Shake Head\"\r\n・Come closer to the hunter with the action \"call over\"\r\n・If a rasta blasts or uses a move that would normally launch a hunter, it will not launch the hunter.\r\n・The bow is shot at charge Lv 3. It may also be shot with a coating. The coating will be refilled in a certain amount of time? replenished in a certain amount of time?\r\n※Legendary Rastas\r\n・Legendary Rasta's attacks do not hit hunters. Hunter attacks do not hit Legendary Rastas\r\n・Will chat with you during a quest and let you know when you can capture a monster.\r\n・Prioritize attacking boss monsters\r\n・Adrenaline +2, Starving Wolf +2 is always in effect\r\n・Boss monsters can be killed.\r\nRasta peculiarities\r\n・Sharpness gauge does not decrease. But they still sharpen.\r\n・will eat meat even if it's not hungry\r\n・Running does not decrease stamina.\r\n・Health may not be affected by bentos?\r\n・Defenses and damage calculations are different from hunters? (Hard to say)\n\n" +
                "Item Lending\r\n・You can borrow items used by Rasta at the Rasta Tavern.\r\n\u3000\u3000※No need to pay it back, no time limit. Simply set it up.\r\n・Guild contribution points are not required for each quest\r\n・There is a limit to the number of these available per quest, but they are replenished each quest.\n\n" +
                "For Legendary Rastas、Dust Set、Flute Set、Each item in the throwing knife set can be set individually.\r\nThe difference between legendary and normal rasta is that the demon flute and armour flute can be reused with legendaries.\n\n" +
                string.Empty;
        }
    }

    public static string GetGameHalkInfo
    {
        get
        {
            return "Halk\r\nThey will go on quests with you and help you defeat monsters.[HR1～]\r\nUnlike Rasta, it is not included in the number of participants, and 4 halks will participate in a 4-person PT.\r\nBy raising them, you can increase their strength, attack power, etc., and also enable them to attack with elements.\r\nWhen completing hunting quests, halk feathers, halk calls, and Skill Books may be dropped.\r\n\r\nMy Halk\r\nYou can call the halk when you approach the rock in My Support.\r\nWhen the Halk is on the rock, you can feed it.\r\nBy talking to the instructor, you can set up Skill Books and change the halk's name.\r\n\r\nStatus\r\nThe initial value is randomly distributed so that the total of health, attack power, defense, and intellect is 10.\r\n\"Intimacy\"、\"Health\"、\"Attack\"、\"Defense\"、\"Intellect\" Halk will level up when all five values reach the right end of the gauge.\r\nLV1:Max 255\u3000LV2:Max 510\u3000LV3:Max 765\r\nAs they level up, not only do their abilities change but also the halk's appearance changes.\r\n\r\nLV\tAs level increases、Each status in increased\r\nAttack Element\tAttack element when attacking.[None、Fire、Water、Thunder、Dragon、Ice]\r\nStatus Attribute\tStatus Attribute when attacking.[None、Para、Poison、Sleep]\r\nFullness\tState of fullness; cannot be fed when 100.\r\nIntimacy\tAffects the probability of dropping items during the quest.\r\nHealth\tAffects the damage that can be sustained.\r\nAttack\tAffects damage done.\r\nDefense\tAffects the damage received.\r\nIntellect\tAffects the accuracy of Halk's actions.\r\nThe higher the number, the less wasteful actions taken.\r\n\r\nAttack Element\tWhen one of the element reaches 100, the element is determined. The other elements will be 0.\r\nIf you want to change an element, raise another element to 100 and it will change.\r\nThe appearance changes depending on the element.\r\nStatus Attribute\tWhen one of the attributes reaches 100, the attribute is determined. The other attributes will be 0.\r\nIf you want to change an attribute, raise another attribute to 100 and it will change.\r\nTraining\r\nThe increase value is determined by the clear time of a quest\r\n\r\nTrain Health\tIncreases Health\r\nTrain Attack\tIncreases Attack\r\nTrain Defense\tIncreases Defense\r\nTrain Intellect\tIncreases Intellect\r\nNot Training\tIncreases Intimacy\n\n" +
                "Skill Book\r\nA book that may be dropped by the halk at the end of a quest. (Hunting, slaying, capturing)\r\nYou can equip up to 2 by talking to the instructor in My Support.\r\nAt GR150, Halk becomes G-rank and can equip up to 3 skill books.\r\n\r\nBook\tQuest\tEffect Description\r\nLarge Monster Priority\tAll\tThey will give priority to attacking large monsters regardless of the distance from their hunters.\r\nSmall Monster Priority\tAll\tThey will give priority to attacking small monsters regardless of the distance from their hunters.\r\nNear Hunter Priority\tAll\tThey will give priority to attacking near partner hunters.\r\nAlways Aggressive\tAll\tHalk will always attack regardless of whether the partner hunter is sheathed.\r\nItem Defender\tAll\tWhen a partner hunter uses an item, it will attack nearby monsters.\r\nRising Intent\tAll\tIf partner hunter dies twice, Halk's attack power increases.\r\nCarving Defender\tAll\tAttacks monsters around partner hunters while they are carving.\r\nUnescapable\tAll\tAttacks the target that Halk himself has targeted without changing for a while.\r\nHealth Conscious\tAll\tWhen Holk's own HP decreases, he performs his own recovery action.\r\nHelper [Health]\tAll\tWhen the partner hunter is in a low health state, perform a health recovery action.\r\nApplies to non partnered hunters as well\r\nHelper [Antidote]\tAll\tIf the partner hunter becomes poisoned, perform a poison recovery action.\r\nApplies to non partnered hunters as well\r\nHelper [Deoderant]\tAll\tIf the partner hunter becomes stinky, perform deodorant action.\r\nApplies to non partnered hunters as well\r\nGuuku Luck\tAll\tRare gooks are more likely to be encountered during gook quests.\r\nFast Riser\tAll\tShortens the time it takes for Halk to return after he leaves the battlefield.\r\nBomber\tAll\tHalk will set barrel bombs (non-attribute bombs).\r\nCounts as a player setting the bomb\r\nUnflinching\tAll\tWhen Halk receives damage, it becomes less likely that he will be sent flying.\r\nMonster Spotter\t～HR30\tIt sometimes gives the effect of a psychoserum to the hunter.\r\nMapper\t～HR30\tEven without a map, the hunter can see the entire Mini-Map\r\nBreath Ball Attack\tHR31～\tHalk shoots a spherical breath attack.\r\nClaw Attack Focus\tHR31～\tHalk will perform more claw attacks (Status attacks).\r\nMore Pugi Appearances\tHR51～\tShorter intervals between Poogie appearances during quests.\r\nFlying Wyvern Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nFanged Beast Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nPiscine Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nCarapaceon Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nBird Wyvern Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nElder Dragon Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nElemental Beam Attack\tHC\tHalk will shoot a linear breath attack.\r\nDemon Drug Dust\tHC\tGives partner hunters the same effect as demon drug.\r\nApplies to non partnered hunters as well\r\nArmour Drug Dust\tHC\tGives partner hunters the same effect as armorskin.\r\nApplies to non partnered hunters as well\r\nAffinity Added\tHC\tHalk's attacks will sometimes be critical.\r\nImprove Status Attack\tHC\tHalk's status value from claw attacks is increased.\r\nImprove Evasion\tHC\tWhen Halk is attacked, he may evade it.\r\nImprove Attack\tHC\tIncreases Holk's attack element value.\r\nSignal to Halt Attacking\tAll\tHalk will stop attacks if you use the signal function.\r\nImprove Fire Element\tHR100～\tIncreases Halk's fire element attack value. * No effect at all if not the element of your halk\r\nImprove Ice Element\tHR100～\tIncreases Halk's ice element attack value. * No effect at all if not the element of your halk\r\nImprove Water Element\tHR100～\tIncreases Halk's water element attack value. * No effect at all if not the element of your halk\r\nImprove Thunder Element\tHR100～\tIncreases Halk's thunder element attack value. * No effect at all if not the element of your halk\r\nImprove Dragon Element\tHR100～\tIncreases Halk's dragon element attack value. * No effect at all if not the element of your halk\r\nSmall Bomber\tHR100～\tHalk will now set small barrel bombs.\r\nSupporter\tHC\tHalk attempts to break hunter out of any statuses such as Snowman or Crystallisation\r\nElemental Bite\tHC\tHalk will perform a powerful bite attack with elemental values.\r\nBrute Wyvern Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nBreath Ball Support\tHR100～\tHalk attacks with an emphasis on breath ball attacks. *There is no effect unless the breath ball attack is active.\r\nAttack Focus\tHC\tHalk's attack power increases, but his defense decreases.\r\nDefense Focus\tHC\tHalk's defense increases, his attack power decreases.\r\nJungle Attack\tHR31～\tIf the quest destination is the Jungle, Halk's attack power will increase slightly.\r\nDesert Attack\tHR31～\tIf the quest destination is the Desert, Halk's attack power will increase slightly.\r\nSwamp Attack\tHR31～\tIf the quest destination is the Swamp, Halk's attack power will increase slightly.\r\nSnowy Mountains Attack\tHR31～\tIf the quest destination is the Snowy Mountains, Halk's attack power will increase slightly.\r\nVolcano Attack\tHR31～\tIf the quest destination is the Volcano, Halk's attack power will increase slightly.\r\nTower Attack\tHR31～\tIf the quest destination is the Tower, Halk's attack power will increase slightly.\r\nForest and Hills Attack\tHR31～\tIf the quest destination is the Forest and Hills, Halk's attack power will increase slightly.\r\nGreat Forest Attack\tHR31～\tIf the quest destination is the Great Forest, Halk's attack power will increase slightly.\r\nArena Attack\tHR31～\tIf the quest destination is the Arena, Halk's attack power will increase slightly.\r\nGorge Attack\tHR31～\tIf the quest destination is the Gorge, Halk's attack power will increase slightly.\r\nHighlands Attack\tHR31～\tIf the quest destination is the Highlands, Halk's attack power will increase slightly.\r\nTidal Island Attack\tHR31～\tIf the quest destination is Tidal Island, Halk's attack power will increase slightly.\r\nWind Pressure\tHC\tHalk will not react to monster wind pressure attacks.\r\nEarplugs\tHC\tHalk will not react to monster roars.\r\nLone Attack\tHC\tIf there is only one quest member, Halk's attack power and defense will increase.\r\nParty Attack\tHC\tIf there are 4 quest members, Halk's attack power and defense will increase.\r\nGathering Affection\tHC\tHalk will no longer attack but will drop items instead.\r\nDanger Affection\tHC\tYou are more likely to spawn in the area of a Large Monster※Supported quests only。\r\nSecret Area Affection\tHC\tYou are more likely to spawn in the secret area if a map has one.※Supported quests only。\r\nLeviathan Offense\tHR100～\tHalk's attack power will increase slightly against the listed monster type.\r\nRapid Breath Attack\tHC\tHalk will now perform an attack in which he spits out a series of spherical breaths.\r\nEmpower Attack and Def\tHC\tHalk will spend time charging up, increasing attack and defense.\r\nCharging Attack\tHC\tHalk will rarely do a damaging high speed attack.\r\nSmash Attack\tHC\tHalk gains the ability to do a spiralling rush attack.\r\nPolar Sea Attack\tGR\tIf the quest destination is the Polar Sea, Halk's attack power will increase slightly.\r\nSummer Assault\tGR\tHalk's Attack and Defense increase while in Hot Areas.\r\nWinter Assault\tGR\tHalk's Attack and Defense increase while in Cold Areas.\r\nContain Monster\tGR\tLarge monsters are less likely to move areas.\r\nSwift Battle\tGR\tHalk's attack power increases, but decreases as quest time passes.\r\nLong Battle\tGR\tAs the quest time goes on, halk's attack power increases.\r\nPainted Falls Attack\tGR\tIf the quest destination is the Painted Falls, Halk's attack power will increase slightly.";
        }
    }

    public static string GetGamePartnyaaInfo
    {
        get
        {
            return "Partnya\r\nThey will accompany you on quests and help you defeat monsters. [HR1~]\r\nUnlike Halks, Partnya's are treated as a party member.\r\nBy raising them, you can increase their Health, attack power, etc.\r\nYou will be able to hire more partnyas when the previous one reaches rank 300.\r\n\r\nStatus\r\nThe initial and maximum values of their abilities are determined by the partnya's nature. They also increase by training.\r\nIncreasing the PNR (partnya rank) increases the value of the abilities.\r\nNormal quest: HRP earned x 0.25\r\nG rank quest: GRP earned x 1.25\r\n\r\nHealth\tThis is the Health of the Partnya\r\nAtk Power\tPartnya's attack power.\r\nDef Power\tPartnya's Defense\r\nBravery\tThe higher the number, the more powerful the attack is likely to be than usual.\r\nReflexes\tAffects the accuracy of guarding. The higher the number, the more accurate the guard will be.\r\nLuck\tIf the number is high, the partnya may bring back materials after completing a quest.\r\nNature\tEach nature type has a different method of status growth and different lines of dialogue.\r\nBehavior\tThis is the pattern of behavior during the quest. Each behavior has a different partnya movement.\r\nAttack Type\tThis is the method of attack used by partnyas in a quest.\r\nHappiness\tAffects movement accuracy. The higher the number, the less useless actions you will take.\r\nIf you use a Felvine, it will be maximized.\r\nStatus\tShows what the Partnya is currently doing.\n\n" +
                "Nature\tHealth\tAtk\tDef\tBravery\tReflex\tLuck\r\nPatient\t200\t80\t190\t5\t35\t5\r\nNaughty\t250\t85\t190\t5\t5\t5\r\nHot Blooded\t200\t100\t130\t35\t5\t5\r\nCareful\t200\t100\t130\t5\t35\t5\r\nObedient\t200\t95\t160\t12\t12\t15\r\nSerious\t200\t95\t205\t10\t10\t5\r\nNaughty\t200\t100\t130\t5\t5\t35\r\nCozy\t200\t80\t130\t5\t15\t45\r\nMax Value\r\nPatient\t350\t100\t420\t50\t200\t35\r\nNaughty\t450\t165\t485\t62\t62\t60\r\nHot Blooded\t300\t180\t255\t200\t50\t35\r\nCareful\t250\t165\t255\t90\t200\t35\r\nObedient\t350\t140\t345\t87\t87\t95\r\nSerious\t350\t165\t405\t87\t87\t50\r\nNaughty\t250\t170\t285\t72\t37\t200\r\nCozy\t250\t100\t375\t37\t137\t175\r\n\n\n" +
                "Special Training\r\nYou can do this up to 50 times. After the selection is made, quests must be completed 5 times or failed.\r\nCan be reset using 9228・初心の巻物\r\n\r\nSleep\tHealth＋２\tFE25・スヤスヤまくら\r\nLift\tAtk Power＋１\tFA25・グイグイダンベル\r\nAbs\tDef Power＋３\tFF25・ビヨーンベルト\r\nBox\tBravery＋１\tFB25・ポスポスグローブ\r\nSwing\tReflexes＋１\tFC25・ブンブンステッキ\r\nZen\tLuck＋１\tFD25・クンクンアロマ\r\nColor Characteristic\r\nColor is determined by the partnyas behavior and weapons, it affects the treasures found and recommended use slot for guild trasure cats.\r\nColor can be changed using 7227・得意行動の書\n\n" +
                "Partnya Item Pouch\r\nExtra pouch available only when accompanied by a partnya.\r\nAt first, it is 1 slot, but it increases for every PR100, and the max is 5 slots at PR400.\r\nWith assist course active, +5 slots will be added (max. 10 slots).\r\n\r\nQuest Accompaniment\r\nThe following bonuses will occur if your partnya accompanies you on a quest. Numbers change depending on luck.\r\nHunters' Festival: Bonus of up to +15 souls\r\nGreat Voyage Festival: Up to 1.8x CP and score increase *outdated*\r\n\r\nPartnya dispatch [soul collection].\r\nOnce dispatched, you can receive souls from the partner sign at 12:00 noon(JP) once a day. (Need to deposit souls at NPC after collecting)\n\n" +
                "Material\tSouls\r\nGreen Valor Stone\t200 Souls\r\nEvent Assist Tkt\t100 Souls\r\nConfession Stone\t80 Souls\r\nShining Mystery\t70 Souls\r\nHammerhead Tuna\t50 Souls\r\nGuild Tkt\t50 Souls\r\nSpeartuna\t30 Souls\r\nLg Knife Mackarel\t20 Souls\r\nWell Done Fish\t10 Souls\r\nGlutton Tuna\t10 Souls\r\nHoney\t5 Souls\r\nKnife Mackerel\t5 Souls\r\nFelvine\t5 Souls\r\nSushifish\t5 Souls\r\nPartnya Dispatch [Guild]\r\nIf the guilds rank is 2 or higher, you can send it to the treasure cat in the guild house.\r\nWhen dispatching, match the characteristic color, the higher the partnya rank, the better the rewards.\n\n" +
                "Dispatch Partnya [My Tore]\r\nWhen dispatched to My Tore, they will go to each store to help.\r\n・My Tore Combo：ネコ珠の素・青(Cat Pearl・Blue) Added\r\n・My Tore General：ネコ珠の素・赤(Cat Pearl・Red) Added\r\n・My Tore Adventurer：ネコ珠の素・緑,黄,紫,白,黒,茶,灰,橙,金/銀/銅の肉球手形(Cat Pearl・Green, Yellow, Purple, White, Black, Brown, Gray, Silver, Gold/Silver/Brnz Pawprint Token) Added";
        }
    }

    public static string GetGamePoogieInfo
    {
        get
        {
            return "My Tore\r\nFarm Manager\r\nYou will receive a manager at HR51, which manager you get is decided by your responses to their questions.（My Tore Mistress）\r\n※The choices in order from left to right will get you which manager. Y = Yes, N = No.\r\nAnswers→Manager\r\n\u3000YYY\u3000\u3000→Middle\r\n\u3000YYN\u3000\u3000→Youngest\r\n\u3000YNY\u3000\u3000→Eldest\r\n\u3000YNN\u3000\u3000→Youngest\r\n\u3000NYY\u3000\u3000→Random\r\n\u3000NYN\u3000\u3000→Random\r\n\u3000NNY\u3000\u3000→Random\r\n\u3000NNN\u3000\u3000→Eldest\r\n\r\nManagerial change event\r\nAfter repeatedly talking to the sisters you want to change to when all three sisters are in My Tore, you are asked if you want to switch when you go to leave leave.\r\nPoogie Points\r\nYou can recieve poogie points by talking to your manager, alternating between 1 and 2 points everyday. You can hold up to 99.\r\nEven if you forget to claim the points, you may still be able to recover them by talking to her. (Up to 14 points)\r\nAfter HR200, you will recieve 10 points for each 100 levels in HR.\r\n\r\nGive Clothing\r\n\r\nItem\tPoints\r\nCool Blue Costume\t30P\r\nLight Orange Costume\t30P\r\nHealthy Cyan Costume\t30P\r\nGraceful White Costume\t30P\r\nAirou Square\r\nThe amount of sales is determined by the cattiness of the Felynes who come to the square.\n\n" +
                "Poogie Ranch\r\n\r\nExpansion\tPoint Cost\r\nObstacle Course\t30P\r\nClimbing Facility\t30P\r\nRunning Facility\t30P\r\nStudy Facility\t30P\r\nDeparture Facility\t30P\r\nPoogie Extension\r\n\r\nItem\tPoint Cost\r\nAdd a Poogie\t30P(Up to 3)\r\nRank up to G rank\t60P\n\n" +
                "※Clothes obtained with poogie points are only displayed 3 at a time, another being revealed each time you make a purchase.\r\nClothes in yellow are premium outfits and can be used for premium skill cuffs.\n\n" +
                "### Poogie Costumes\r\n\r\n| ID | Poogie Costume |\r\n|----|------|\r\n| 0  |First Costume|\r\n| 1  |Kirin Costume  |\r\n| 2  | Smart Costume   |\r\n| 3  | Cheerful Costume   |\r\n| 4  | Wild Costume  |\r\n| 5  |Hypnoc Costume   |\r\n| 6  |  Volganos Costume  |\r\n| 7  | Espinas Costume  |\r\n| 8  | Comrade Costume |\r\n| 9  | Sporty Costume  |\r\n|10  | Lively Costume   |\r\n|11  | Akura Costume |\r\n|12  | Azul Costume   |\r\n|13  |Cool Costume     |\r\n|14  |  Fine Costume   |\r\n|15  | Beru Costume    |\r\n|16  |  Gospel Costume    |\r\n|17  | Winning Costume    |\r\n|18  | Miner Costume |\r\n|19  | Paria Costume  |\r\n|20  | Magisa Costume     |\r\n|21  | Mischievous Costume |\r\n|22  | Gatherer Costume |\r\n|23  |Kamu Costume |\r\n|24  |  Nono Costume  |\r\n|25  | Tasty Costume   |\r\n|26  | Fishing Costume    |\r\n|27  | Legendary Costume  |\r\n|28  |UNK  |\r\n|29  |  Rainbow Costume  |\r\n|30  | Hope Costume   |\r\n|31  | Pokara Costume   |\n\n" +
                "Manager\r\nGifts, Etc.\r\nWhen an item is requested, favor-ability will not increase if it is taken from storage. (In this case, don't give her the item)\r\n「Flower Extract」will be given the next day after the maximum number of seeds have been planted, after which the caretaker will have a conversation with you, saying \"Here is the flower extract.\"\r\nWhen giving an item as a gift, the second item is the key. The first doesn't affect anything.\r\nThe manager responds according to our choices.\n\n" +
                "Special Requests\r\nRequests other than the initial offerings will increase by receiving gifts from the Maitre manager.\r\n\r\nName\tReward\tState\tTarget\r\nBoiled Silver Egg\t3\tInitial\tRathian\r\nHot and Cold Drink\t3\tInitial\tBlangonga\r\nHerbal Medicine G\t2\tInitial\tBasarios\r\nBoiled Golden Egg\t2\t\r\nTigrex\r\nDisposable Earplug\t3\t\r\nLavasioth\r\nHGE Earplugs\t3\t\r\nRajang\r\nFast Whetstone\t3\t\r\nPlesioth\r\nG Whetstone\t5\t\r\nRathalos\r\nRock-steady Fruit\t2\t\r\nYian Garuga\n\n" +
                "My Tore Poogi\r\nBody color/size changes when fed.\r\n\r\nmeals\tEffect\r\nStubborn Shrinking Stew(頑固な縮小シチュー)\tSmaller body size. (up to 0.75 times normal)\r\nMerry Expansion Bowl(陽気な拡張ボーロ)\tIncreased body length. (up to 1.25 times normal)\r\nAll Else\tChanges the body color\r\nOn rare occasions, managers may become ill. Note that when sick, the conversation event to receive missed points does not occur until the manager is healed.\r\n\r\nManager\tMats to cure\r\nEldest\tSleep Herbx3、Ice Crystalx5\r\nMiddle\tSleepyfishx4、Cold Meat Gx3\r\nYoungest\tNutrientsx2、Cool Drinkx1\r\nIf you maximize your favorability, you can get the title of \"I love Poogie\" with \"street name\" titles.\r\n\r\nAbility\r\n・LV：Increases the number of appearances during each quest. Makes it easier to bring items\r\n\u3000(Intelligence + Exercise power + Perseverance) / 5 = Poogie LV (Rounded Down)\r\n・Intelligence: Brings better items. bring items for the situation\r\n・Exercise power: Poogi appears at the right time, such as when your health is low.\r\n・Perseverance: Appears at shorter intervals\r\n\r\nTreasure\r\nCan bring back items you get from mining, catching, gathering, fishing and carving at a (5%?) chance.\r\nThe higher the LV, the easier it is to bring back.\n\n" +
                "Items to be carried(If Intelligence is high, the number to bring may also increase?)\r\n\r\nItem\tNo.\tIntelligence\r\nHerb\t1\tlow\r\nFirst-Aid Med\t1-2\tlow\r\nMega Potion\t1-2\tlow\r\nEZ Max Potion\t1-2\tlow\r\nAncient Potion\t1\tMed\r\nAntidote\t1-2\tlow\r\nHerbal Medicine\t1-2\tlow\r\nRation\t1-2\tlow\r\nGourmet Steak\t1-2\tlow\r\nPower Pill\t1-2\tlow\r\nDemondrug\t1-2\tlow\r\nPower Juice\t1-2\tlow\r\nArmourskin\t1-2\tlow\r\nArmour Pill\t1-2\tlow\r\nThrowing Knife\t1-4\tlow\r\nEZ Barrel Bomb L\t1-2\tlow\r\nSplyLgBarrelBombG2\t1\tHigh\r\nHot Drink\t1-2\tlow\r\nCool Drink\t1-2\tlow\r\nPsychoserum\t1-2\tlow\r\nPaintball\t1-2\tlow\r\nMega Pickaxe\t1-2\tlow\r\nMega Bugnet\t1-2\tlow\r\nFarcaster\t1\tlow\r\nMini-Whetstone\t1-2\tlow\r\nWhetstone\t1-2\tMed\r\nHi-Speed Whetstone\t1\tHigh\r\nNormal S LV2\t10-20\tlow\r\nPierce S LV1\t10-20\tlow\r\nPellet S Lv1\t10-20\tlow\r\nPoison Coating\t5\tlow\r\nPara Coating\t5\tlow\r\nG-rank Poogie\r\nA G-class skill cuff can be equipped at GR 50.\r\nItems held for use can be given at GR 350.\r\nCarried items are: Potions, Mega Potions, Antidotes, Energy Drinks, Power Juice and Max Potions.\r\n（Poogie uses these directly from your item box.）\n\n" +
                "My Tore Felynes\r\nLV\r\nRestrictions on use of stores depending on cat LV (currently at My Tore Adventurer's adventure destination).\r\n(Affection + Cattiness)/5+1=Cat LV (round off fractions)\r\n\r\nAttributes(MAX255)\r\nCats have Affection (affects the items brought back in the adventure) and cattiness (sales of the store).\r\nThe two Attributes increase differently depending on the type of shop.\r\n\r\nShop\tHow it affects lvls\r\nMy Tore Combine\tIncreased cattiness (rarely decreases affection)\r\nMy Tore General Store\tIncreased cattiness (rarely decreases affection)\r\nMy Tore Bar\tRaises an average amount\r\nMy Tore Clothing\tCattiness increases slightly(rarely decreases affection)\r\nMy Tore Adventurer\tIncreases affection (rarely decreases cattiness)\r\nThe type of cat depends on the number of shops, and the number of cats that come to the square at one time depends on the level of the bar.\n\n" +
                "My Tore General Store\r\nItem Name\tCost\tNotes\r\nHealth Drink Mix\t800z\tLV1\r\nHerb\t20z\tLV2\r\nPotion\t66z\tLV2\r\nNutrients\t760z\tLV1\r\nAntidote\t60z\tLV1\r\nImmunizer\t923z\tLV3\r\nDemondrug\t668z\tLV3\r\nMega Demondrug\t2831ｚ\tLV3SR\r\nArmourskin\t578z\tLV3\r\nMega Armourskin\t2696ｚ\tLV3SR\r\nCool Drink\t300z\tLV1\r\nHot Drink\t250z\tLV1\r\nHot & Cold Drink\t450z\tLV2\r\nPsychoserum\t300z\tLV1\r\nHerbal Medicine\t250z\tLV3SR\r\nTranquilizer\t150z\tLV1\r\nPoisoned Meat\t188z\tLV1\r\nTainted Meat\t300z\tLV1\r\nDrugged Meat\t315z\tLV1\r\nHot Meat\t250z\tLV3\r\nHot Meat G\t300z\tLV3SR\r\nCold Meat\t300z\tLV3\r\nCold Meat G\t380z\tLV3SR\r\nAntidote Meat\t200z\tLV3\r\nAntidote Meat G\t280z\tLV3SR\r\nBoomerang\t150z\tLV1\r\nPaintball\t100z\tLV3\r\nSmoke Bomb\t437z\tLV3\r\nPoison Smoke Bomb\t600z\tLV3\r\nFarcaster\t300z\tLV1\r\nDeodorant\t80z\tLV3\r\nTrap Tool\t200z\tLV1\r\nSmall Barrel\t80z\tLV1\r\nLarge Barrel\t210z\tLV1\r\nSm Barrel Bomb\t156z\tLV1\r\nLg Barrel Bomb\t518z\tLV1\r\nHuskberry\t2z\tLV1\r\nNormal S LV2\t2z\tLV1\r\nNormal S LV3\t4z\tLV1\r\nPierce S LV1\t10z\tLV1\r\nPierce S LV2\t23z\tLV1\r\nPierce S LV3\t36z\tLV1\r\nPellet S Lv1\t7z\tLV1\r\nPellet S Lv2\t11z\tLV1\r\nPellet S Lv3\t15z\tLV1\r\nCrag S Lv1\t29z\tLV1\r\nCrag S Lv2\t46z\tLV1\r\nCrag S Lv3\t49z\tLV3SR\r\nClust S Lv1\t27z\tLV1\r\nClust S Lv2\t44z\tLV1\r\nClust S Lv3\t69z\tLV1\r\nFlaming S\t20z\tLV1\r\nWater S\t20z\tLV1\r\nThunder S\t20z\tLV1\r\nFreeze S\t20z\tLV1\r\nRecov S Lv1\t6z\tLV1\r\nRecov S Lv2\t15z\tLV1\r\nPoison S Lv1\t12z\tLV1\r\nPoison S Lv2\t17z\tLV3SR\r\nPara S Lv1\t18z\tLV1\r\nPara S Lv2\t38z\tLV3SR\r\nSleep S Lv1\t12z\tLV1\r\nSleep S Lv2\t25z\tLV3SR\r\nPaint S\t14z\tLV3\r\nEmpty Bottle\t3z\tLV1\r\nPower Coating\t16z\tLV1\r\nPoison Coating\t12z\tLV1\r\nPara Coating\t20z\tLV1\r\nSleep Coating\t12z\tLV1\r\nCricket\t10z\tLV1\r\nWorm\t20z\tLV1\r\nIron Pickaxe\t160z\tLV1\r\nMega Pickaxe\t240z\tLV2\r\nBugnet\t80z\tLV1\r\nMega Bugnet\t120z\tLV2\r\nWhetstone\t80z\tLV1\r\nBinoculars\t50z\tLV3\r\nFlute\t480z\tLV1\r\nBlue Mushroom\t24z\tLV2\r\nNitroshroom\t60z\tLV2\r\nParashroom\t150z\tLV2\r\nToadstool\t75z\tLV2\r\nAntidote Herb\t20z\tLV1\r\nFire Herb\t44z\tLV1\r\nIvy\t75z\tLV1\r\nSap Plant\t24z\tLV1\r\nFelvine\t14z\tLV1\r\nHot Pepper\t44z\tLV1\r\nPower Seed\t280z\tLV2\r\nArmour Seed\t220z\tLV2\r\nDragon Seed\t780z\tLV3\r\nNeedleberry\t22z\tLV1\r\nBomberry\t120z\tLV1\r\nPaintberry\t60z\tLV2\r\nMocha Pot\t300z\tLV1\r\nVelociprey Fang\t82z\tLV2\r\nIoprey Fang\t50z\tLV2\r\nGenprey Fang\t80z\tLV2\r\nGreen Onion\t50z\tLV3\r\nMixed Beans\t150z\tLV3\r\nStubborn Bread\t350z\tLV3\r\nBeans\t880z\tLV3\r\nFish Notes\t30z\tLV3\r\nRice Weevil\t30z\tLV3\r\nPower Lard\t400z\tLV3\r\nLongevity Jam\t980z\tLV3\r\nDry Margarine\t100z\tLV3\r\nMaengwoo Butter\t400z\tLV3\r\nRoyal Cheese\t880z\tLV3\r\nHopi Alcohol\t300z\tLV3\r\nBrass Wine\t600z\tLV3\r\nRed Oil\t100z\tLV3\r\nRare Onion\t250z\tLV3GR600\r\nGodbug\t420z\tLV3GR600\r\nGolden Alcohol\t1280z\tLV3GR600\r\nGold Extract\t1620z\tLV3GR600\r\nPredator Honey\t1940z\tLV3GR600\r\nCE27・ネコ珠の素・赤\t1500z\tパートニャー派遣\r\nEA29・ネコ珠の素・桜\t1500z\tパートニャー派遣\r\nF92D・ネコ珠の素・梅\t1500z\tパートニャー派遣\n\n" +
                "My Tore Combo List\r\nItems for guild special requests can only be created if they are given as a gift from the manager.\n\n" +
                "My Tore Adventurer\r\n\u3000※As Affection rises, the number of frames and the number of takeaways increase (maximum + 1 more takeaway)Needs Verified\n\n" +
                "My Tore Clothing Store\r\nPersonality and Clothing\r\nActivated in the order in which the conditions are met, starting with skill 1\r\n\r\nPersonality\tActivated Skill\r\nShallow and Wide\tActivate up to 4 skills [small] on clothing.\r\nShallow and Narrow\tActivate up to 3 skills [small] on clothing.\r\nDeep and Narrow\tActivate up to two skills [Small] or [Large] on the clothing.\r\nDeep and Wide\tActivate up to 3 skills [Small] or [Large] on the clothing.\r\nOther\tActivate only one skill [Small] or [Large] on the clothing.\r\nSkill\r\n\r\nSkill Name\tEffect\r\n～～王(King)\tA certain probability of bringing back more of the relevant materials.\r\n～～嫌(Reluctant)\tLess chance to bring back the relevant material.\r\n～～爆発(撃沈) Explosion (sinking)\tWhen customers, Felynes, are looking for ~~, there is a probability that it will be added (subtracted) to sales\r\n受け上手(下手) Good (bad) at receiving\tFaster (slower) responses to instructions given to the shopkeeper\r\n怠け上手 Lazy\tMay stop in the middle of the instructions given to the store keeper.\r\n戻り上手(下手) Good (bad) at returning\tAdventurer cat's return time may be shortened (extended)\n\n" +
                "My Tore Bar\r\nThe number of cats coming to My Tore increases.\r\nYou can hear information such as skills related to cat, open-air baths, and personalities of cats in small talk.\r\n\r\nA \"hospitality\" section is added at Lv2.\r\nIf \"Hospitality Settings\" is ON, Other Players will receive an item 10 times a day when they select \"Hospitality\" at the bar.\r\nThe items you will receive are those on the guild special request list of the bar owner.\r\n\r\nItems you can get\r\n・Boiled Golden Egg\r\n・Boiled Silver Egg\r\n・Hot & Cold Drink\r\n・Herbal Medicine G\r\n・Disposable Earplugs\r\n・HGE Earplugs\r\n・Hi-Speed Whetstone\r\n・G Whetstone\r\n・Anti-Flinch Fruit\r\n\r\nAt Lv3, you can get \"Regulars' Gifts\" 3 times a day when you select \"Talk\" at your own bar.\r\n(※You have to talk to them several times. Once you get one again, you need to go on a quest.)\r\n・Perk Token\r\n・Poogie Garden Tkt\r\n・Best Recipe\r\n・Essential Drink\r\n\r\nYou can ask a hunter to introduce you to a Felyne that he or she has hired, and you can hire him or her for your own My Tore\r\nHowever, cat LV, etc. are returned to their initial values.\r\nYou can't introduce a cat that was introduced to you and hired for your bar.";
        }
    }

    public static string GetGameDamageInfo
    {
        get
        {
            return "Calculation formula: Basic weapon multiplier = attack power ÷ (Great Sword/Long Sword: 4.8, SnS/dual sword: 1.4, hammer/hunting horn: 5.2, lance/gunlance: 2.3, bowgun/bow: 1.2)\r\nWeapon Magnification = [[[ ([Basic Weapon Magnification x Weapon Technique] + Powercharm + Powertalon + Meal/Demon Medicine + Demon Flute/Strength Class + Attack Power Up Skill + SR Correction + Sigil + SP Correction)\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000×Melody Effect]×Real Power]×Secret] + Bonds + Tenran/Supremacy/G-class Armor Correction\r\n\u3000\u3000\u3000\u3000Attribute value = [(weapon attribute value + sigil) x ○ attribute enhancement x attribute attack x supremacy armor correction] x serious drink\r\n\u3000\u3000\u3000\u3000 Abnormal status value = Abnormal weapon status value x Abnormal status enhancement x Supreme armor correction\r\nAffinity rate = (weapon criticality + criticality UP skill + SR correction + sigil)  *if positive up to this point + sharpness bonus\r\n+ SP Correction + HC Gun Correction + Fatal Fury + Serious + Critical Whetstone\r\n①Slash/Strike\r\nPhysical Damage = Weapon Magnification x Motion Value x Sharpness Magnification x Cutting Method x Weapon Modification x Flesh Quality x Critical Hit Modification\r\nAttribute damage = attribute value x sharpness multiplier x meat quality x attribute melody\r\n② Shots\r\n\u3000Physical Damage = Weapon Magnification x Ammo x Distance Modification x Power UP Skill x Weapon Modification x Flesh Quality x Critical Hit Modification\r\nAttribute damage = attribute value x flesh quality x attribute melody\r\n\u3000\u3000\u3000\u3000\u3000\u3000The power of the bow will also be corrected by charging\r\nTotal\r\nFinal damage = (physical damage + elemental damage) x total defense rate x anger multiplier x HC correction x HC anger correction\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000※A minimum of 1 physical damage is included\r\nNotation Gray: Bounced, Green: small hitstop, Blue: large hitstop";
        }
    }

    public string GetWeaponIcon
    {
        get
        {
            switch (GetWeaponNameFromType(WeaponType()))
            {
                default:
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/sns.png";
                case "Sword and Shield":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/sns.png";
                case "Dual Swords":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/ds.png";
                case "Great Sword":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/gs.png";
                case "Long Sword":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/ls.png";
                case "Hammer":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/hammer.png";
                case "Hunting Horn":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/hh.png";
                case "Lance":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/lance.png";
                case "Gunlance":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/gl.png";
                case "Tonfa":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/tonfa.png";
                case "Switch Axe F":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/saf.png";
                case "Magnet Spike":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/ms.png";
                case "Heavy Bowgun":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/hbg.png";
                case "Light Bowgun":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/lbg.png";
                case "Bow":
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/weapon/bow.png";
            }
        }
    }

    public string GetMetadataForImage
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            var metadata = string.Empty;

            if (!s.EnableMetadataExport)
            {
                metadata = string.Empty;
            }
            else
            {
                string guildName;
                string hunterName;
                var DateAndTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

                if (s.GuildName.Length >= 1)
                {
                    guildName = s.GuildName;
                }
                else
                {
                    guildName = "Guild Name";
                }

                if (s.HunterName.Length >= 1)
                {
                    hunterName = s.HunterName;
                }
                else
                {
                    hunterName = "Hunter Name";
                }

                metadata = " | " + hunterName + " | " + guildName + " | " + DateAndTime;
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}({1}){2}", GetWeaponClass(), GetGender(), metadata);
        }
    }

    public string GetWeaponForGuildCard
    {
        get
        {
            var className = GetWeaponClass();
            var lv = GetWeaponNameFromType(WeaponType()).Contains("Bowgun") ? GetGRWeaponLevel(GRWeaponLvBowguns()) : GetGRWeaponLevel(GRWeaponLv());

            if (className == "Blademaster")
            {
                WeaponBlademaster.IDName.TryGetValue(BlademasterWeaponID(), out var wepname);
                var address = BlademasterWeaponID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();  // gives you hex 4 digit "007B"

                return string.Format(CultureInfo.InvariantCulture, "{0}{1} ({2})", wepname, lv, address);

            }
            else if (className == "Gunner")
            {
                WeaponGunner.IDName.TryGetValue(GunnerWeaponID(), out var wepname);
                var address = GunnerWeaponID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();  // gives you hex 4 digit "007B"
                return string.Format(CultureInfo.InvariantCulture, "{0}{1} ({2})", wepname, lv, address);
            }
            else
            {
                return "None";
            }
        }
    }

    public string GetWeaponDecos
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", GetDecoName(WeaponDeco1ID(), 1, true), GetDecoName(WeaponDeco2ID(), 2, true), GetDecoName(WeaponDeco3ID(), 3, true));
        }
    }

    public string GetArmorHeadNameForGuildCard
    {
        get
        {
            ArmorHead.IDName.TryGetValue(ArmorHeadID(), out var piecename);
            var address = ArmorHeadID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", piecename, address);
        }
    }

    public string GetArmorChestNameForGuildCard
    {
        get
        {
            ArmorChest.IDName.TryGetValue(ArmorChestID(), out var piecename);
            var address = ArmorChestID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", piecename, address);
        }
    }

    public string GetArmorArmNameForGuildCard
    {
        get
        {
            ArmorArms.IDName.TryGetValue(ArmorArmsID(), out var piecename);
            var address = ArmorArmsID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", piecename, address);
        }
    }

    public string GetArmorWaistNameForGuildCard
    {
        get
        {
            ArmorWaist.IDName.TryGetValue(ArmorWaistID(), out var piecename);
            var address = ArmorWaistID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", piecename, address);
        }
    }

    public string GetArmorLegNameForGuildCard
    {
        get
        {
            ArmorLegs.IDName.TryGetValue(ArmorLegsID(), out var piecename);
            var address = ArmorLegsID().ToString("X4", CultureInfo.InvariantCulture).ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", piecename, address);
        }
    }

    public string GetArmorHeadDecos
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", GetItemName(ArmorHeadDeco1ID()), GetItemName(ArmorHeadDeco2ID()), GetItemName(ArmorHeadDeco3ID()));
        }
    }

    public string GetArmorChestDecos
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", GetItemName(ArmorChestDeco1ID()), GetItemName(ArmorChestDeco2ID()), GetItemName(ArmorChestDeco3ID()));
        }
    }

    public string GetArmorArmDecos
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", GetItemName(ArmorArmsDeco1ID()), GetItemName(ArmorArmsDeco2ID()), GetItemName(ArmorArmsDeco3ID()));
        }
    }

    public string GetArmorWaistDecos
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", GetItemName(ArmorWaistDeco1ID()), GetItemName(ArmorWaistDeco2ID()), GetItemName(ArmorWaistDeco3ID()));
        }
    }

    public string GetArmorLegDecos
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", GetItemName(ArmorLegsDeco1ID()), GetItemName(ArmorLegsDeco2ID()), GetItemName(ArmorLegsDeco3ID()));
        }
    }

    public string GetWeaponForImage
    {
        get
        {
            var className = GetWeaponClass();
            var lv = GetWeaponNameFromType(WeaponType()).Contains("Bowgun") ? GetGRWeaponLevel(GRWeaponLvBowguns()) : GetGRWeaponLevel(GRWeaponLv());

            if (className == "Blademaster")
            {
                WeaponBlademaster.IDName.TryGetValue(BlademasterWeaponID(), out var wepname);
                return string.Format(CultureInfo.InvariantCulture, "{0}{1}", wepname, lv);

            }
            else if (className == "Gunner")
            {
                WeaponGunner.IDName.TryGetValue(GunnerWeaponID(), out var wepname);
                return string.Format(CultureInfo.InvariantCulture, "{0}{1}", wepname, lv);
            }
            else
            {
                return "None";
            }
        }
    }

    public string GetWeaponDecosForImage
    {
        get
        {
            var className = GetWeaponClass();

            if (className == "Blademaster" || className == "Gunner")
            {
                return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2}", GetDecoName(WeaponDeco1ID(), 1), GetDecoName(WeaponDeco2ID(), 2), GetDecoName(WeaponDeco3ID(), 3));
            }
            else
            {
                return "None";
            }
        }
    }

    public string GetArmorHeadNameForImage
    {
        get
        {
            ArmorHead.IDName.TryGetValue(ArmorHeadID(), out var piecename);
            return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2} | {3}", piecename, GetDecoName(ArmorHeadDeco1ID(), 0, true), GetDecoName(ArmorHeadDeco2ID(), 0, true), GetDecoName(ArmorHeadDeco3ID(), 0, true));
        }
    }

    public string GetArmorChestNameForImage
    {
        get
        {
            ArmorChest.IDName.TryGetValue(ArmorChestID(), out var piecename);
            return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2} | {3}", piecename, GetDecoName(ArmorChestDeco1ID(), 0, true), GetDecoName(ArmorChestDeco2ID(), 0, true), GetDecoName(ArmorChestDeco3ID(), 0, true));
        }
    }

    public string GetArmorArmNameForImage
    {
        get
        {
            ArmorArms.IDName.TryGetValue(ArmorArmsID(), out var piecename);
            return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2} | {3}", piecename, GetDecoName(ArmorArmsDeco1ID(), 0, true), GetDecoName(ArmorArmsDeco2ID(), 0, true), GetDecoName(ArmorArmsDeco3ID(), 0, true));
        }
    }

    public string GetArmorWaistNameForImage
    {
        get
        {
            ArmorWaist.IDName.TryGetValue(ArmorWaistID(), out var piecename);
            return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2} | {3}", piecename, GetDecoName(ArmorWaistDeco1ID(), 0, true), GetDecoName(ArmorWaistDeco2ID(), 0, true), GetDecoName(ArmorWaistDeco3ID(), 0, true));
        }
    }

    public string GetArmorLegNameForImage
    {
        get
        {
            ArmorLegs.IDName.TryGetValue(ArmorLegsID(), out var piecename);
            return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2} | {3}", piecename, GetDecoName(ArmorLegsDeco1ID(), 0, true), GetDecoName(ArmorLegsDeco2ID(), 0, true), GetDecoName(ArmorLegsDeco3ID(), 0, true));
        }
    }

    public string GetAtkDefForImage
    {
        get
        {
            return string.Format(CultureInfo.InvariantCulture, "Bloat Attack: {0} | Total Defense: {1}", BloatedWeaponAttack().ToString(), TotalDefense().ToString());
        }
    }

    public string GetStyleRankForImage
    {
        get
        {
            var style = WeaponStyle() switch
            {
                0 => "Earth Style",
                1 => "Heaven Style",
                2 => "Storm Style",
                3 => "Extreme Style",
                _ => "Earth Style"
            };

            return string.Format(CultureInfo.InvariantCulture, "{0} | {1}", style, GetGSRSkills);
        }
    }

    public string GetZenithSkillsForImage
    {
        get
        {
            var zenithSkills = GetZenithSkills;
            zenithSkills = zenithSkills.Replace("\n", ", ");
            return zenithSkills;
        }
    }

    public string GetAutomaticSkillsForImage
    {
        get
        {
            var automaticSkills = GetAutomaticSkills;
            automaticSkills = automaticSkills.Replace("\n", ", ");
            return automaticSkills;
        }
    }

    public string GetActiveSkillsForImage
    {
        get
        {
            var activeSkills = GetArmorSkills;
            activeSkills = activeSkills.Replace("\n", ", ");
            return activeSkills;
        }
    }

    public string GetDivaSkillForImage
    {
        get
        {
            SkillDiva.IDName.TryGetValue(DivaSkill(), out var divaskillaname);
            return divaskillaname + string.Empty;
        }

    }

    // GetArmorSkill(GuildFoodSkill()), GetGSRSkills, GetItemPouch, GetAmmoPouch, GetItemName(PoogieItemUseID()), GetRoadDureSkills
    public string GetGuildFoodForImage
    {
        get
        {
            return GetArmorSkill(GuildFoodSkill());
        }
    }

    public string GetPoogieItemForImage
    {
        get
        {
            return GetItemName(PoogieItemUseID());
        }
    }

    public string GetAlternateMonsterImage(int id)
    {
        switch (id)
        {
            default:
                return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png";
            case 2:
                if (RankBand() == 53)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/conquest_fatalis.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/fatalis.png";
                }

            case 11:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_rathalos.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/rathalos.png";
                }

            case 15:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_khezu.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/khezu.png";
                }

            case 17:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_gravios.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gravios.png";
                }

            case 21:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_plesioth.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/plesioth.png";
                }

            case 36:
                if (RankBand() == 53)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/conquest_crimson_fatalis.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/crimson_fatalis.png";
                }

            case 48:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_daimyo_hermitaur.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/daimyo_hermitaur.png";
                }

            case 51:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_blangonga.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/blangonga.png";
                }

            case 53:
                if (RankBand() == 56 || RankBand() == 57)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/twinhead_rajang.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/rajang.png";
                }

            case 65:
                if (RankBand() == 32)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/supremacy_teostra.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/teostra.png";
                }

            case 71:
                if (RankBand() == 53)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/road_white_fatalis.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/white_fatalis.png";
                }

            case 74:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_hypnoc.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/hypnoc.png";
                }

            case 76:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_tigrex.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/tigrex.png";
                }

            case 80:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_espinas.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/espinas.png";
                }

            case 83:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_akura_vashimu.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/akura_vashimu.png";
                }

            case 89:
                if (RankBand() == 54)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/thirsty_pariapuria.png";
                }
                else if (RankBand() == 32)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/supremacy_pariapuria.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/pariapuria.png";
                }

            case 95:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_doragyurosu.gif";
                }
                else if (RankBand() == 32)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/supremacy_doragyurosu.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/doragyurosu.png";
                }

            case 99:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_rukodiora.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/rukodiora.png";
                }

            case 100:
                if (RankBand() == 70 || RankBand() == 54)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shiten_unknown.png";
                }
                else if (RankBand() == 46)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/supremacy_unknown.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/unknown.png";
                }

            case 103:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_taikun_zamuza.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/taikun_zamuza.png";
                }

            case 106:
                if (RankBand() == 32)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/supremacy_odibatorasu.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/odibatorasu.png";
                }

            case 107:
                if (RankBand() == 54 || RankBand() == 55)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shiten_disufiroa.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/disufiroa.png";
                }

            case 109:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_anorupatisu.gif";
                }
                else
                {

                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/anorupatisu.png";
                }

            case 110:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_hyujikiki.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/hyujikiki.png";
                }

            case 111:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_midogaron.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/midogaron.png";
                }

            case 112:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_giaorugu.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/giaorugu.png";
                }

            case 113:
                if (RankBand() == 55)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shifting_mi_ru.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/mi_ru.png";
                }

            case 116:
                if (RankBand() == 53)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/conquest_shantien.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shantien.png";
                }

            case 121:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_baruragaru.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/baruragaru.png";
                }

            case 129:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_inagami.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/inagami.png";
                }

            case 140:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_harudomerugu.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/harudomerugu.png";
                }

            case 141:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_toridcless.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/toridcless.png";
                }

            case 142:
                if (RankBand() >= 64 && RankBand() <= 67)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_gasurabazura.gif";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gasurabazura.png";
                }

            case 146:
                if (RankBand() >= 54 && RankBand() <= 55)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/howling_zinogre.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zinogre.png";
                }

            case 154:
                if (RankBand() >= 54 && RankBand() <= 55)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/ruling_guanzorumu.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/guanzorumu.png";
                }

            case 155:
                if (RankBand() == 55)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/golden_deviljho.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/starving_deviljho.png";
                }

            case 166:
                if (RankBand() >= 54 && RankBand() <= 55)
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/burning_freezing_elzelion.png";
                }
                else
                {
                    return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/elzelion.png";
                }
        }
    }



    public static string GetGuildCardBackground
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");

            var optionChosen = s.GuildCardBackground.ToLowerInvariant();
            optionChosen = optionChosen.Replace(" ", "_");

            return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/guild_card/" + optionChosen + ".png";
        }
    }

    public static string GetCurrentFeriasVersion
    {
        get
        {
            var s = (Settings)Application.Current.TryFindResource("Settings");
            return s.FeriasVersionLink;
        }
    }

    public ObservableCollection<ObservablePoint> attackBuffCollection = new();

    public ObservableCollection<ObservablePoint> damagePerSecondCollection = new();

    public ObservableCollection<ObservablePoint> actionsPerMinuteCollection = new();

    public ObservableCollection<ObservablePoint> hitsPerSecondCollection = new();

    private ObservableCollection<long> weaponUsageSwordAndShieldEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwordAndShieldHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwordAndShieldStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwordAndShieldExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageDualSwordsEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageDualSwordsHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageDualSwordsStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageDualSwordsExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageGreatSwordEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageGreatSwordHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageGreatSwordStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageGreatSwordExtremeStyleCollection = new();


    private ObservableCollection<long> weaponUsageLongSwordEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageLongSwordHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageLongSwordStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageLongSwordExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageHammerEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageHammerHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageHammerStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageHammerExtremeStyleCollection = new();


    private ObservableCollection<long> weaponUsageHuntingHornEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageHuntingHornHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageHuntingHornStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageHuntingHornExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageLanceEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageLanceHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageLanceStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageLanceExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageGunlanceEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageGunlanceHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageGunlanceStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageGunlanceExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageTonfaEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageTonfaHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageTonfaStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageTonfaExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwitchAxeFEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwitchAxeFHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwitchAxeFStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageSwitchAxeFExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageLightBowgunEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageLightBowgunHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageLightBowgunStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageLightBowgunExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageHeavyBowgunEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageHeavyBowgunHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageHeavyBowgunStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageHeavyBowgunExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageBowEarthStyleCollection = new();

    private ObservableCollection<long> weaponUsageBowHeavenStyleCollection = new();

    private ObservableCollection<long> weaponUsageBowStormStyleCollection = new();

    private ObservableCollection<long> weaponUsageBowExtremeStyleCollection = new();

    private ObservableCollection<long> weaponUsageMagnetSpikeExtremeStyleCollection = new();

    public ObservableCollection<long> weaponUsageEarthStyle = new();

    public ObservableCollection<long> weaponUsageHeavenStyle = new();

    public ObservableCollection<long> weaponUsageStormStyle = new();

    public ObservableCollection<long> weaponUsageExtremeStyle = new();

    public object attackBuffSync { get; } = new();

    public object damagePerSecondSync { get; } = new();

    public object actionsPerMinuteSync { get; } = new();

    public object hitsPerSecondSync { get; } = new();

    public object weaponUsageSync { get; set; } = new();

    public List<ISeries> attackBuffSeries { get; set; } = new();

    public List<ISeries> damagePerSecondSeries { get; set; } = new();

    public List<ISeries> actionsPerMinuteSeries { get; set; } = new();

    public List<ISeries> hitsPerSecondSeries { get; set; } = new();

    public List<ISeries> weaponUsageSeries { get; set; } = new();

    public string GetTimeElapsed(double frames)
    {
        var elapsedTime = TimeSpan.FromSeconds(frames / Numbers.FramesPerSecond);
        var elapsedTimeString = elapsedTime.ToString("mm\\:ss", CultureInfo.InvariantCulture);
        return elapsedTimeString;
    }

    public static string StaticGetTimeElapsed(double seconds)
    {
        var elapsedTime = TimeSpan.FromSeconds(seconds);
        var elapsedTimeString = elapsedTime.ToString("mm\\:ss", CultureInfo.InvariantCulture);
        return elapsedTimeString;
    }

    public string GetMinutesSecondsMillisecondsFromFrames(long frames)
    {
        var elapsedTime = TimeSpan.FromSeconds((double)frames / Numbers.FramesPerSecond);
        var elapsedTimeString = elapsedTime.ToString(TimeFormats.MinutesSecondsMilliseconds, CultureInfo.InvariantCulture);
        return elapsedTimeString;
    }

    static Settings staticSettings = (Settings)Application.Current.TryFindResource("Settings");

    // since the x axis for all of my graphs is the time elapsed in seconds in a quest, i only need 1 definition
    public Axis[] XAxes { get; set; } =
    {
        new Axis
        {
            // Now the Y axis we will display labels as currency
            // LiveCharts provides some common formatters
            // in this case we are using the currency formatter.
            TextSize=12,
            Labeler = (value) => StaticGetTimeElapsed(value),
            NamePaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            LabelsPaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),

            // you could also build your own currency formatter
            // for example:
            // Labeler = (value) => value.ToString("C")

            // But the one that LiveCharts provides creates shorter labels when
            // the amount is in millions or trillions
            }
    };

    public Axis[] attackBuffYAxes { get; set; } =
    {
        new Axis
        {
            NameTextSize= 12,
            TextSize=12,
            NamePadding= new LiveChartsCore.Drawing.Padding(0),
            NamePaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            LabelsPaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            Name="ATK",
        }
    };

    public Axis[] hitsPerSecondYAxes { get; set; } =
{
        new Axis
        {
            NameTextSize= 12,
            TextSize=12,
            NamePadding= new LiveChartsCore.Drawing.Padding(0),
            NamePaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            LabelsPaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            Name="HPS",
        }
    };

    public Axis[] damagePerSecondYAxes { get; set; } =
{
        new Axis
        {
            NameTextSize= 12,
            TextSize=12,
            NamePadding= new LiveChartsCore.Drawing.Padding(0),
            NamePaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            LabelsPaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            Name="DPS",
        }
    };

    public Axis[] actionsPerMinuteYAxes { get; set; } =
{
        new Axis
        {
            NameTextSize= 12,
            TextSize=12,
            NamePadding= new LiveChartsCore.Drawing.Padding(0),
            NamePaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            LabelsPaint = new SolidColorPaint(new SKColor(StaticHexColorToDecimal("#a6adc8"))),
            Name="APM",
        }
    };

    /// <summary>
    /// Gets the objective name from identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetObjectiveNameFromID(int id, bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText)
        {
            return string.Empty;
        }

        // TODO dictionary
        return id switch
        {
            0 => "Nothing ",
            1 => "Hunt ",
            257 => "Capture ",
            513 => "Slay ",
            32772 => "Repel ",
            98308 => "Slay or Repel ",
            262144 => "Slay All ",
            131072 => "Slay Total ",
            2 => "Deliver ",
            16388 => "Break Part ",
            4098 => "Deliver Flag ",
            16 => "Esoteric Action ",
            _ => "Nothing ",
        };
    }

    /// <summary>
    /// Gets the objective1 quantity.
    /// </summary>
    /// <returns></returns>
    public string GetObjective1Quantity(bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText)
        {
            return string.Empty;
        }

        if (Objective1Quantity() <= 1)
        {
            return string.Empty;
        }

        // hunt / capture / slay
        else if (ObjectiveType() == 0x1 || ObjectiveType() == 0x101 || ObjectiveType() == 0x201)
        {
            return Objective1Quantity().ToString() + " ";
        }
        else if (ObjectiveType() == 0x8004 || ObjectiveType() == 0x18004)
        {
            return string.Format(CultureInfo.InvariantCulture, "({0} True HP) ", Objective1Quantity() * 100);
        }
        else
        {
            return Objective1Quantity().ToString() + " ";
        }
    }

    /// <summary>
    /// Gets the objective1 current quantity.
    /// </summary>
    /// <returns></returns>
    public string GetObjective1CurrentQuantity(bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText)
        {
            return string.Empty;
        }

        if (ObjectiveType() == 0x0 || ObjectiveType() == 0x02 || ObjectiveType() == 0x1002)
        {
            if (Objective1Quantity() <= 1)
            {
                return string.Empty;
            }
            else
            {
                return Objective1CurrentQuantityItem().ToString() + "/";
            }
        }
        else
        {
            if (Objective1Quantity() <= 1)
            {
                return string.Empty;
            }
            else
            {
                //increases when u hit a dead large monster
                return Objective1CurrentQuantityMonster().ToString() + "/";
            }
        }
    }

    /// <summary>
    /// Gets the rank name from identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetRankNameFromID(int id, bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText)
        {
            return string.Empty;
        }

        return GetRankName(id);
    }

    /// <summary>
    /// Gets the monster icon.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string getMonsterIcon(int id)
    {
        // quest ids:
        // mp road: 23527
        // solo road: 23628
        // 1st district dure: 21731
        // 2nd district dure: 21746
        // 1st district dure sky corridor: 21749
        // 2nd district dure sky corridor: 21750
        // arrogant dure repel: 23648
        // arrogant dure slay: 23649
        // urgent tower: 21751
        // 4th district dure: 21748
        // 3rd district dure: 21747
        // 3rd district dure 2: 21734
        // UNUSED sky corridor: 21730
        // sky corridor prologue: 21729
        if (roadOverride() == false)
        {
            id = RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID();
        }
        else if (CaravanOverride())
        {
            id = CaravanMonster1ID();
        }

        // Duremudira Arena
        if (AreaID() == 398 && (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750))
        {
            id = 132;
        }

        // duremudira
        else if (AreaID() == 398 && (QuestID() == 23648 || QuestID() == 23649))
        {
            id = 167;
        }

        // arrogant duremudira
        return DetermineMonsterImage(id);
    }

    /// <summary>
    /// Gets the star grade.
    /// </summary>
    /// <param name="isLargeImageText">if set to <c>true</c> [is large image text].</param>
    /// <returns></returns>
    public string GetStarGrade(bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText || CaravanOverride())
        {
            return string.Empty;
        }

        if (IsToggeableDifficulty())
        {
            return string.Format(CultureInfo.InvariantCulture, "★{0} ", StarGrades().ToString());
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Determines whether this instance is road.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is road; otherwise, <c>false</c>.
    /// </returns>
    public bool IsRoad()
    {
        if (roadOverride() != null && roadOverride() == false)
        {
            return true;
        }
        else if (roadOverride() != null && roadOverride() == true)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    public int previousMezFesArea = -1;

    public int previousMezFesScore = 0;

    /// <summary>
    /// Determines whether this instance is dure quest.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is dure; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDure()
    {
        if (getDureName() != "None")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether [is toggeable difficulty].
    /// </summary>
    /// <returns>
    ///   <c>true</c> if [is toggeable difficulty]; otherwise, <c>false</c>.
    /// </returns>
    public bool IsToggeableDifficulty()
    {
        if (!IsDure() && !IsRavi() && !IsRoad() && QuestID() != 0)
        {
            switch (RankBand())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 26:
                case 31:
                case 42:
                case 53:
                    return true;
                default:
                    return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is ravi.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is ravi; otherwise, <c>false</c>.
    /// </returns>
    public bool IsRavi()
    {
        if (getRaviName() != "None")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the real name of the monster.
    /// </summary>
    /// <param name="iconName">Name of the icon.</param>
    /// <returns></returns>
    public string GetRealMonsterName(string iconName, bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText)
        {
            return string.Empty;
        }

        // quest ids:
        // mp road: 23527
        // solo road: 23628
        // 1st district dure: 21731
        // 2nd district dure: 21746
        // 1st district dure sky corridor: 21749
        // 2nd district dure sky corridor: 21750
        // arrogant dure repel: 23648
        // arrogant dure slay: 23649
        // urgent tower: 21751
        // 4th district dure: 21748
        // 3rd district dure: 21747
        // 3rd district dure 2: 21734
        // UNUSED sky corridor: 21730
        // sky corridor prologue: 21729
        // https://stackoverflow.com/questions/4315564/capitalizing-words-in-a-string-using-c-sharp
        int id;

        if (roadOverride() == false)
        {
            id = RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID();
        }
        else if (CaravanOverride())
        {
            id = CaravanMonster1ID();
        }
        else
        {
            id = LargeMonster1ID();
        }

        // dure
        if (QuestID() == 21731 || QuestID() == 21746 || QuestID() == 21749 || QuestID() == 21750)
        {
            return "Duremudira";
        }
        else if (QuestID() == 23648 || QuestID() == 23649)
        {
            return "Arrogant Duremudira";
        }

        // ravi
        if (getRaviName() != "None")
        {
            switch (getRaviName())
            {
                case "Raviente":
                    return "Raviente";
                case "Violent Raviente":
                    return "Violent Raviente";
                case "Berserk Raviente Practice":
                    return "Berserk Raviente (Practice)";
                case "Berserk Raviente":
                    return "Berserk Raviente";
                case "Extreme Raviente":
                    return "Extreme Raviente";
                default:
                    return "Raviente";
            }
        }

        return DetermineMonsterName(id);
    }

    /// <summary>
    /// Gets the name of the objective1.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetObjective1Name(int id, bool isLargeImageText = false)
    {
        if (DiscordService.ShowDiscordQuestNames() && !isLargeImageText)
        {
            return string.Empty;
        }

        string? objValue1;
        Item.IDName.TryGetValue(id, out objValue1);  //returns true
        return objValue1 + string.Empty;
    }

    /// <summary>
    /// Gets the name of the area.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetAreaName(int id)
    {
        if (id == 0)
        {
            return "Loading...";
        }

        Location.IDName.TryGetValue(id, out var areaname);
        Location.IDName.TryGetValue(RavienteAreaID(), out var raviareaname);

        switch (getRaviName())
        {
            default:// or None
                return areaname + string.Empty;
            case "Raviente":
            case "Violent Raviente":
                return raviareaname + string.Empty;
            case "Berserk Raviente Practice":
            case "Berserk Raviente":
            case "Extreme Raviente":
                if (QuestID() != 55796 || QuestID() != 55807 || QuestID() != 54751 || QuestID() != 54761 || QuestID() != 55596 || QuestID() != 55607)
                {
                    return areaname + string.Empty;
                }
                else
                {
                    return raviareaname + string.Empty;
                }
        }
    }

    public bool loadedItemsAtQuestStart;

    public int PouchItem1IDAtQuestStart;

    public int PouchItem2IDAtQuestStart;

    public int PouchItem3IDAtQuestStart;

    public int PouchItem4IDAtQuestStart;

    public int PouchItem5IDAtQuestStart;

    public int PouchItem6IDAtQuestStart;

    public int PouchItem7IDAtQuestStart;

    public int PouchItem8IDAtQuestStart;

    public int PouchItem9IDAtQuestStart;

    public int PouchItem10IDAtQuestStart;

    public int PouchItem11IDAtQuestStart;

    public int PouchItem12IDAtQuestStart;

    public int PouchItem13IDAtQuestStart;

    public int PouchItem14IDAtQuestStart;

    public int PouchItem15IDAtQuestStart;

    public int PouchItem16IDAtQuestStart;

    public int PouchItem17IDAtQuestStart;

    public int PouchItem18IDAtQuestStart;

    public int PouchItem19IDAtQuestStart;

    public int PouchItem20IDAtQuestStart;

    public int PouchItem1QuantityAtQuestStart;

    public int PouchItem2QuantityAtQuestStart;

    public int PouchItem3QuantityAtQuestStart;

    public int PouchItem4QuantityAtQuestStart;

    public int PouchItem5QuantityAtQuestStart;

    public int PouchItem6QuantityAtQuestStart;

    public int PouchItem7QuantityAtQuestStart;

    public int PouchItem8QuantityAtQuestStart;

    public int PouchItem9QuantityAtQuestStart;

    public int PouchItem10QuantityAtQuestStart;

    public int PouchItem11QuantityAtQuestStart;

    public int PouchItem12QuantityAtQuestStart;

    public int PouchItem13QuantityAtQuestStart;

    public int PouchItem14QuantityAtQuestStart;

    public int PouchItem15QuantityAtQuestStart;

    public int PouchItem16QuantityAtQuestStart;

    public int PouchItem17QuantityAtQuestStart;

    public int PouchItem18QuantityAtQuestStart;

    public int PouchItem19QuantityAtQuestStart;

    public int PouchItem20QuantityAtQuestStart;

    public int AmmoPouchItem1IDAtQuestStart;

    public int AmmoPouchItem2IDAtQuestStart;

    public int AmmoPouchItem3IDAtQuestStart;

    public int AmmoPouchItem4IDAtQuestStart;

    public int AmmoPouchItem5IDAtQuestStart;

    public int AmmoPouchItem6IDAtQuestStart;

    public int AmmoPouchItem7IDAtQuestStart;

    public int AmmoPouchItem8IDAtQuestStart;

    public int AmmoPouchItem9IDAtQuestStart;

    public int AmmoPouchItem10IDAtQuestStart;

    public int AmmoPouchItem1QuantityAtQuestStart;

    public int AmmoPouchItem2QuantityAtQuestStart;

    public int AmmoPouchItem3QuantityAtQuestStart;

    public int AmmoPouchItem4QuantityAtQuestStart;

    public int AmmoPouchItem5QuantityAtQuestStart;

    public int AmmoPouchItem6QuantityAtQuestStart;

    public int AmmoPouchItem7QuantityAtQuestStart;

    public int AmmoPouchItem8QuantityAtQuestStart;

    public int AmmoPouchItem9QuantityAtQuestStart;

    public int AmmoPouchItem10QuantityAtQuestStart;

    public int PartnyaBagItem1IDAtQuestStart;

    public int PartnyaBagItem2IDAtQuestStart;

    public int PartnyaBagItem3IDAtQuestStart;

    public int PartnyaBagItem4IDAtQuestStart;

    public int PartnyaBagItem5IDAtQuestStart;

    public int PartnyaBagItem6IDAtQuestStart;

    public int PartnyaBagItem7IDAtQuestStart;

    public int PartnyaBagItem8IDAtQuestStart;

    public int PartnyaBagItem9IDAtQuestStart;

    public int PartnyaBagItem10IDAtQuestStart;

    public int PartnyaBagItem1QuantityAtQuestStart;

    public int PartnyaBagItem2QuantityAtQuestStart;

    public int PartnyaBagItem3QuantityAtQuestStart;

    public int PartnyaBagItem4QuantityAtQuestStart;

    public int PartnyaBagItem5QuantityAtQuestStart;

    public int PartnyaBagItem6QuantityAtQuestStart;

    public int PartnyaBagItem7QuantityAtQuestStart;

    public int PartnyaBagItem8QuantityAtQuestStart;

    public int PartnyaBagItem9QuantityAtQuestStart;

    public int PartnyaBagItem10QuantityAtQuestStart;

    public TimeSpan TotalTimeSpent { get; set; }

    public bool questCleared; // quest state 1

    public bool questRewardsGiven; // quest state 129

    public bool ValidateGameFolder()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        var findFiles = new List<string>{
            s.GameFolderPath + @"\dat\mhfdat.bin",
            s.GameFolderPath + @"\dat\mhfinf.bin",
            s.GameFolderPath + @"\dat\mhfemd.bin",
            s.GameFolderPath + @"\dat\mhfsqd.bin",
            s.GameFolderPath + @"\mhfo.dll",
            s.GameFolderPath + @"\mhfo-hd.dll",
            s.GameFolderPath + @"\mhf.exe"
            };

        //check if the folder is named dat and contains dat, emd, and sqd.
        if (s.GameFolderPath == string.Empty || s.GameFolderPath == null)
        {
            LoggerInstance.Warn(CultureInfo.InvariantCulture, "Game folder path not found");
            MessageBox.Show("Game folder path not found. If you do not want to log quests into the database or see this message, disable the Quest Logging option in Quest Logs section, and click the save button.", Messages.WarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            s.EnableQuestLogging = false;
            s.Save();
            return false;
        }

        if (s.DatabaseFilePath == string.Empty || s.DatabaseFilePath == null)
        {
            LoggerInstance.Warn(CultureInfo.InvariantCulture, "Database file path not found");
            MessageBox.Show("Database file path not found. If you do not want to log quests into the database or see this message, disable the Quest Logging option in Quest Logs section, and click the save button.", Messages.WarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            s.EnableQuestLogging = false;
            s.Save();
            return false;
        }

        // Get a list of file names in the game folder
        var gameFolderFiles = Directory.EnumerateFiles(s.GameFolderPath);

        // Get the intersection of the findFiles list and the gameFolderFiles list
        var intersection = findFiles.Intersect(gameFolderFiles);

        // If the intersection is not empty, it means that all the files in the findFiles list are present in the game folder
        if (intersection.Any())
        {
            // All required files are present
            return true;
        }
        else
        {
            // Some required files are missing
            MessageBox.Show("Some required files are missing from the game folder. Please make sure that the game folder contains the following files: "
            + string.Join(", ", findFiles) + "\n" +
            "gameFolderFiles: " + string.Join(", ", gameFolderFiles),
            Messages.WarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            LoggerInstance.Warn(CultureInfo.InvariantCulture, "Missing game files");
            s.EnableQuestLogging = false;
            s.Save();
            return false;
        }
    }

    // all dictionaries get a new entry every 1 second. freezes on quest state 1, resets on quest id = 0.
    // use modulo
    // int for timeint() which is current quest time, second int for current attack buff
    public Dictionary<int, int> attackBuffDictionary = new();

    // the deserealized are used for displays
    public Dictionary<int, int>? attackBuffDictionaryDeserealized;

    // same for this but second is current hit count
    public Dictionary<int, int> hitCountDictionary = new();

    public Dictionary<int, int>? hitCountDictionaryDeserealized;

    // same but the second int is the damage dealt when hitting monster.
    public Dictionary<int, int> damageDealtDictionary = new();

    public Dictionary<int, int>? damageDealtDictionaryDeserealized;

    // then for DPS i can calculate from the above dictionary and only update the
    // DPS value every time you register a new hit (new entry in damageDealtDictionary), which means its accurate but
    // not according to the real-time, but then again, the time that passes in-game
    // is not real-time.
    public double DPS { get; set; }

    public double HitsPerSecond { get; set; }

    public double APM { get; set; }

    public double TotalHitsTakenBlocked
    {
        get
        {
            // Check if the damageDealtDictionary is empty
            if (!hitsTakenBlockedDictionary.Any())
            {
                // If the dictionary is empty, return 0 as the DPS value
                return 0;
            }

            // Sum up the values in the maxValues dictionary to get the total amount
            var totalAmount = hitsTakenBlockedDictionary.Count;
            return totalAmount;
        }
    }

    public string TotalHitsTakenBlockedStats
    {
        get
        {
            var totalHitsTakenBlockedPerSecond = string.Empty;

            if (ShowTotalHitsTakenBlockedPerSecond())
            {
                totalHitsTakenBlockedPerSecond = string.Format(CultureInfo.InvariantCulture, " ({0:0.00}/s)", TotalHitsTakenBlockedPerSecond);
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", TotalHitsTakenBlocked, totalHitsTakenBlockedPerSecond);
        }
    }

    public double TotalHitsTakenBlockedPerSecond { get; set; }

    public double CalculateDPS()
    {
        // Check if the damageDealtDictionary is empty
        if (!damageDealtDictionary.Any())
        {
            // If the dictionary is empty, return 0 as the DPS value
            return 0;
        }

        double damageDealt = damageDealtDictionary.Values.Sum();
        double timeElapsedIn30FPS = TimeDefInt() - TimeInt();

        // Calculate and return the DPS
        return damageDealt / (timeElapsedIn30FPS / Numbers.FramesPerSecond);
    }

    // TODO: gamepad
    public double CalculateAPM()
    {
        double totalInputs = gamepadInputDictionary.Count + keystrokesDictionary.Count + mouseInputDictionary.Count;
        double timeElapsedIn30FPS = TimeDefInt() - TimeInt();

        return totalInputs / (timeElapsedIn30FPS / 1800);
    }

    public double CalculateTotalHitsTakenBlockedPerSecond()
    {
        double timeElapsedIn30FPS = TimeDefInt() - TimeInt();

        // Calculate and return the DPS
        return TotalHitsTakenBlocked / (timeElapsedIn30FPS / Numbers.FramesPerSecond);
    }

    public double CalculateHitsPerSecond()
    {
        if (!hitCountDictionary.Any())
        {
            return 0;
        }

        return HitCountInt() / ((double)(TimeDefInt() - TimeInt()) / Numbers.FramesPerSecond);
    }


    // new entry every second during quest (use this for chart?)
    public Dictionary<int, double> damagePerSecondDictionary = new();

    public Dictionary<int, double>? damagePerSecondDictionaryDeserealized;

    public Dictionary<int, int> areaChangesDictionary = new();

    public Dictionary<int, int>? areaChangesDictionaryDeserealized;

    public Dictionary<int, int> cartsDictionary = new();

    public Dictionary<int, int>? cartsDictionaryDeserealized;

    // time <monsterid, monsterhp>
    public Dictionary<int, Dictionary<int, int>> monster1HPDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster2HPDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster3HPDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster4HPDictionary = new();

    public Dictionary<int, Dictionary<int, int>>? monster1HPDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster2HPDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster3HPDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster4HPDictionaryDeserealized;

    // time, itemid, itemquantity
    // can calculate itemuse by counting rows
    // this is a dicitonary where the first int is the quest time,
    // the second int is the item id and the third int is the item quantity of that id.
    // meaning that this is a dictionary of quest time and a list of item ids and quantities respectively.
    public Dictionary<int, List<Dictionary<int, int>>> playerInventoryDictionary = new();

    public Dictionary<int, List<Dictionary<int, int>>>? playerInventoryDictionaryDeserealized;

    public Dictionary<int, List<Dictionary<int, int>>> playerAmmoPouchDictionary = new();

    public Dictionary<int, List<Dictionary<int, int>>>? playerAmmoPouchDictionaryDeserealized;

    public Dictionary<int, List<Dictionary<int, int>>> partnyaBagDictionary = new();

    public Dictionary<int, List<Dictionary<int, int>>>? partnyaBagDictionaryDeserealized;

    // time, areaid, hitstakenblocked
    // can calculate total hits by area by checking areaid, or in total by all sum.
    public Dictionary<int, Dictionary<int, int>> hitsTakenBlockedDictionary = new();

    public Dictionary<int, Dictionary<int, int>>? hitsTakenBlockedDictionaryDeserealized = new();

    public Dictionary<int, int> playerHPDictionary = new();

    public Dictionary<int, int>? playerHPDictionaryDeserealized = new();

    public Dictionary<int, int> playerStaminaDictionary = new();

    public Dictionary<int, int>? playerStaminaDictionaryDeserealized = new();

    public Dictionary<int, double> hitsPerSecondDictionary = new();

    public Dictionary<int, double>? hitsPerSecondDictionaryDeserealized = new();

    public Dictionary<int, double> hitsTakenBlockedPerSecondDictionary = new();

    public Dictionary<int, double>? hitsTakenBlockedPerSecondDictionaryDeserealized = new();

    public Dictionary<int, string> keystrokesDictionary = new();

    public Dictionary<int, string>? keystrokesDictionaryDeserealized = new();

    public Dictionary<int, string> gamepadInputDictionary = new();

    public Dictionary<int, string>? gamepadInputDictionaryDeserealized = new();

    public Dictionary<int, string> mouseInputDictionary = new();

    public Dictionary<int, string>? mouseInputDictionaryDeserealized = new();

    public Dictionary<int, double> actionsPerMinuteDictionary = new();

    public Dictionary<int, double>? actionsPerMinuteDictionaryDeserealized = new();

    public Dictionary<int, string> overlayModeDictionary = new();

    public Dictionary<int, Dictionary<int, double>> monster1AttackMultiplierDictionary = new();

    public Dictionary<int, Dictionary<int, double>> monster1DefenseRateDictionary = new();

    public Dictionary<int, Dictionary<int, double>> monster1SizeMultiplierDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster1PoisonThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster1SleepThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster1ParalysisThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster1BlastThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, int>> monster1StunThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, List<int>>> monster1PartThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, List<int>>> monster2PartThresholdDictionary = new();

    public Dictionary<int, Dictionary<int, double>>? monster1AttackMultiplierDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, double>>? monster1DefenseRateDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, double>>? monster1SizeMultiplierDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster1PoisonThresholdDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster1SleepThresholdDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster1ParalysisThresholdDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster1BlastThresholdDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, int>>? monster1StunThresholdDictionaryDeserealized;

    public Dictionary<int, Dictionary<int, List<int>>>? monster1PartThresholdDictionaryDeserialized;

    public Dictionary<int, Dictionary<int, List<int>>>? monster2PartThresholdDictionaryDeserialized;

    public int previousTimeInt;

    public int previousAttackBuffInt;

    public int previousHitCountInt;

    public double previousDPS;

    public int previousAreaID;

    public int previousGlobalAreaID;

    public int previousQuestID;

    public int previousCartsInt;

    public int previousMonster1HP;

    public int previousMonster2HP;

    public int previousMonster3HP;

    public int previousMonster4HP;

    public int previousTotalInventoryItems;

    public int previousTotalAmmo;

    public int previousTotalPartnyaItems;


    public int previousHitsTakenBlocked;

    public double previousTotalHitsTakenBlockedPerSecond;

    public int previousPlayerHP;

    public int previousPlayerStamina;

    public double previousHitsPerSecond;

    public double previousActionsPerMinute;

    public string previousOverlayMode = "N/A";

    public double previousMonster1AttackMultiplier;

    public double previousMonster1DefenseRate;

    public double previousMonster1SizeMultiplier;

    public int previousMonster1PoisonThreshold;

    public int previousMonster1SleepThreshold;

    public int previousMonster1ParalysisThreshold;

    public int previousMonster1BlastThreshold;

    public int previousMonster1StunThreshold;

    public int previousMonster1Part1Threshold;

    public int previousMonster1Part2Threshold;

    public int previousMonster1Part3Threshold;

    public int previousMonster1Part4Threshold;

    public int previousMonster1Part5Threshold;

    public int previousMonster1Part6Threshold;

    public int previousMonster1Part7Threshold;

    public int previousMonster1Part8Threshold;

    public int previousMonster1Part9Threshold;

    public int previousMonster1Part10Threshold;

    public int previousMonster2Part1Threshold;

    public int previousMonster2Part2Threshold;

    public int previousMonster2Part3Threshold;

    public int previousMonster2Part4Threshold;

    public int previousMonster2Part5Threshold;

    public int previousMonster2Part6Threshold;

    public int previousMonster2Part7Threshold;

    public int previousMonster2Part8Threshold;

    public int previousMonster2Part9Threshold;

    public int previousMonster2Part10Threshold;

    public List<Dictionary<int, int>> InsertInventoryDictionaryIntoList(string inventoryType)
    {
        var list = new List<Dictionary<int, int>>();

        var x = 20;
        switch (inventoryType)
        {
            case "Pouch":
            case "Ammo":
                x = 20;
                break;
            case "Partnya Bag":
                x = 10;
                break;
        }

        for (var i = 1; i <= x; i++)
        {
            var itemID = 0;
            var itemQty = 0;
            Dictionary<int, int> itemIDQuantityDictionary = new();

            if (inventoryType == "Pouch")
            {
                switch (i)
                {
                    case 1:
                        itemID = PouchItem1ID();
                        itemQty = PouchItem1Qty();
                        break;
                    case 2:
                        itemID = PouchItem2ID();
                        itemQty = PouchItem2Qty();
                        break;
                    case 3:
                        itemID = PouchItem3ID();
                        itemQty = PouchItem3Qty();
                        break;
                    case 4:
                        itemID = PouchItem4ID();
                        itemQty = PouchItem4Qty();
                        break;
                    case 5:
                        itemID = PouchItem5ID();
                        itemQty = PouchItem5Qty();
                        break;
                    case 6:
                        itemID = PouchItem6ID();
                        itemQty = PouchItem6Qty();
                        break;
                    case 7:
                        itemID = PouchItem7ID();
                        itemQty = PouchItem7Qty();
                        break;
                    case 8:
                        itemID = PouchItem8ID();
                        itemQty = PouchItem8Qty();
                        break;
                    case 9:
                        itemID = PouchItem9ID();
                        itemQty = PouchItem9Qty();
                        break;
                    case 10:
                        itemID = PouchItem10ID();
                        itemQty = PouchItem10Qty();
                        break;
                    case 11:
                        itemID = PouchItem11ID();
                        itemQty = PouchItem11Qty();
                        break;
                    case 12:
                        itemID = PouchItem12ID();
                        itemQty = PouchItem12Qty();
                        break;
                    case 13:
                        itemID = PouchItem13ID();
                        itemQty = PouchItem13Qty();
                        break;
                    case 14:
                        itemID = PouchItem14ID();
                        itemQty = PouchItem14Qty();
                        break;
                    case 15:
                        itemID = PouchItem15ID();
                        itemQty = PouchItem15Qty();
                        break;
                    case 16:
                        itemID = PouchItem16ID();
                        itemQty = PouchItem16Qty();
                        break;
                    case 17:
                        itemID = PouchItem17ID();
                        itemQty = PouchItem17Qty();
                        break;
                    case 18:
                        itemID = PouchItem18ID();
                        itemQty = PouchItem18Qty();
                        break;
                    case 19:
                        itemID = PouchItem19ID();
                        itemQty = PouchItem19Qty();
                        break;
                    case 20:
                        itemID = PouchItem20ID();
                        itemQty = PouchItem20Qty();
                        break;
                }
            }
            else if (inventoryType == "Ammo")
            {
                switch (i)
                {
                    case 1:
                        itemID = AmmoPouchItem1ID();
                        itemQty = AmmoPouchItem1Qty();
                        break;
                    case 2:
                        itemID = AmmoPouchItem2ID();
                        itemQty = AmmoPouchItem2Qty();
                        break;
                    case 3:
                        itemID = AmmoPouchItem3ID();
                        itemQty = AmmoPouchItem3Qty();
                        break;
                    case 4:
                        itemID = AmmoPouchItem4ID();
                        itemQty = AmmoPouchItem4Qty();
                        break;
                    case 5:
                        itemID = AmmoPouchItem5ID();
                        itemQty = AmmoPouchItem5Qty();
                        break;
                    case 6:
                        itemID = AmmoPouchItem6ID();
                        itemQty = AmmoPouchItem6Qty();
                        break;
                    case 7:
                        itemID = AmmoPouchItem7ID();
                        itemQty = AmmoPouchItem7Qty();
                        break;
                    case 8:
                        itemID = AmmoPouchItem8ID();
                        itemQty = AmmoPouchItem8Qty();
                        break;
                    case 9:
                        itemID = AmmoPouchItem9ID();
                        itemQty = AmmoPouchItem9Qty();
                        break;
                    case 10:
                        itemID = AmmoPouchItem10ID();
                        itemQty = AmmoPouchItem10Qty();
                        break;
                }
            }
            else if (inventoryType == "Partnya Bag")
            {
                switch (i)
                {
                    case 1:
                        itemID = PartnyaBagItem1ID();
                        itemQty = PartnyaBagItem1Qty();
                        break;
                    case 2:
                        itemID = PartnyaBagItem2ID();
                        itemQty = PartnyaBagItem2Qty();
                        break;
                    case 3:
                        itemID = PartnyaBagItem3ID();
                        itemQty = PartnyaBagItem3Qty();
                        break;
                    case 4:
                        itemID = PartnyaBagItem4ID();
                        itemQty = PartnyaBagItem4Qty();
                        break;
                    case 5:
                        itemID = PartnyaBagItem5ID();
                        itemQty = PartnyaBagItem5Qty();
                        break;
                    case 6:
                        itemID = PartnyaBagItem6ID();
                        itemQty = PartnyaBagItem6Qty();
                        break;
                    case 7:
                        itemID = PartnyaBagItem7ID();
                        itemQty = PartnyaBagItem7Qty();
                        break;
                    case 8:
                        itemID = PartnyaBagItem8ID();
                        itemQty = PartnyaBagItem8Qty();
                        break;
                    case 9:
                        itemID = PartnyaBagItem9ID();
                        itemQty = PartnyaBagItem9Qty();
                        break;
                    case 10:
                        itemID = PartnyaBagItem10ID();
                        itemQty = PartnyaBagItem10Qty();
                        break;
                }
            }

            itemIDQuantityDictionary.Add(itemID, itemQty);
            list.Add(itemIDQuantityDictionary);
        }

        return list;
    }

    public int CalculateTotalDictionaryValuesInList(List<Dictionary<int, int>> list)
    {
        if (list.Count > 0)
        {
            return list.SelectMany(x => x.Values).Sum();
        }
        else
        {
            return 0;
        }
    }

    public double GetCurrentQuestElapsedTimeInSeconds()
    {
        if (TimeDefInt() <= 0)
        {
            return 0;
        }

        return (double)(TimeDefInt() - TimeInt()) / Numbers.FramesPerSecond;
    }

    public int previousRoadFloor = 0;

    /// <summary>
    /// Inserts the quest information into dictionaries. If you want to insert a new dictionary, do the following:
    /// 1. Create a dictionary as a property of this class.
    /// 2. Follow the code structure of the current dictionary insertions,
    /// but for this particular dictionary structure if modifications are needed.
    /// 3. Go to DatabaseManager and modify the Quests table for taking into account this new property.
    /// 4. Copy-paste the query into a new function for updating the database schema (PerformUpdateToVersion_x_y_z), for new_Quests table.
    /// See also the 12 steps from SQLite linked for helping update and port data from an old table schema to a new table schema.
    /// 5. Modify InsertQuestData Quests table insertion section taking into account the new property and table schema.
    /// 6. Don't forget to add the clear() in the cleanup functions for the dictionaries.
    /// 7. Complete a quest to see if everything works. Also test the update process.
    /// </summary>
    public void InsertQuestInfoIntoDictionaries()
    {
        // TODO: the above update process should be simplified. refactoring might be needed
        // in many places, not just this function.
        var timeInt = TimeInt();

        if (IsRoad() && AreaID() == 459) // Hunter's Road Base Camp
        {
            if (RoadFloor() + 1 > previousRoadFloor)
            {
                previousRoadFloor = RoadFloor() + 1;
                clearQuestInfoDictionaries();
                clearGraphCollections();
                resetQuestInfoVariables();
            }
        }

        if (previousAttackBuffInt != WeaponRaw() && !attackBuffDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousAttackBuffInt = WeaponRaw();
                attackBuffDictionary.Add(TimeInt(), WeaponRaw());

                // TODO the very last value gets put which is 0 when going back to mezeporta
                lock (attackBuffSync)
                {
                    // Any changes including adding, clearing, etc must be synced.
                    attackBuffCollection.Add(new ObservablePoint(GetCurrentQuestElapsedTimeInSeconds(), WeaponRaw()));
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into attackBuffDictionary");
            }
        }

        if (previousHitCountInt != HitCountInt() && !hitCountDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousHitCountInt = HitCountInt();
                hitCountDictionary.Add(TimeInt(), HitCountInt());
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into hitCountDictionary");
            }
        }

        if (previousDPS != DPS && !damagePerSecondDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousDPS = DPS;
                damagePerSecondDictionary.Add(TimeInt(), DPS);

                lock (damagePerSecondSync)
                {
                    // Any changes including adding, clearing, etc must be synced.
                    damagePerSecondCollection.Add(new ObservablePoint(GetCurrentQuestElapsedTimeInSeconds(), DPS));
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into damagePerSecondDictionary");
            }

        }

        if (previousCartsInt != CurrentFaints() && !cartsDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousCartsInt = CurrentFaints();
                cartsDictionary.Add(TimeInt(), CurrentFaints());
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into cartsDictionary");
            }
        }

        if (previousAreaID != AreaID() && !areaChangesDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousAreaID = AreaID();
                areaChangesDictionary.Add(TimeInt(), AreaID());
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into areaChangesDictionary");
            }
        }

        if (previousMonster1HP != Monster1HPInt() && !monster1HPDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1HP = Monster1HPInt();
                Dictionary<int, int> monster1HPDictionaryMonsterInfo = new();
                monster1HPDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1HPInt());
                monster1HPDictionary.Add(TimeInt(), monster1HPDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1HPDictionary");
            }
        }

        if (previousMonster2HP != Monster2HPInt() && !monster2HPDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster2HP = Monster2HPInt();
                Dictionary<int, int> monster2HPDictionaryMonsterInfo = new();
                monster2HPDictionaryMonsterInfo.Add(LargeMonster2ID(), Monster2HPInt());
                monster2HPDictionary.Add(TimeInt(), monster2HPDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster2HPDictionary");
            }
        }

        if (previousMonster3HP != Monster3HPInt() && !monster3HPDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster3HP = Monster3HPInt();
                Dictionary<int, int> monster3HPDictionaryMonsterInfo = new();
                monster3HPDictionaryMonsterInfo.Add(LargeMonster3ID(), Monster3HPInt());
                monster3HPDictionary.Add(TimeInt(), monster3HPDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster3HPDictionary");
            }

        }

        if (previousMonster4HP != Monster4HPInt() && !monster4HPDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster4HP = Monster4HPInt();
                Dictionary<int, int> monster4HPDictionaryMonsterInfo = new();
                monster4HPDictionaryMonsterInfo.Add(LargeMonster4ID(), Monster4HPInt());
                monster4HPDictionary.Add(TimeInt(), monster4HPDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster4HPDictionary");
            }
        }

        //inventory
        var currentInventoryList = InsertInventoryDictionaryIntoList("Pouch");
        var currentInventorySum = CalculateTotalDictionaryValuesInList(currentInventoryList);
        var lastInventorySum = 0;

        if (playerInventoryDictionary.Values.Any())
        {
            var lastInsertedDictionary = playerInventoryDictionary.Values.Last();

            // Sum the values in the list
            lastInventorySum = lastInsertedDictionary.SelectMany(x => x.Values).Sum();
        }

        if (currentInventorySum != lastInventorySum)
        {
            try
            {
                playerInventoryDictionary.Add(TimeInt(), currentInventoryList);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into playerInventoryDictionary");
            }
        }
        else if (loadedItemsAtQuestStart && !playerInventoryDictionary.Values.Any())
        {
            List<Dictionary<int, int>> itemIDsQuantityList = new();
            for (var i = 1; i <= 20; i++)
            {
                var itemID = 0;
                var itemQty = 0;
                Dictionary<int, int> itemIDQuantityDictionary = new();
                switch (i)
                {
                    case 1:
                        itemID = PouchItem1IDAtQuestStart;
                        itemQty = PouchItem1QuantityAtQuestStart;
                        break;
                    case 2:
                        itemID = PouchItem2IDAtQuestStart;
                        itemQty = PouchItem2QuantityAtQuestStart;
                        break;
                    case 3:
                        itemID = PouchItem3IDAtQuestStart;
                        itemQty = PouchItem3QuantityAtQuestStart;
                        break;
                    case 4:
                        itemID = PouchItem4IDAtQuestStart;
                        itemQty = PouchItem4QuantityAtQuestStart;
                        break;
                    case 5:
                        itemID = PouchItem5IDAtQuestStart;
                        itemQty = PouchItem5QuantityAtQuestStart;
                        break;
                    case 6:
                        itemID = PouchItem6IDAtQuestStart;
                        itemQty = PouchItem6QuantityAtQuestStart;
                        break;
                    case 7:
                        itemID = PouchItem7IDAtQuestStart;
                        itemQty = PouchItem7QuantityAtQuestStart;
                        break;
                    case 8:
                        itemID = PouchItem8IDAtQuestStart;
                        itemQty = PouchItem8QuantityAtQuestStart;
                        break;
                    case 9:
                        itemID = PouchItem9IDAtQuestStart;
                        itemQty = PouchItem9QuantityAtQuestStart;
                        break;
                    case 10:
                        itemID = PouchItem10IDAtQuestStart;
                        itemQty = PouchItem10QuantityAtQuestStart;
                        break;
                    case 11:
                        itemID = PouchItem11IDAtQuestStart;
                        itemQty = PouchItem11QuantityAtQuestStart;
                        break;
                    case 12:
                        itemID = PouchItem12IDAtQuestStart;
                        itemQty = PouchItem12QuantityAtQuestStart;
                        break;
                    case 13:
                        itemID = PouchItem13IDAtQuestStart;
                        itemQty = PouchItem13QuantityAtQuestStart;
                        break;
                    case 14:
                        itemID = PouchItem14IDAtQuestStart;
                        itemQty = PouchItem14QuantityAtQuestStart;
                        break;
                    case 15:
                        itemID = PouchItem15IDAtQuestStart;
                        itemQty = PouchItem15QuantityAtQuestStart;
                        break;
                    case 16:
                        itemID = PouchItem16IDAtQuestStart;
                        itemQty = PouchItem16QuantityAtQuestStart;
                        break;
                    case 17:
                        itemID = PouchItem17IDAtQuestStart;
                        itemQty = PouchItem17QuantityAtQuestStart;
                        break;
                    case 18:
                        itemID = PouchItem18IDAtQuestStart;
                        itemQty = PouchItem18QuantityAtQuestStart;
                        break;
                    case 19:
                        itemID = PouchItem19IDAtQuestStart;
                        itemQty = PouchItem19QuantityAtQuestStart;
                        break;
                    case 20:
                        itemID = PouchItem20IDAtQuestStart;
                        itemQty = PouchItem20QuantityAtQuestStart;
                        break;
                }

                itemIDQuantityDictionary.Add(itemID, itemQty);
                itemIDsQuantityList.Add(itemIDQuantityDictionary);
            }

            try
            {
                playerInventoryDictionary.Add(TimeInt(), itemIDsQuantityList);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into playerInventoryDictionary");
            }
        }

        //ammo
        var currentAmmoList = InsertInventoryDictionaryIntoList("Ammo");
        var currentAmmoSum = CalculateTotalDictionaryValuesInList(currentAmmoList);
        var lastAmmoSum = 0;

        if (playerAmmoPouchDictionary.Values.Any())
        {
            var lastInsertedDictionary = playerAmmoPouchDictionary.Values.Last();

            // Sum the values in the list
            lastAmmoSum = lastInsertedDictionary.SelectMany(x => x.Values).Sum();
        }

        if (currentAmmoSum != lastAmmoSum)
        {
            try
            {
                playerAmmoPouchDictionary.Add(TimeInt(), currentAmmoList);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into playerAmmoPouchDictionary");
            }
        }
        else if (loadedItemsAtQuestStart && !playerAmmoPouchDictionary.Values.Any())
        {
            List<Dictionary<int, int>> itemIDsQuantityList = new();
            for (var i = 1; i <= 20; i++)
            {
                var itemID = 0;
                var itemQty = 0;
                Dictionary<int, int> itemIDQuantityDictionary = new();
                switch (i)
                {
                    case 1:
                        itemID = AmmoPouchItem1IDAtQuestStart;
                        itemQty = AmmoPouchItem1QuantityAtQuestStart;
                        break;
                    case 2:
                        itemID = AmmoPouchItem2IDAtQuestStart;
                        itemQty = AmmoPouchItem2QuantityAtQuestStart;
                        break;
                    case 3:
                        itemID = AmmoPouchItem3IDAtQuestStart;
                        itemQty = AmmoPouchItem3QuantityAtQuestStart;
                        break;
                    case 4:
                        itemID = AmmoPouchItem4IDAtQuestStart;
                        itemQty = AmmoPouchItem4QuantityAtQuestStart;
                        break;
                    case 5:
                        itemID = AmmoPouchItem5IDAtQuestStart;
                        itemQty = AmmoPouchItem5QuantityAtQuestStart;
                        break;
                    case 6:
                        itemID = AmmoPouchItem6IDAtQuestStart;
                        itemQty = AmmoPouchItem6QuantityAtQuestStart;
                        break;
                    case 7:
                        itemID = AmmoPouchItem7IDAtQuestStart;
                        itemQty = AmmoPouchItem7QuantityAtQuestStart;
                        break;
                    case 8:
                        itemID = AmmoPouchItem8IDAtQuestStart;
                        itemQty = AmmoPouchItem8QuantityAtQuestStart;
                        break;
                    case 9:
                        itemID = AmmoPouchItem9IDAtQuestStart;
                        itemQty = AmmoPouchItem9QuantityAtQuestStart;
                        break;
                    case 10:
                        itemID = AmmoPouchItem10IDAtQuestStart;
                        itemQty = AmmoPouchItem10QuantityAtQuestStart;
                        break;
                }

                itemIDQuantityDictionary.Add(itemID, itemQty);
                itemIDsQuantityList.Add(itemIDQuantityDictionary);
            }

            try
            {
                playerAmmoPouchDictionary.Add(TimeInt(), itemIDsQuantityList);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into playerAmmoPouchDictionary");
            }
        }

        //partnya bag
        var currentPartnyaBagList = InsertInventoryDictionaryIntoList("Partnya Bag");
        var currentPartnyaBagSum = CalculateTotalDictionaryValuesInList(currentPartnyaBagList);
        var lastPartnyaBagSum = 0;

        if (partnyaBagDictionary.Values.Any())
        {
            var lastInsertedDictionary = partnyaBagDictionary.Values.Last();

            // Sum the values in the list
            lastPartnyaBagSum = lastInsertedDictionary.SelectMany(x => x.Values).Sum();
        }

        if (currentPartnyaBagSum != lastPartnyaBagSum)
        {
            try
            {
                partnyaBagDictionary.Add(TimeInt(), currentPartnyaBagList);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into partnyaBagDictionary");
            }
        }
        else if (loadedItemsAtQuestStart && !partnyaBagDictionary.Values.Any())
        {
            List<Dictionary<int, int>> itemIDsQuantityList = new();
            for (var i = 1; i <= 20; i++)
            {
                var itemID = 0;
                var itemQty = 0;
                Dictionary<int, int> itemIDQuantityDictionary = new();
                switch (i)
                {
                    case 1:
                        itemID = PartnyaBagItem1IDAtQuestStart;
                        itemQty = PartnyaBagItem1QuantityAtQuestStart;
                        break;
                    case 2:
                        itemID = PartnyaBagItem2IDAtQuestStart;
                        itemQty = PartnyaBagItem2QuantityAtQuestStart;
                        break;
                    case 3:
                        itemID = PartnyaBagItem3IDAtQuestStart;
                        itemQty = PartnyaBagItem3QuantityAtQuestStart;
                        break;
                    case 4:
                        itemID = PartnyaBagItem4IDAtQuestStart;
                        itemQty = PartnyaBagItem4QuantityAtQuestStart;
                        break;
                    case 5:
                        itemID = PartnyaBagItem5IDAtQuestStart;
                        itemQty = PartnyaBagItem5QuantityAtQuestStart;
                        break;
                    case 6:
                        itemID = PartnyaBagItem6IDAtQuestStart;
                        itemQty = PartnyaBagItem6QuantityAtQuestStart;
                        break;
                    case 7:
                        itemID = PartnyaBagItem7IDAtQuestStart;
                        itemQty = PartnyaBagItem7QuantityAtQuestStart;
                        break;
                    case 8:
                        itemID = PartnyaBagItem8IDAtQuestStart;
                        itemQty = PartnyaBagItem8QuantityAtQuestStart;
                        break;
                    case 9:
                        itemID = PartnyaBagItem9IDAtQuestStart;
                        itemQty = PartnyaBagItem9QuantityAtQuestStart;
                        break;
                    case 10:
                        itemID = PartnyaBagItem10IDAtQuestStart;
                        itemQty = PartnyaBagItem10QuantityAtQuestStart;
                        break;
                }

                itemIDQuantityDictionary.Add(itemID, itemQty);
                itemIDsQuantityList.Add(itemIDQuantityDictionary);
            }

            try
            {
                partnyaBagDictionary.Add(TimeInt(), itemIDsQuantityList);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into partnyaBagDictionary");
            }
        }

        if (previousHitsTakenBlocked != AreaHitsTakenBlocked() && AreaHitsTakenBlocked() != 0 && !hitsTakenBlockedDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousHitsTakenBlocked = AreaHitsTakenBlocked();
                Dictionary<int, int> hitsAreaPairs = new();
                hitsAreaPairs.Add(AreaID(), AreaHitsTakenBlocked());
                hitsTakenBlockedDictionary.Add(TimeInt(), hitsAreaPairs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into hitsTakenBlockedDictionary");
            }
        }

        if (previousPlayerHP != HunterHP() && playerHPDictionary.TryGetValue(TimeInt(), out var hp) == false)
        {
            try
            {
                previousPlayerHP = HunterHP();
                playerHPDictionary.Add(TimeInt(), HunterHP());
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into playerHPDictionary");
            }
        }

        if (previousPlayerStamina != HunterStamina() && !playerStaminaDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousPlayerStamina = HunterStamina();
                playerStaminaDictionary.Add(TimeInt(), HunterStamina());
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into playerStaminaDictionary");
            }
        }

        if (previousHitsPerSecond != HitsPerSecond && !hitsPerSecondDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousHitsPerSecond = HitsPerSecond;
                hitsPerSecondDictionary.Add(TimeInt(), HitsPerSecond);

                lock (hitsPerSecondSync)
                {
                    // Any changes including adding, clearing, etc must be synced.
                    hitsPerSecondCollection.Add(new ObservablePoint(GetCurrentQuestElapsedTimeInSeconds(), HitsPerSecond));
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into hitsPerSecondDictionary");
            }

        }

        if (previousTotalHitsTakenBlockedPerSecond != TotalHitsTakenBlockedPerSecond && !hitsTakenBlockedPerSecondDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousTotalHitsTakenBlockedPerSecond = TotalHitsTakenBlockedPerSecond;
                hitsTakenBlockedPerSecondDictionary.Add(TimeInt(), TotalHitsTakenBlockedPerSecond);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into hitsTakenBlockedPerSecondDictionary");
            }
        }

        if (previousActionsPerMinute != APM && !actionsPerMinuteDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousActionsPerMinute = APM;
                actionsPerMinuteDictionary.Add(TimeInt(), APM);

                lock (actionsPerMinuteSync)
                {
                    // Any changes including adding, clearing, etc must be synced.
                    actionsPerMinuteCollection.Add(new ObservablePoint(GetCurrentQuestElapsedTimeInSeconds(), APM));
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into actionsPerMinuteDictionary");
            }
        }

        if (previousOverlayMode != GetOverlayMode() && !overlayModeDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousOverlayMode = GetOverlayMode();
                overlayModeDictionary.Add(TimeInt(), GetOverlayMode());
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into overlayModeDictionary");
            }
        }

        if (previousMonster1AttackMultiplier != Monster1AttackMultForDictionary() && !monster1AttackMultiplierDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1AttackMultiplier = Monster1AttackMultForDictionary();
                Dictionary<int, double> monster1AttackMultiplierDictionaryMonsterInfo = new();
                monster1AttackMultiplierDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1AttackMultForDictionary());
                monster1AttackMultiplierDictionary.Add(TimeInt(), monster1AttackMultiplierDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1AttackMultiplierDictionary");
            }
        }

        if (previousMonster1DefenseRate != Monster1DefMultForDictionary() && !monster1DefenseRateDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1DefenseRate = Monster1DefMultForDictionary();
                Dictionary<int, double> monster1DefenseRateDictionaryMonsterInfo = new();
                monster1DefenseRateDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1DefMultForDictionary());
                monster1DefenseRateDictionary.Add(TimeInt(), monster1DefenseRateDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1DefenseRateDictionary");
            }
        }

        if (previousMonster1SizeMultiplier != Monster1SizeMultForDictionary() && !monster1SizeMultiplierDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1SizeMultiplier = Monster1SizeMultForDictionary();
                Dictionary<int, double> monster1SizeMultiplierDictionaryMonsterInfo = new();
                monster1SizeMultiplierDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1SizeMultForDictionary());
                monster1SizeMultiplierDictionary.Add(TimeInt(), monster1SizeMultiplierDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1SizeMultiplierDictionary");
            }
        }

        if (previousMonster1PoisonThreshold != Monster1PoisonForDictionary() && !monster1PoisonThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1PoisonThreshold = Monster1PoisonForDictionary();
                Dictionary<int, int> monster1PoisonThresholdDictionaryMonsterInfo = new();
                monster1PoisonThresholdDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1PoisonForDictionary());
                monster1PoisonThresholdDictionary.Add(TimeInt(), monster1PoisonThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1PoisonThresholdDictionary");
            }
        }

        if (previousMonster1SleepThreshold != Monster1SleepForDictionary() && !monster1SleepThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1SleepThreshold = Monster1SleepForDictionary();
                Dictionary<int, int> monster1SleepThresholdDictionaryMonsterInfo = new();
                monster1SleepThresholdDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1SleepForDictionary());
                monster1SleepThresholdDictionary.Add(TimeInt(), monster1SleepThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1SleepThresholdDictionary");
            }
        }

        if (previousMonster1ParalysisThreshold != Monster1ParalysisForDictionary() && !monster1ParalysisThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1ParalysisThreshold = Monster1ParalysisForDictionary();
                Dictionary<int, int> monster1ParalysisThresholdDictionaryMonsterInfo = new();
                monster1ParalysisThresholdDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1ParalysisForDictionary());
                monster1ParalysisThresholdDictionary.Add(TimeInt(), monster1ParalysisThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1ParalysisThresholdDictionary");
            }
        }

        if (previousMonster1BlastThreshold != Monster1BlastForDictionary() && !monster1BlastThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1BlastThreshold = Monster1BlastForDictionary();
                Dictionary<int, int> monster1BlastThresholdDictionaryMonsterInfo = new();
                monster1BlastThresholdDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1BlastForDictionary());
                monster1BlastThresholdDictionary.Add(TimeInt(), monster1BlastThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1BlastThresholdDictionary");
            }
        }

        if (previousMonster1StunThreshold != Monster1StunForDictionary() && !monster1StunThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1StunThreshold = Monster1StunForDictionary();
                Dictionary<int, int> monster1StunThresholdDictionaryMonsterInfo = new();
                monster1StunThresholdDictionaryMonsterInfo.Add(LargeMonster1ID(), Monster1StunForDictionary());
                monster1StunThresholdDictionary.Add(TimeInt(), monster1StunThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1StunThresholdDictionary");
            }
        }

        // TODO: may want to optimize performance/readability
        if ((previousMonster1Part1Threshold != Monster1Part1() ||
            previousMonster1Part2Threshold != Monster1Part2() ||
            previousMonster1Part3Threshold != Monster1Part3() ||
            previousMonster1Part4Threshold != Monster1Part4() ||
            previousMonster1Part5Threshold != Monster1Part5() ||
            previousMonster1Part6Threshold != Monster1Part6() ||
            previousMonster1Part7Threshold != Monster1Part7() ||
            previousMonster1Part8Threshold != Monster1Part8() ||
            previousMonster1Part9Threshold != Monster1Part9() ||
            previousMonster1Part10Threshold != Monster1Part10()
            )
            && !monster1PartThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster1Part1Threshold = Monster1Part1();
                previousMonster1Part2Threshold = Monster1Part2();
                previousMonster1Part3Threshold = Monster1Part3();
                previousMonster1Part4Threshold = Monster1Part4();
                previousMonster1Part5Threshold = Monster1Part5();
                previousMonster1Part6Threshold = Monster1Part6();
                previousMonster1Part7Threshold = Monster1Part7();
                previousMonster1Part8Threshold = Monster1Part8();
                previousMonster1Part9Threshold = Monster1Part9();
                previousMonster1Part10Threshold = Monster1Part10();
                Dictionary<int, List<int>> monster1PartThresholdDictionaryMonsterInfo = new();
                var partsList = new List<int>() {
                    Monster1Part1(),
                    Monster1Part2(),
                    Monster1Part3(),
                    Monster1Part4(),
                    Monster1Part5(),
                    Monster1Part6(),
                    Monster1Part7(),
                    Monster1Part8(),
                    Monster1Part9(),
                    Monster1Part10()
                };
                monster1PartThresholdDictionaryMonsterInfo.Add(LargeMonster1ID(), partsList);
                monster1PartThresholdDictionary.Add(TimeInt(), monster1PartThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster1PartThresholdDictionary");
            }
        }

        if ((previousMonster2Part1Threshold != Monster2Part1() ||
            previousMonster2Part2Threshold != Monster2Part2() ||
            previousMonster2Part3Threshold != Monster2Part3() ||
            previousMonster2Part4Threshold != Monster2Part4() ||
            previousMonster2Part5Threshold != Monster2Part5() ||
            previousMonster2Part6Threshold != Monster2Part6() ||
            previousMonster2Part7Threshold != Monster2Part7() ||
            previousMonster2Part8Threshold != Monster2Part8() ||
            previousMonster2Part9Threshold != Monster2Part9() ||
            previousMonster2Part10Threshold != Monster2Part10()
            )
            && !monster2PartThresholdDictionary.ContainsKey(TimeInt()))
        {
            try
            {
                previousMonster2Part1Threshold = Monster2Part1();
                previousMonster2Part2Threshold = Monster2Part2();
                previousMonster2Part3Threshold = Monster2Part3();
                previousMonster2Part4Threshold = Monster2Part4();
                previousMonster2Part5Threshold = Monster2Part5();
                previousMonster2Part6Threshold = Monster2Part6();
                previousMonster2Part7Threshold = Monster2Part7();
                previousMonster2Part8Threshold = Monster2Part8();
                previousMonster2Part9Threshold = Monster2Part9();
                previousMonster2Part10Threshold = Monster2Part10();
                Dictionary<int, List<int>> monster2PartThresholdDictionaryMonsterInfo = new();
                var partsList = new List<int>() {
                    Monster2Part1(),
                    Monster2Part2(),
                    Monster2Part3(),
                    Monster2Part4(),
                    Monster2Part5(),
                    Monster2Part6(),
                    Monster2Part7(),
                    Monster2Part8(),
                    Monster2Part9(),
                    Monster2Part10()
                };
                monster2PartThresholdDictionaryMonsterInfo.Add(LargeMonster2ID(), partsList);
                monster2PartThresholdDictionary.Add(TimeInt(), monster2PartThresholdDictionaryMonsterInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Warn(ex, "Could not insert into monster2PartThresholdDictionary");
            }
        }
    }

    public void resetQuestInfoVariables()
    {
        PouchItem1IDAtQuestStart = 0;
        PouchItem2IDAtQuestStart = 0;
        PouchItem3IDAtQuestStart = 0;
        PouchItem4IDAtQuestStart = 0;
        PouchItem5IDAtQuestStart = 0;
        PouchItem6IDAtQuestStart = 0;
        PouchItem7IDAtQuestStart = 0;
        PouchItem8IDAtQuestStart = 0;
        PouchItem9IDAtQuestStart = 0;
        PouchItem10IDAtQuestStart = 0;
        PouchItem11IDAtQuestStart = 0;
        PouchItem12IDAtQuestStart = 0;
        PouchItem13IDAtQuestStart = 0;
        PouchItem14IDAtQuestStart = 0;
        PouchItem15IDAtQuestStart = 0;
        PouchItem16IDAtQuestStart = 0;
        PouchItem17IDAtQuestStart = 0;
        PouchItem18IDAtQuestStart = 0;
        PouchItem19IDAtQuestStart = 0;
        PouchItem20IDAtQuestStart = 0;
        PouchItem1QuantityAtQuestStart = 0;
        PouchItem2QuantityAtQuestStart = 0;
        PouchItem3QuantityAtQuestStart = 0;
        PouchItem4QuantityAtQuestStart = 0;
        PouchItem5QuantityAtQuestStart = 0;
        PouchItem6QuantityAtQuestStart = 0;
        PouchItem7QuantityAtQuestStart = 0;
        PouchItem8QuantityAtQuestStart = 0;
        PouchItem9QuantityAtQuestStart = 0;
        PouchItem10QuantityAtQuestStart = 0;
        PouchItem11QuantityAtQuestStart = 0;
        PouchItem12QuantityAtQuestStart = 0;
        PouchItem13QuantityAtQuestStart = 0;
        PouchItem14QuantityAtQuestStart = 0;
        PouchItem15QuantityAtQuestStart = 0;
        PouchItem16QuantityAtQuestStart = 0;
        PouchItem17QuantityAtQuestStart = 0;
        PouchItem18QuantityAtQuestStart = 0;
        PouchItem19QuantityAtQuestStart = 0;
        PouchItem20QuantityAtQuestStart = 0;

        AmmoPouchItem1IDAtQuestStart = 0;
        AmmoPouchItem2IDAtQuestStart = 0;
        AmmoPouchItem3IDAtQuestStart = 0;
        AmmoPouchItem4IDAtQuestStart = 0;
        AmmoPouchItem5IDAtQuestStart = 0;
        AmmoPouchItem6IDAtQuestStart = 0;
        AmmoPouchItem7IDAtQuestStart = 0;
        AmmoPouchItem8IDAtQuestStart = 0;
        AmmoPouchItem9IDAtQuestStart = 0;
        AmmoPouchItem10IDAtQuestStart = 0;
        AmmoPouchItem1QuantityAtQuestStart = 0;
        AmmoPouchItem2QuantityAtQuestStart = 0;
        AmmoPouchItem3QuantityAtQuestStart = 0;
        AmmoPouchItem4QuantityAtQuestStart = 0;
        AmmoPouchItem5QuantityAtQuestStart = 0;
        AmmoPouchItem6QuantityAtQuestStart = 0;
        AmmoPouchItem7QuantityAtQuestStart = 0;
        AmmoPouchItem8QuantityAtQuestStart = 0;
        AmmoPouchItem9QuantityAtQuestStart = 0;
        AmmoPouchItem10QuantityAtQuestStart = 0;

        PartnyaBagItem1IDAtQuestStart = 0;
        PartnyaBagItem2IDAtQuestStart = 0;
        PartnyaBagItem3IDAtQuestStart = 0;
        PartnyaBagItem4IDAtQuestStart = 0;
        PartnyaBagItem5IDAtQuestStart = 0;
        PartnyaBagItem6IDAtQuestStart = 0;
        PartnyaBagItem7IDAtQuestStart = 0;
        PartnyaBagItem8IDAtQuestStart = 0;
        PartnyaBagItem9IDAtQuestStart = 0;
        PartnyaBagItem10IDAtQuestStart = 0;
        PartnyaBagItem1QuantityAtQuestStart = 0;
        PartnyaBagItem2QuantityAtQuestStart = 0;
        PartnyaBagItem3QuantityAtQuestStart = 0;
        PartnyaBagItem4QuantityAtQuestStart = 0;
        PartnyaBagItem5QuantityAtQuestStart = 0;
        PartnyaBagItem6QuantityAtQuestStart = 0;
        PartnyaBagItem7QuantityAtQuestStart = 0;
        PartnyaBagItem8QuantityAtQuestStart = 0;
        PartnyaBagItem9QuantityAtQuestStart = 0;
        PartnyaBagItem10QuantityAtQuestStart = 0;

        previousAttackBuffInt = 0;
        previousDPS = 0;
        previousHitCountInt = 0;
        previousAreaID = 0;
        previousCartsInt = 0;
        previousMonster1HP = 0;
        previousMonster2HP = 0;
        previousMonster3HP = 0;
        previousMonster4HP = 0;
        previousTotalInventoryItems = 0;
        previousTotalAmmo = 0;
        previousTotalPartnyaItems = 0;
        previousHitsTakenBlocked = 0;
        previousTotalHitsTakenBlockedPerSecond = 0;
        previousPlayerHP = 0;
        previousPlayerStamina = 0;
        previousHitsPerSecond = 0;
        previousActionsPerMinute = 0;
        previousOverlayMode = "N/A";
        previousRoadFloor = 0;

        previousMonster1AttackMultiplier = 0;
        previousMonster1DefenseRate = 0;
        previousMonster1SizeMultiplier = 0;
        previousMonster1PoisonThreshold = 0;
        previousMonster1SleepThreshold = 0;
        previousMonster1ParalysisThreshold = 0;
        previousMonster1BlastThreshold = 0;
        previousMonster1StunThreshold = 0;
        previousMonster1Part1Threshold = 0;
        previousMonster1Part2Threshold = 0;
        previousMonster1Part3Threshold = 0;
        previousMonster1Part4Threshold = 0;
        previousMonster1Part5Threshold = 0;
        previousMonster1Part6Threshold = 0;
        previousMonster1Part7Threshold = 0;
        previousMonster1Part8Threshold = 0;
        previousMonster1Part9Threshold = 0;
        previousMonster1Part10Threshold = 0;
        previousMonster2Part1Threshold = 0;
        previousMonster2Part2Threshold = 0;
        previousMonster2Part3Threshold = 0;
        previousMonster2Part4Threshold = 0;
        previousMonster2Part5Threshold = 0;
        previousMonster2Part6Threshold = 0;
        previousMonster2Part7Threshold = 0;
        previousMonster2Part8Threshold = 0;
        previousMonster2Part9Threshold = 0;
        previousMonster2Part10Threshold = 0;

    }

    public void clearQuestInfoDictionaries()
    {
        attackBuffDictionary.Clear();
        hitCountDictionary.Clear();
        damageDealtDictionary.Clear();
        damagePerSecondDictionary.Clear();
        areaChangesDictionary.Clear();
        cartsDictionary.Clear();
        monster1HPDictionary.Clear();
        monster2HPDictionary.Clear();
        monster3HPDictionary.Clear();
        monster4HPDictionary.Clear();
        playerInventoryDictionary.Clear();
        playerAmmoPouchDictionary.Clear();
        partnyaBagDictionary.Clear();
        hitsTakenBlockedDictionary.Clear();
        playerHPDictionary.Clear();
        playerStaminaDictionary.Clear();
        hitsPerSecondDictionary.Clear();
        hitsTakenBlockedPerSecondDictionary.Clear();
        keystrokesDictionary.Clear();
        mouseInputDictionary.Clear();
        gamepadInputDictionary.Clear();
        actionsPerMinuteDictionary.Clear();
        overlayModeDictionary.Clear();

        monster1AttackMultiplierDictionary.Clear();
        monster1DefenseRateDictionary.Clear();
        monster1SizeMultiplierDictionary.Clear();
        monster1PoisonThresholdDictionary.Clear();
        monster1SleepThresholdDictionary.Clear();
        monster1ParalysisThresholdDictionary.Clear();
        monster1BlastThresholdDictionary.Clear();
        monster1StunThresholdDictionary.Clear();
        monster1PartThresholdDictionary.Clear();
        monster2PartThresholdDictionary.Clear();
    }

    public void clearGraphCollections()
    {
        lock (attackBuffSync)
        {
            // Any changes including adding, clearing, etc must be synced.
            attackBuffCollection.Clear();
        }

        lock (actionsPerMinuteSync)
        {
            actionsPerMinuteCollection.Clear();
        }

        lock (damagePerSecondSync)
        {
            damagePerSecondCollection.Clear();
        }

        lock (hitsPerSecondSync)
        {
            hitsPerSecondCollection.Clear();
        }
    }

    // Get all countries
    public IEnumerable<Country> Countries
    {
        get
        {
            return RestCountriesService.GetAllCountries();
        }
    }

    public string OverlayModeWatermarkText
    {
        get
        {
            var overlayMode = GetOverlayMode();
            overlayMode = overlayMode.Replace("(", string.Empty);
            overlayMode = overlayMode.Replace(")", string.Empty);
            overlayMode = overlayMode.Trim();
            if (overlayMode == null || overlayMode == string.Empty)
            {
                overlayMode = "Standard";
            }

            return overlayMode;
        }
    }

    public string QuestIDBind
    {
        get
        {
            return QuestID().ToString(CultureInfo.InvariantCulture);
        }
    }

    public ObservableCollection<RecentRuns> RecentRuns { get; set; } = new();

    // TODO: the plural/singular is inconsistent
    public List<FastestRun> FastestRuns { get; set; } = new();

    public List<RecentRuns> CalendarRuns { get; set; } = new();

    public ObservableCollection<QuestLogsOption> QuestLogsSearchOption { get; set; } = new ObservableCollection<QuestLogsOption>()
    {
        new QuestLogsOption{Name = "Compendium", IsSelected = false},
        new QuestLogsOption{Name = "Calendar", IsSelected = false},
        new QuestLogsOption{Name = "Personal Best", IsSelected = false},
        new QuestLogsOption{Name = "Gear", IsSelected = false},
        new QuestLogsOption{Name = "Top 20", IsSelected = false},
        new QuestLogsOption{Name = "Weapon Stats", IsSelected = false},
        new QuestLogsOption{Name = "Most Recent", IsSelected = false},
        new QuestLogsOption{Name = "YouTube", IsSelected = false},
        new QuestLogsOption{Name = "Stats (Graphs)", IsSelected = false},
        new QuestLogsOption{Name = "Stats (Text)", IsSelected = false},
    };

    public string ReplaceAlphaChannel(string hexColor, string alphaChannel)
    {
        var index = hexColor.IndexOf("ff", StringComparison.InvariantCulture);
        if (index != -1)
        {
            hexColor = hexColor.Remove(index, 2).Insert(index, alphaChannel);
        }

        return hexColor;
    }

    public static string StaticReplaceFirstFF(string hexColor)
    {
        var index = hexColor.IndexOf("ff", StringComparison.InvariantCulture);
        if (index != -1)
        {
            hexColor = hexColor.Remove(index, 2).Insert(index, "00");
        }

        return hexColor;
    }

    public uint HexColorToDecimal(string hexColor, string? alphaChannel = "ff")
    {
        if (hexColor.StartsWith("#", StringComparison.InvariantCulture))
        {
            hexColor = hexColor.Substring(1);
        }

        if (hexColor.Length == 6)
        {
            hexColor = hexColor.Insert(0, "ff");
        }
        else
        {
            hexColor = hexColor.Remove(0, 2).Insert(0, "ff");
        }

        if (alphaChannel != null && alphaChannel != "ff")
        {
            hexColor = ReplaceAlphaChannel(hexColor, alphaChannel);
        }

        return uint.Parse(hexColor, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }

    public static uint StaticHexColorToDecimal(string hexColor, bool? toTransparent = false)
    {
        if (hexColor.StartsWith("#", StringComparison.InvariantCulture))
        {
            hexColor = hexColor.Substring(1);
        }

        if (hexColor.Length == 6)
        {
            hexColor = hexColor.Insert(0, "ff");
        }
        else
        {
            hexColor = hexColor.Remove(0, 2).Insert(0, "ff");
        }

        if (toTransparent != null && toTransparent == true)
        {
            hexColor = StaticReplaceFirstFF(hexColor);
        }

        return uint.Parse(hexColor, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }

    public QuestLogsOption SelectedOption { get; set; } = new QuestLogsOption { Name = "Default", IsSelected = true };

    private DateTime ProgramStart = DateTime.UtcNow;

    public TimeSpan CurrentSessionTime
    {
        get
        {
            var ProgramEnd = DateTime.UtcNow;
            var duration = ProgramEnd - ProgramStart;
            return duration;
        }
    }

    public int currentMonster1MaxHP { get; set; }

    /// <summary>
    /// Shows the current hp percentage.
    /// </summary>
    /// <returns></returns>
    public static bool ShowCurrentHPPercentage()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");
        return s.EnableCurrentHPPercentage;
    }

    /// <summary>
    /// Gets the monster1 ehp percent.
    /// </summary>
    /// <returns></returns>
    public string GetMonster1EHPPercent()
    {
        if (currentMonster1MaxHP < int.Parse(Monster1HP, CultureInfo.InvariantCulture))
        {
            currentMonster1MaxHP = int.Parse(Monster1HP, CultureInfo.InvariantCulture);
        }

        if (currentMonster1MaxHP == 0 || GetMonster1EHP() == 0) // should be OK
        {
            currentMonster1MaxHP = 1;
        }

        if (!ShowCurrentHPPercentage())
        {
            return string.Empty;
        }

        return string.Format(CultureInfo.InvariantCulture, " ({0:0}%)", (float)int.Parse(Monster1HP, CultureInfo.InvariantCulture) / currentMonster1MaxHP * 100.0);
    }

    /// <summary>
    /// Gets the monster1 ehp.
    /// </summary>
    /// <returns></returns>
    public int GetMonster1EHP()
    {
        return DisplayMonsterEHP(Monster1DefMult(), Monster1HPInt());
    }

    /// <summary>
    /// Gets the monster1 maximum ehp.
    /// </summary>
    /// <returns></returns>
    public int GetMonster1MaxEHP()
    {
        return currentMonster1MaxHP;
    }

    /// <summary>
    /// Gets the max faints
    /// </summary>
    /// <returns></returns>
    public string GetMaxFaints()
    {
        var s = (Settings)Application.Current.TryFindResource("Settings");

        switch (s.MaxFaintsOverride)
        {
            default:
                return MaxFaints().ToString(CultureInfo.InvariantCulture);
            case "Normal Quests":
                return MaxFaints().ToString(CultureInfo.InvariantCulture);
            case "Shiten/Conquest/Pioneer/Daily/Caravan/Interception Quests":
                return AlternativeMaxFaints().ToString(CultureInfo.InvariantCulture);
            case "Automatic":
                if (roadOverride() != null && roadOverride() == false)
                {
                    return MaxFaints().ToString(CultureInfo.InvariantCulture);
                }

                if
                (

                        CaravanOverride() && !
                        (
                            QuestID() == 23603 ||
                            RankBand() == 70 ||
                            QuestID() == 23602 ||
                            QuestID() == 23604 ||
                            QuestID() == 23588 ||
                            QuestID() == 23592 ||
                            QuestID() == 23596 ||
                            QuestID() == 23601 ||
                            QuestID() == 23599 ||
                            QuestID() == 23595 ||
                            QuestID() == 23591 ||
                            QuestID() == 23587 ||
                            QuestID() == 23598 ||
                            QuestID() == 23594 ||
                            QuestID() == 23590 ||
                            QuestID() == 23586 ||
                            QuestID() == 23597 ||
                            QuestID() == 23593 ||
                            QuestID() == 23589 ||
                            QuestID() == 23585
                        )


                    ||

                    QuestID() == 23603 ||
                    RankBand() == 70 ||
                    QuestID() == 23602 ||
                    QuestID() == 23604 ||
                    QuestID() == 23588 ||
                    QuestID() == 23592 ||
                    QuestID() == 23596 ||
                    QuestID() == 23601 ||
                    QuestID() == 23599 ||
                    QuestID() == 23595 ||
                    QuestID() == 23591 ||
                    QuestID() == 23587 ||
                    QuestID() == 23598 ||
                    QuestID() == 23594 ||
                    QuestID() == 23590 ||
                    QuestID() == 23586 ||
                    QuestID() == 23597 ||
                    QuestID() == 23593 ||
                    QuestID() == 23589 ||
                    QuestID() == 23585
                    )
                {
                    return AlternativeMaxFaints().ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    return MaxFaints().ToString(CultureInfo.InvariantCulture);
                }
        }
    }

    /// <summary>
    /// Gets the color of the armor.
    /// </summary>
    /// <returns></returns>
    public string GetArmorColor()
    {
        EZlion.Mapper.ArmorColor.IDName.TryGetValue(ArmorColor(), out var colorname);
        return colorname + string.Empty;
    }

    /// <summary>
    /// Gets the weapon style from identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetWeaponStyleFromID(int id)
    {
        return id switch
        {
            0 => "Earth",
            1 => "Heaven",
            2 => "Storm",
            3 => "Extreme",
            _ => "None",
        };
    }

    /// <summary>
    /// Gets the area icon from identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetAreaIconFromID(int id) // TODO: are highlands, tidal island or painted falls icons correct?
    {
        if (id >= 470 && id < 0)
        {
            return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/cattleya.png";
        }
        else
        {
            return FindAreaIcon(id);
        }
    }

    public static string FindAreaIcon(int id)
    {
        var AreaGroup = new List<int> { 0 };

        foreach (var kvp in AreaIconCollection.AreaIconID)
        {
            var areaIDs = kvp.Key;

            if (areaIDs.Contains(id))
            {
                AreaGroup = kvp.Key;
                break;
            }
        }

        return DetermineAreaIcon(AreaGroup);
    }

    public static string DetermineAreaIcon(List<int> key)
    {
        var areaIcon = AreaIconCollection.AreaIconID.ContainsKey(key);
        if (!areaIcon)
        {
            return "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/cattleya.png";
        }
        else
        {
            return AreaIconCollection.AreaIconID[key];
        }
    }

    /// <summary>
    /// Gets the poogie clothes.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public string GetPoogieClothes(int id)
    {
        string? clothesValue1;
        _ = EZlion.Mapper.PoogieCostume.IDName.TryGetValue(id, out clothesValue1);  //returns true
        return clothesValue1 + string.Empty;
    }

    /// <summary>
    /// Gets the caravan skills.
    /// </summary>
    /// <returns></returns>
    public string GetCaravanSkillsWithoutMarkdown(DataLoader dataLoader)
    {
        int id1 = dataLoader.model.CaravanSkill1();
        int id2 = dataLoader.model.CaravanSkill2();
        int id3 = dataLoader.model.CaravanSkill3();

        SkillCaravan.IDName.TryGetValue(id1, out var caravanSkillName1);
        SkillCaravan.IDName.TryGetValue(id2, out var caravanSkillName2);
        SkillCaravan.IDName.TryGetValue(id3, out var caravanSkillName3);

        if (caravanSkillName1 == string.Empty || caravanSkillName1 == "None")
        {
            return "None";
        }
        else if (caravanSkillName2 == string.Empty || caravanSkillName2 == "None")
        {
            return caravanSkillName1 + string.Empty;
        }
        else if (caravanSkillName3 == string.Empty || caravanSkillName3 == "None")
        {
            return caravanSkillName1 + ", " + caravanSkillName2;
        }
        else
        {
            return caravanSkillName1 + ", " + caravanSkillName2 + ", " + caravanSkillName3;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Reloads the data.
    /// </summary>
    public void ReloadData(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}