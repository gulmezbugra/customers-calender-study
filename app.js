const userList = document.getElementById("list-tab");
const selection = document.getElementById("secili-kullanici");

const doorSelect = document.getElementById("inputGroupSelect04");
const selectDoorButton = document.querySelector(
    ".input-group .btn"
);



const eraserButton = document.getElementById("eraserButton");
const deleteAllButton = document.getElementById("deleteAllButton");


let eraserMode = false;

let seciliKullanici = null;
let selectedDoor = null;
let aktifFiltre = null;

let kayitlar = [];

const SAAT_API_URL = "https://localhost:7021/api/saat";
const ATAMA_API_URL = "https://localhost:7021/api/KayitAtama";

baslat();

async function baslat() {

    await atamalariGetir();

    kayitliVerileriTabloyaYaz();
    tabloyuFiltrele();
    containerList();

    await saatleriGetir();

}

async function atamalariGetir() {

    try {
        const response = await fetch(ATAMA_API_URL);

        if (!response.ok) {
            throw new Error("Atamalar alınamadı, HTTP durumu: " + response.status);
        }

        kayitlar = await response.json();

    } catch (error) {

        console.error("Atama verisi API'den çekilemedi:", error);
        kayitlar = [];

    }

}

async function saatleriGetir() {

    try {
        const response = await fetch(SAAT_API_URL);

        if (!response.ok) {
            throw new Error("Saatler alınamadı, HTTP durumu: " + response.status);
        }

        const saatler = await response.json();

        saatleriTabloyaYaz(saatler);

    } catch (error) {

        console.error("Saat verisi API'den çekilemedi:", error);

    }

}

function saatMetniCikar(saatVerisi) {
    return typeof saatVerisi === "string"
        ? saatVerisi
        : (saatVerisi.saatAraligi ?? saatVerisi.saat ?? saatVerisi.value);

}

function saatleriTabloyaYaz(saatler) {

    const satirlar = document.querySelectorAll(
        ".schedule-table table tbody tr"
    );

    satirlar.forEach((satir, index) => {

        const saatVerisi = saatler[index];

        if (!saatVerisi) {
            return;
        }

        const baslangicMetni = saatMetniCikar(saatVerisi);

        if (!baslangicMetni) {
            return;
        }

        const saatHucresi = satir.querySelector("td.day");

        if (baslangicMetni.includes("-")) {

            if (saatHucresi) {
                saatHucresi.textContent = baslangicMetni;
            }

            satir.setAttribute("data-saat", baslangicMetni);

            return;
        }
        const sonrakiVeri = saatler[index + 1];
        const bitisMetni = sonrakiVeri ? saatMetniCikar(sonrakiVeri) : null;

        if (!bitisMetni) {
            satir.remove();
            return;
        }

        const saatMetni = `${baslangicMetni} - ${bitisMetni}`;

        if (saatHucresi) {
            saatHucresi.textContent = saatMetni;
        }

        satir.setAttribute("data-saat", saatMetni);

    });

    tabloyuFiltrele();

}

function tabloyuFiltrele() {

    document.querySelectorAll("[data-cell-id]").forEach(bosluk => {

        const boslukId = bosluk.getAttribute("data-cell-id");

        let eslesenKayitlar = kayitlar.filter(item => item.boslukID === boslukId);


        if (eslesenKayitlar.length === 0) {
            boslukSil(bosluk);
        }
        else {
            boslukDoldur(bosluk, eslesenKayitlar);
        }

    });

}



function containerList() {

    fetch("https://jsonplaceholder.typicode.com/users")
        .then((res) => res.json())
        .then((users) => {

            users.forEach((user, i) => {

                const a = document.createElement("a");

                a.className =
                    "list-group-item list-group-item-action";

                a.id = `user-${i}-list`;
                a.setAttribute("role", "button");

                a.textContent = user.name;



                a.addEventListener("click", function (e) {

                    e.preventDefault();



                    document
                        .querySelectorAll(
                            "#list-tab .list-group-item"
                        )
                        .forEach(item => {

                            item.classList.remove("active");

                        });



                    this.classList.add("active");


                    seciliKullanici = {

                        id: user.id,
                        name: user.name

                    };

                    selection.innerHTML =
                        `SEÇİLİ KULLANICI: ${user.name.toUpperCase()}
                         <hr>`;

                });


                userList.appendChild(a);

            });

        })
        .catch((error) => {

            console.log("Kullanıcılar alınamadı:", error);

        });

}

selectDoorButton.addEventListener("click", async function () {

    const selectedValue = doorSelect.value;
    const selectedText =
        doorSelect.options[doorSelect.selectedIndex].text;

    if (selectedValue === "Kapılar") {

        selectedDoor = null;
        aktifFiltre = null;

        await atamalariGetir();

        tabloyuFiltrele();

        return;
    }

    selectedDoor = selectedText;
    aktifFiltre = selectedText;

    try {

        const response = await fetch(
            `${ATAMA_API_URL}/Kapı-${selectedValue}`
        );

        if (!response.ok) {

            if (response.status === 404) {
                kayitlar = [];
                tabloyuFiltrele();

                alert(selectedText + " için kayıt bulunamadı.");
                return;
            }

            throw new Error(
                "Kapı kayıtları alınamadı. HTTP durumu: " +
                response.status
            );
        }

        kayitlar = await response.json();

        tabloyuFiltrele();

        alert(selectedText + " seçildi.");

    } catch (error) {

        console.error(
            "Kapı kayıtları API'den alınamadı:",
            error
        );

        alert("Kapı kayıtları alınırken bir hata oluştu.");
    }

});


function kayitliVerileriTabloyaYaz() {

    const satirlar = document.querySelectorAll(
        ".schedule-table table tbody tr"
    );

    satirlar.forEach((satir, satirIndex) => {

        const bosluklar = satir.querySelectorAll("td");

        bosluklar.forEach((bosluk, boslukIndex) => {

            if (boslukIndex === 0) {
                return;
            }

            const boslukId = `cell-${satirIndex}-${boslukIndex}`;

            bosluk.setAttribute("data-cell-id", boslukId);


            bosluk.addEventListener("click", function () {
                if (eraserMode) {
                    veriyiSil(this);
                } else {
                    kullaniciyiBoslugaGir(this);
                }
            });

        });

    });

}


async function veriyiSil(bosluk) {

    const boslukId = bosluk.getAttribute("data-cell-id");
    let hedefKayitlar = kayitlar.filter(item => item.boslukID === boslukId);

    if (aktifFiltre) {
        hedefKayitlar = hedefKayitlar.filter(item => item.door === aktifFiltre);
    }

    if (hedefKayitlar.length === 0) {
        return;
    }

    try {

        for (const kayit of hedefKayitlar) {

            const response = await fetch(`${ATAMA_API_URL}/${kayit.id}`, {
                method: "DELETE"
            });

            if (!response.ok) {
                throw new Error("Silme başarısız, HTTP durumu: " + response.status);
            }

        }

        const silinecekIdler = hedefKayitlar.map(k => k.id);
        kayitlar = kayitlar.filter(item => !silinecekIdler.includes(item.id));

        tabloyuFiltrele();

    } catch (error) {

        console.error("Atama API'den silinemedi:", error);
        alert("Silme işlemi sırasında bir hata oluştu, konsolu kontrol ediniz.");

    }

}



async function kullaniciyiBoslugaGir(bosluk) {


    if (!seciliKullanici) {

        alert("Önce bir kullanıcı seçiniz.");

        return;

    }


    if (!selectedDoor) {

        alert("Önce bir kapı seçip Seç butonuna basınız.");

        return;

    }


    const boslukId =
        bosluk.getAttribute("data-cell-id");

    const mevcutKayit = kayitlar.find(item => {
        return item.boslukID === boslukId && item.door === selectedDoor;
    });

    if (
        mevcutKayit && mevcutKayit.kullaniciIsmi !== seciliKullanici.name
    ) {
        const result = confirm(
            "Bu alanda zaten başka bir kullanıcı bulunmaktadır.\n\n" +
            "Değişiklik yapmak istediğinize emin misiniz?"
        );

        if (!result) {
            return;
        }

    }

    const atamaVerisi = {
        boslukID: boslukId,
        kullaniciID: seciliKullanici.id,
        kullaniciIsmi: seciliKullanici.name,
        door: selectedDoor
    };

    try {

        if (mevcutKayit) {

            const response = await fetch(`${ATAMA_API_URL}/${mevcutKayit.id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ ...atamaVerisi, id: mevcutKayit.id })
            });

            if (!response.ok) {
                throw new Error("Güncelleme başarısız, HTTP durumu: " + response.status);
            }

            kayitlar = kayitlar.filter(item => item.id !== mevcutKayit.id);
            kayitlar.push({ ...atamaVerisi, id: mevcutKayit.id });

        } else {
            const response = await fetch(ATAMA_API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(atamaVerisi)
            });

            if (!response.ok) {
                throw new Error("Ekleme başarısız, HTTP durumu: " + response.status);
            }

            const eklenenKayit = await response.json();
            kayitlar.push(eklenenKayit);

        }

        tabloyuFiltrele();

    } catch (error) {

        console.error("Atama API'ye kaydedilemedi:", error);
        alert("Atama kaydedilirken bir hata oluştu, konsolu kontrol ediniz.");

    }

}



function boslukDoldur(bosluk, kayitListesi) {

    bosluk.classList.add("active");

    const satir = bosluk.closest("tr");
    const saatMetni = satir ? satir.getAttribute("data-saat") : null;

    const ustBaslik = kayitListesi
        .map(k => k.kullaniciIsmi).join(", ");

    const saatBaslikHtml = saatMetni
        ? `<h5 class="hover-saat">${saatMetni}</h5>`
        : "";

    const hoverIcerik = kayitListesi
        .map(k => `
            <div>
                <h4>${k.door}</h4>
                <span>${k.kullaniciIsmi}</span>
            </div>
        `).join("");

    bosluk.innerHTML = `

        <h4>${ustBaslik}</h4>

        <div class="hover">
            ${saatBaslikHtml}
            ${hoverIcerik}
        </div>

    `;

}



function boslukSil(bosluk) {

    bosluk.classList.remove("active");

    bosluk.innerHTML = "";

}



eraserButton.addEventListener("click", function () {

    eraserMode = !eraserMode;

    const scheduleTable =
        document.querySelector(".schedule-table");


    if (eraserMode) {

        eraserButton.innerHTML =
            '<i class="bi bi-eraser"></i> Silgi Aktif';

        eraserButton.classList.remove("btn-outline-danger");
        eraserButton.classList.add("btn-danger");

        scheduleTable.classList.add("eraser-mode");

    } else {

        eraserButton.innerHTML =
            '<i class="bi bi-eraser"></i> Silgi';

        eraserButton.classList.remove("btn-danger");
        eraserButton.classList.add("btn-outline-danger");

        scheduleTable.classList.remove("eraser-mode");
    }

});

deleteAllButton.addEventListener("click", async function () {

    const result = confirm("Tüm tabloyu silmek istediğinize emin misiniz?");
    if (!result) return;

    try {

        for (const kayit of kayitlar) {

            const response = await fetch(`${ATAMA_API_URL}/${kayit.id}`, {
                method: "DELETE"
            });

            if (!response.ok) {
                throw new Error("Silme başarısız, HTTP durumu: " + response.status);
            }

        }

        document.querySelectorAll("[data-cell-id]").forEach(bosluk => {
            boslukSil(bosluk);
        });

        kayitlar = [];

        alert("Tüm tablo silindi.");

    } catch (error) {

        console.error("Tümünü silme sırasında hata:", error);
        alert("Tümünü silme işlemi sırasında bir hata oluştu, konsolu kontrol ediniz.");

    }

});






