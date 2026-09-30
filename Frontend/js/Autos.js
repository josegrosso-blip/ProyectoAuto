const urlAutos = "https://tuning-logic-salmon-launched.trycloudflare.com/api/CargarAutos";

function mostrarCargaAuto(data) {
    const tbody = document.getElementById("tablaCargaAuto");

   
}

function ObtenerCargaAuto() {
	fetch(urlAutos)
		.then((respuesta) => {
			if (!respuesta.ok) {
				throw new Error(`Error HTTP: ${respuesta.status}`);
			}

			return respuesta.json();
		})
		.then((data) => {
			mostrarCargaAuto(data);
		})
		.catch((error) => {
			console.log("No se pudieron cargar los autos:", error);
		});
}

function mostrarCargaAuto(data) {
	const tbody = document.getElementById("tablaCargaAuto");

	if (!tbody) {
		return;
	}

	tbody.innerHTML = "";

	data.forEach((element) => {
		let tr = tbody.insertRow();

		tr.insertCell(0).textContent = element.marca;

		let tdModelo = tr.insertCell(1);
		tdModelo.textContent = element.modelo;

		let tdAnio = tr.insertCell(2);
		tdAnio.textContent = element.año;

		tr.insertCell(3).textContent = element.patente;

		let tdKm = tr.insertCell(4);
		tdKm.textContent = element.kms;

		let tdFecha = tr.insertCell(5);
		tdFecha.textContent = element.fechaDeIngreso ? element.fechaDeIngreso.substring(0, 10) : "";

		tr.insertCell(6).textContent = element.disponibilidad ? "Disponible" : "No disponible";

		let editar = document.createElement("button");
		editar.textContent = "Editar";
		editar.classList.add("btn", "btn-primary");
		editar.onclick = function () {
			BuscarValoresCargaAuto(element.autosId);
		};

		tr.insertCell(7).appendChild(editar);

		let eliminar = document.createElement("button");
		eliminar.textContent = "Eliminar";
		eliminar.classList.add("btn", "btn-danger");
		eliminar.onclick = function () {
			if (element.disponibilidad !== false) {
				alert("Solo se puede eliminar un auto cuando está No disponible.");
			} else if (confirm("¿Está seguro de que desea eliminar este auto?")) {
				EliminarCargaAuto(element.autosId);
			}
		};

		tr.insertCell(8).appendChild(eliminar);
	});
}

function AgregarCargaAuto(event) {
	if (event) {
		event.preventDefault();
	}

	let disponibilidad = document.getElementById("Disponibilidad").value;
	let nuevoAuto = {
		marca: document.getElementById("Marca").value.toUpperCase(),modelo: document.getElementById("Modelo").value.toUpperCase(),
        año: parseInt(document.getElementById("Año").value),
        patente: document.getElementById("Patente").value.toUpperCase(),
        kms: parseInt(document.getElementById("Kms").value),
        fechaDeIngreso: document.getElementById("FechaDeIngreso").value,
		disponibilidad: disponibilidad === "true"
	};

	if (!nuevoAuto.marca.trim()) {
		alert("El campo 'Marca' es obligatorio.");
		return;
	}

	if (!nuevoAuto.modelo.trim()) {
		alert("El campo 'Modelo' es obligatorio.");
		return;
	}

	if (nuevoAuto.modelo.trim().length < 2) {
		alert("El modelo debe tener al menos 2 caracteres.");
		return;
	}

	if (isNaN(nuevoAuto.año)) {
		alert("El campo 'Año' es obligatorio.");
		return;
	}

	if (nuevoAuto.año < 2000 || nuevoAuto.año > 2050) {
		alert("El campo 'Año' no puede ser menor que 2000 ni mayor a 2050.");
		return;
	}

	if (!nuevoAuto.patente.trim()) {
		alert("El campo 'Patente' es obligatorio.");
		return;
	}

	if (nuevoAuto.patente.trim().length < 7 || nuevoAuto.patente.trim().length > 9) {
		alert("La patente debe tener entre 7 y 9 caracteres.");
		return;
	}

	if (isNaN(nuevoAuto.kms) || nuevoAuto.kms < 0) {
		alert("El campo 'KM' debe ser un número igual o mayor que 0.");
		return;
	}

	if (!nuevoAuto.fechaDeIngreso) {
		alert("Debe seleccionar una fecha de ingreso.");
		return;
	}

	if (!disponibilidad) {
		alert("Debe seleccionar si el auto está disponible.");
		return;
	}

	fetch(urlAutos, {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(nuevoAuto)
	})
		.then((respuesta) => {
			if (!respuesta.ok) {
				throw new Error(`Error HTTP: ${respuesta.status}`);
			}

			alert("Auto agregado correctamente.");
			document.getElementById("Marca").value = "";
			document.getElementById("Modelo").value = "";
			document.getElementById("Año").value = "";
			document.getElementById("Patente").value = "";
			document.getElementById("Kms").value = "";
			document.getElementById("FechaDeIngreso").value = "";
			document.getElementById("Disponibilidad").value = "";
			ObtenerCargaAuto();
		})
		.catch((error) => {
			alert("No se pudo guardar el auto. " + error.message);
			console.log(error);
		});
}

function BuscarValoresCargaAuto(autosId) {
	fetch(`${urlAutos}/${autosId}`)
		.then((respuesta) => {
			if (!respuesta.ok) {
				throw new Error(`Error HTTP: ${respuesta.status}`);
			}

			return respuesta.json();
		})
		.then((data) => {
			document.getElementById("idEditar").value = data.autosId;
			document.getElementById("marcaEditar").value = data.marca;
			document.getElementById("modeloEditar").value = data.modelo;
			document.getElementById("anioEditar").value = data.año;
			document.getElementById("patenteEditar").value = data.patente;
			document.getElementById("kmEditar").value = data.kms;
			document.getElementById("fechaIngresoEditar").value = data.fechaDeIngreso.substring(0, 10);
			document.getElementById("disponibilidadEditar").value = data.disponibilidad;

			let modal = new bootstrap.Modal(document.getElementById("editarAutoModal"));
			modal.show();
		})
		.catch((error) => {
			console.log("No se pudo buscar el auto:", error);
		});
}

function EditarCargaAuto() {
	let id = document.getElementById("idEditar").value;
	let disponibilidad = document.getElementById("disponibilidadEditar").value;

	let editarAuto = {
		autosId: parseInt(id),
		marca: document.getElementById("marcaEditar").value.toUpperCase(),
		modelo: document.getElementById("modeloEditar").value.toUpperCase(),
		año: parseInt(document.getElementById("anioEditar").value),
		patente: document.getElementById("patenteEditar").value.toUpperCase(),
		kms: parseInt(document.getElementById("kmEditar").value),
		fechaDeIngreso: document.getElementById("fechaIngresoEditar").value,
		disponibilidad: disponibilidad === "true"
	};

	if (!editarAuto.marca.trim() || !editarAuto.modelo.trim()) {
		alert("La marca y el modelo son obligatorios.");
		return;
	}

	if (editarAuto.modelo.trim().length < 2) {
		alert("El modelo debe tener al menos 2 caracteres.");
		return;
	}

	if (isNaN(editarAuto.año) || editarAuto.año < 2000 || editarAuto.año > 2050) {
		alert("El año debe estar entre 2000 y 2050.");
		return;
	}

	if (editarAuto.patente.trim().length < 7 || editarAuto.patente.trim().length > 9) {
		alert("La patente debe tener entre 7 y 9 caracteres.");
		return;
	}

	if (isNaN(editarAuto.kms) || editarAuto.kms < 0) {
		alert("El campo 'KM' debe ser un número igual o mayor que 0.");
		return;
	}

	if (!editarAuto.fechaDeIngreso) {
		alert("Debe seleccionar una fecha de ingreso.");
		return;
	}

	if (!disponibilidad) {
		alert("Debe seleccionar si el auto está disponible.");
		return;
	}

	fetch(`${urlAutos}/${id}`, {
		method: "PUT",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(editarAuto)
	})
		.then((respuesta) => {
			if (!respuesta.ok) {
				throw new Error(`Error HTTP: ${respuesta.status}`);
			}

			alert("Auto editado correctamente.");
			let modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("editarAutoModal"));
			modal.hide();
			ObtenerCargaAuto();
		})
		.catch((error) => {
			console.log("No se pudo editar el auto:", error);
		});
}

function actualizarAuto(event) {
	event.preventDefault();
	EditarCargaAuto();
}

function EliminarCargaAuto(autosId) {
	fetch(`${urlAutos}/${autosId}`, {
		method: "DELETE"
	})
		.then((respuesta) => {
			if (!respuesta.ok) {
				if (respuesta.status === 400) {
					throw new Error("Solo se puede eliminar un auto cuando está No disponible.");
				}

				throw new Error(`Error HTTP: ${respuesta.status}`);
			}

			alert("Auto eliminado correctamente.");
			ObtenerCargaAuto();
		})
		.catch((error) => {
			alert(error.message);
		});
}

ObtenerCargaAuto();
