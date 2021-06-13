using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Translator : MonoBehaviour
{
    private static int languageId;
    private static List<TranslatableText> listId = new List<TranslatableText>();

    #region ALL TEXT [ARRAY 2D]

    private static string[,] LineText =
    {
        #region ENGLISH
        {
            "Score:",       // 0
            "Best score:",  // 1
            "Tap to restart",
            "Shots delay (s):",
            "Destroyed asterods:",
            "BEST SCORE:",
            "Play",
            "INFO",
            "Settings",
            "Language",
            "Game Rules",
            "1. Shoot down asteroids to get points.\n2. Medium and small asteroids are destroyed with \n1 hit, large ones - with 3.\n3. Having collected 4000 points along with the asteroids, bonuses(green) that speed up the reloading of weapons and death orbs(red) begin to fly towards you.\n4. Gaining 10000 and 100000 points, your ship becomes more powerful\n5. The difficulty of the game gradually increases while you are alive.",
            "Game settings",
            "Difficulty",
            "Graphics",
            "Control type",
            "Sound",
            "Select language",
            "Enter you name...",
            "OK",
            "Name already exists",
            "Place",
            "Name",
            "Score",
            "Asteroids",
            "Show my positon"

        },
        #endregion

        #region RUSSIAN
        {
            "Счет:",       
            "Лучший счет:",  
            "Попробовать еще раз",
            "Задержка между выстрелами (с):",
            "Уничтожено астероидов:",
            "ЛУЧШИЙ СЧЕТ:",
            "Играть",
            "Игровые механики",
            "Настройки",
            "Выбор языка",
            "Правила игры",
            "1. Сбивайте астероиды, чтобы получать очки.\n2. Средние и малые астероиды уничтожаются с 1 попадания, большие и огромные — с 3 - х.\n3. Набрав 4000 очков вместе с астероидами к вам начинают лететь бонусы(зеленые) ускоряющие перезарядку орудий и сферы смерти(красные).\n4. Набрав 10000 и 100000 очков, ваш корабль\nстановится мощнее.\n5. Сложность игры постепенно возрастает пока вы живы.",
            "Игровые параметры",
            "Сложность",
            "Графика",
            "Тип управления",
            "Звук",
            "Выберите язык",
            "Введите свое имя...",
            "ОК",
            "Имя уже существует",
            "Позиция",
            "Имя",
            "Счет",
            "Астероиды",
            "Моя позиция"
        },
        #endregion

        #region CHIENSE
        {
            "结果",
            "最佳得分:",
            "再试一次",
            "拍摄之间的延迟 (秒):",
            "被摧毁的小行星:",
            "最佳得分:",
            "玩",
            "游戏规则",
            "参数",
            "语言选择",
            "游戏规则",
            "1. 击落小行星以获得积分.\n2. 小型和中型小行星可以一击摧毁，\n而大型和巨大的小行星可以三击摧毁.\n3. 与小行星一起获得4000点后，加速武器重装的奖励 (绿色）和死亡球（红色）开始向你飞来.\n4. 拥有10,000和100,000点，\n您的飞船将变得更加强大.\n5. 游戏难度在你活着的时候逐渐增加.",
            "游戏参数",
            "游戏难度",
            "图形质量",
            "控制类型",
            "声音",
            "选择语言",
            "输入你的名字...",
            "确认",
            "名称已存在",
            "位置",
            "选手姓名",
            "结果",
            "小行星",
            "我的位置"
        },
        #endregion

        #region GERMAN
        {
            "Ergebnis:",
            "Bestes Ergebnis:",
            "Versuchen Sie es nochmal",
            "Verzögerung zwischen Schüssen (s):",
            "Zerstörte Asteroiden:",
            "BESTES ERGEBNIS:",
            "Starte das Spiel",
            "Spielregel",
            "die Einstellungen",
            "Sprachauswahl",
            "Spielregel",
            "1. Schießen Sie Asteroiden ab, um Punkte zu erhalten.\n2. Mittlere und kleine Asteroiden werden mit 1 Treffer zerstört, große und riesige – mit 3.\n3. Nachdem Sie 4000 Punkte gesammelt haben, fliegen zusammen mit den Asteroiden Boni (grün), die das Nachladen von Waffen beschleunigen, und Todeskugeln (rot) auf Sie zu.\n4. Mit 10.000 und 100.000 Punkten wird dein Schiff mächtiger.\n5. Der Schwierigkeitsgrad des Spiels erhöht sich allmählich, während Sie leben.",
            "Spielparameter",
            "Komplexität",
            "Grafikqualität",
            "Steuerungstyp",
            "Klang",
            "Wähle deine Sprache",
            "Gib deinen Namen ein...",
            "Bestätigen",
            "Name existiert bereits",
            "Position",
            "Name",
            "Ergebnis",
            "Asteroiden",
            "Meine Position"
        },
        #endregion

        #region JAPANESE
        {
            "スコア:",
            "最高のスコア:",
            "再試行",
            "ショット間の遅延 (秒):",
            "破壊された小惑星の総数:",
            "最高のスコア:",
            "演奏する",
            "ゲームの仕組み",
            "パラメーター",
            "言語選択",
            "ゲームのルール",
            "1. ポイントを獲得するために小惑星を撃墜する.\n2. 中小惑星は1ヒットで破壊され、\n大小惑星は3ヒットで破壊されます.\n3. 小惑星の助けを借りて4000ポイントを獲得した後、\nボーナス (緑) があなたに向かって飛んで武器のリロードをスピードアップし、それとともに死の球 (赤) があなたを破壊する可能性があります.\n4. 10,000ポイントと100,000ポイントで、 あなたの船はより強力になります.\n5. あなたが生きている間、 ゲームの難易度は徐々に増加します.",
            "ゲームパラメータ",
            "複雑",
            "グラフィックの質",
            "コントロールタイプ",
            "音",
            "言語を選択",
            "あなたの名前を入力してください...",
            "確認",
            "名前はすでに存在します",
            "場所",
            "エイリアス",
            "スコア",
            "総小惑星",
            "私の立場"
        },
        #endregion

        #region FRENCH
        {
            "Score actuel:",
            "Meilleur score:",
            "Réessayer",
            "Délai entre le(s) tir (s):",
            "Astéroïdes détruits:",
            "MEILLEUR SCORE:",
            "Jouer",
            "Règles du jeu",
            "Paramètres",
            "Sélection de la langue",
            "Règles du jeu",
            "1. Abattez des astéroïdes pour obtenir des points.\n2. Les astéroïdes moyens et petits sont détruits en 1 coup, gros et énormes - avec 3.\n3. Après avoir gagné 4000 points, avec les astéroïdes, des bonus (vert) qui accélèrent le rechargement des armes et des orbes de mort (rouge) commencent à voler vers vous.\n4. Avec 10 000 et 100 000 points, votre vaisseau devient plus puissant.\n5. La difficulté du jeu augmente progressivement pendant que vous êtes en vie.",
            "Paramètres de jeu",
            "Complexité",
            "Qualité graphique",
            "Type de contrôle",
            "Le son",
            "Choisissez la langue",
            "Entrez votre nom...",
            "Confirmer",
            "Le nom existe déjà",
            "Un endroit",
            "Surnom",
            "Score",
            "Astéroïdes",
            "Ma position"
        },
        #endregion

        #region PORTUGUESE
        {
            "Pontuação atual:",
            "Melhor pontuação:",
            "Tente novamente",
            "Atraso entre tiros (s):",
            "Asteróides destruídos:",
            "MELHOR PONTUAÇÃO:",
            "Jogar",
            "Regras do jogo",
            "Parâmetros",
            "Seleção de idioma",
            "Regras do jogo",
            "1. Derrube asteróides para ganhar pontos.\n2. Asteróides médios e pequenos são destruídos com 1 acerto, grandes e enormes - com 3.\n3. Depois de ganhar 4000 pontos, junto com os asteróides, bônus (verdes) que aceleram o recarregamento de armas e orbes mortais (vermelhos) começam a voar em sua direção.\n4. Com 10.000 e 100.000 pontos, sua nave se torna mais poderosa.\n5. A dificuldade do jogo aumenta gradualmente enquanto você está vivo.",
            "Parâmetros do jogo",
            "Complexidade",
            "Gráficos",
            "Tipo de controlo",
            "Som",
            "Escolha o seu idioma",
            "Digite seu nome...",
            "Confirme",
            "Nome já existe",
            "Número",
            "Nome",
            "Pontuações",
            "Asteróides",
            "Minha posição"
        },
        #endregion

        #region SPANISH
        {
            "Puntaje:",
            "Mejor puntuación:",
            "Intentar otra vez",
            "Retraso de los disparos (s):",
            "Asteroides destruidos:",
            "MEJOR PUNTUACIÓN:",
            "Tocar",
            "Reglas del juego",
            "Ajustes",
            "Selección de idioma",
            "Reglas del juego",
            "1. Derriba asteroides para ganar puntos.\n2. Los asteroides medianos y pequeños se destruyen de un solo golpe, grandes y grandes, tres golpes.\n3. Después de ganar 4000 puntos, junto con los asteroides, las bonificaciones (verdes) que aceleran la recarga de armas y los orbes de la muerte (rojos) comienzan a volar hacia ti.\n4. Con 10,000 y 100,000 puntos, tu nave se vuelve más poderosa.\n5. La dificultad del juego aumenta gradualmente mientras estás vivo.",
            "Parámetros del juego",
            "Complejidad",
            "Gráficos",
            "Tipo de control",
            "Soñar",
            "Elige lengua",
            "Introduzca su nombre...",
            "Confirmar",
            "Nombre ya existe",
            "Posición",
            "Nombre",
            "Puntaje",
            "Asteroides",
            "Mi posición"
        },
        #endregion

        #region TURKISH
        {
            "Puan:",
            "En iyi puan:",
            "Yeniden başlatmak için dokunun",
            "Çekim (ler) arasındaki gecikme (s):",
            "Yok edilen asteroitler:",
            "EN İYİ PUAN:",
            "Oyna",
            "Oyunun kuralları",
            "Ayarlar",
            "Dil seçimi",
            "Oyunun kuralları",
            "1. Puan almak için asteroitleri vur.\n2. Orta ve küçük asteroitler 1 vuruşla yok edilir, büyük ve büyük - 3 ile.\n3. 4000 puan kazandıktan sonra asteroitler ile birlikte silahların yeniden yüklenmesini hızlandıran bonuslar (yeşil) ve ölüm küreleri (kırmızı) size doğru uçmaya başlar.\n4. 10.000 ve 100.000 puan ile geminiz daha güçlü hale gelir.\n5. Siz hayattayken oyunun zorluğu giderek artıyor.",
            "Oyun parametreleri",
            "Karmaşıklık",
            "Grafikler",
            "Kontrol tipi",
            "Ses",
            "Dil seçiniz",
            "Adınızı giriniz...",
            "Onaylamak",
            "Bu isim zaten var",
            "Bir yer",
            "Takma ad",
            "Puan",
            "Asteroitler",
            "Pozisyonum"
        },
        #endregion

        #region UKRANIAN
        {
            "Рахунок:",
            "Кращий рахунок:",
            "Спробувати ще раз",
            "Затримка між пострілами (с):",
            "Знищено астероїдів:",
            "КРАЩИЙ РАХУНОК:",
            "Грати",
            "Ігрові механіки",
            "Налаштування",
            "Вибір мови",
            "Правила гри",
            "1. Збівайте астероїді, щоб отрімуваті бали.\n2. Середні і малі астероїди знищуються з 1 попадання, великі і величезні - з 3 - х.\n3. Набравши 4000 балів разом з астероїдами до вас починають летіти бонуси (зелені) прискорюють перезарядку знарядь і сфери смерті (червоні).\n4. Набравши 10000 і 100000 балів, ваш корабель стає потужнішою.\n5. Складність гри поступово зростає поки ви живі.",
            "Ігрові параметри",
            "Складність",
            "Графіка",
            "Тип управління",
            "Звук",
            "Виберіть мову",
            "Введіть своє ім'я...",
            "ОК",
            "Ім'я вже існує",
            "Позиція",
            "Ім'я",
            "Рахунок",
            "Астероїди",
            "Моя позиція"
        },
        #endregion

    };
    #endregion

    static public void SelectLanguage(int ID)
    {
        languageId = ID;
        UpdateTexts();
    }

    static public string GetText(int textKey)
    {
        return LineText[languageId, textKey];
    }

    static public void Add(TranslatableText idText)
    {
        listId.Add(idText);
    }

    static public void Delete(TranslatableText idText)
    {
        listId.Remove(idText);
    }

    static public void UpdateTexts()
    {
        for (int i = 0; i < listId.Count; i++)
        {
            listId[i].UIText.text = LineText[languageId, listId[i].textID]; // тексты
        }
    }

}

