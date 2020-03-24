

let validation = document.getElementById("nissValidate");
let CheckNiss = () => {
    let nissValue = document.getElementById("niss").value;
    $.ajax({
        url: 'Identity/CheckIdentity/' + nissValue,
        type: 'GET',
        dateType: 'text',
        success: function(data) {
            let result = JSON.parse(data);

            validation.innerText = result == 1 ? "Ce n° de registre national est déja enregistré" : "";
            
        });

    }
}

$('#niss').on('t')




