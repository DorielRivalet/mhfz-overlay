// © 2023 The mhfz-overlay developers.
// Use of this source code is governed by a MIT license that can be
// found in the LICENSE file.

namespace MHFZ_Overlay.Models.Collections;

using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// The monster image list.
/// </summary>
public static class MonsterImagesDiscord
{
    public static ReadOnlyDictionary<int, string> MonsterImageID { get; } = new (new Dictionary<int, string>
    {
        { 0, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/none.png" },
        { 1, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/rathian.png" },
        { 3, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/kelbi.png" },
        { 4, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/mosswine.png" },
        { 5, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/bullfango.png" },
        { 6, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/yian_kut-ku.png" },
        { 7, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/lao-shan_lung.png" },
        { 8, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/cephadrome.png" },
        { 9, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/felyne.png" },
        { 10, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 12, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/aptonoth.png" },
        { 13, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/genprey.png" },
        { 14, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/diablos.png" },
        { 16, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/velociprey.png" },
        { 18, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/felyne.png" },
        { 19, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/vespoid.png" },
        { 20, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gypceros.png" },
        { 22, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/basarios.png" },
        { 23, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/melynx.png" },
        { 24, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/hornetaur.png" },
        { 25, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/apceros.png" },
        { 26, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/monoblos.png" },
        { 27, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/velocidrome.png" },
        { 28, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gendrome.png" },
        { 29, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 30, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/ioprey.png" },
        { 31, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/iodrome.png" },
        { 32, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 33, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/kirin.png" },
        { 34, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/cephalos.png" },
        { 35, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/giaprey.png" },
        { 37, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/pink_rathian.png" },
        { 38, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/blue_yian_kut-ku.png" },
        { 39, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/purple_gypceros.png" },
        { 40, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/yian_garuga.png" },
        { 41, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/silver_rathalos.png" },
        { 42, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gold_rathian.png" },
        { 43, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/black_diablos.png" },
        { 44, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/white_monoblos.png" },
        { 45, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/red_khezu.png" },
        { 46, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/green_plesioth.png" },
        { 47, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/black_gravios.png" },
        { 49, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/azure_rathalos.png" },
        { 50, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/ashen_lao-shan_lung.png" },
        { 52, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/congalala.png" },
        { 54, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/kushala_daora.png" },
        { 55, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shen_gaoren.png" },
        { 56, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/great_thunderbug.png" },
        { 57, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shakalaka.png" },
        { 58, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/yama_tsukami.png" },
        { 59, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/chameleos.png" },
        { 60, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/rusted_kushala_daora.png" },
        { 61, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/blango.png" },
        { 62, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/conga.png" },
        { 63, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/remobra.png" },
        { 64, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/lunastra.png" },
        { 66, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/hermitaur.png" },
        { 67, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shogun_ceanataur.png" },
        { 68, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/bulldrome.png" },
        { 69, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/anteka.png" },
        { 70, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/popo.png" },
        { 72, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/yama_tsukami.png" },
        { 73, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/ceanataur.png" },
        { 75, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/lavasioth.png" },
        { 77, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/akantor.png" },
        { 78, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/bright_hypnoc.png" },
        { 79, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/red_lavasioth.png" },
        { 81, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/orange_espinas.png" },
        { 82, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/silver_hypnoc.png" },
        { 84, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/akura_jebia.png" },
        { 85, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/berukyurosu.png" },
        { 86, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/cactus.png" },
        { 87, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 88, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 90, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/white_espinas.png" },
        { 91, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/kamu_orugaron.png" },
        { 92, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/nono_orugaron.png" },
        { 93, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/raviente.png" },
        { 94, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/dyuragaua.png" },
        { 96, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gurenzeburu.png" },
        { 97, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/burukku.png" },
        { 98, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/erupe.png" },
        { 101, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gogomoa.png" },
        { 102, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gogomoa.png" },
        { 104, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/abiorugu.png" },
        { 105, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/kuarusepusu.png" },
        { 108, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/rebidiora.png" },
        { 114, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/farunokku.png" },
        { 115, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/pokaradon.png" },
        { 117, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/pokara.png" },
        { 118, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 119, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/goruganosu.png" },
        { 120, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/aruganosu.png" },
        { 122,  "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zerureusu.png" },
        { 123, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gougarf.png" },
        { 124, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/uruki.png" },
        { 125, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/forokururu.png" },
        { 126, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/meraginasu.png" },
        { 127, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/diorex.png" },
        { 128, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/garuba_daora.png" },
        { 130, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/varusaburosu.png" },
        { 131, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/poborubarumu.png" },
        { 132, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/duremudira.png" },
        { 133, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 134, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/felyne.png" },
        { 135, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 136, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 137, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/cactus.png" },
        { 138, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 139, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gureadomosu.png" },
        { 143, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/kusubami.png" },
        { 144, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/yama_kurai.png" },
        { 145, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/duremudira.png" },
        { 147, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/deviljho.png" },
        { 148, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/brachydios.png" },
        { 149, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/berserk_raviente.png" },
        { 150, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/toa_tesukatora.png" },
        { 151, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/barioth.png" },
        { 152, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/uragaan.png" },
        { 153, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/stygian_zinogre.png" },
        { 156, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 157, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 158, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/voljang.png" },
        { 159, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/nargacuga.png" },
        { 160, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/keoaruboru.png" },
        { 161, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenaserisu.png" },
        { 162, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/gore_magala.png" },
        { 163, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/blinking_nargacuga.png" },
        { 164, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/shagaru_magala.png" },
        { 165, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/amatsu.png" },
        { 167, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/arrogant_duremudira.png" },
        { 168, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 169, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/seregios.png" },
        { 170, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/zenith_bogabadorumu.gif" },
        { 171, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/random.png" },
        { 172, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/bombardier_bogabadorumu.png" },
        { 173, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/uruki.png" },
        { 174, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/sparkling_zerureusu.png" },
        { 175, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/pso2_rappy.png" },
        { 176, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/monster/king_shakalaka.png" },
    });
}
