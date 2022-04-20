"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
		alert('Connected to dashboardHub');

		InvokeProducts();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedProducts", function (products) {
	BindProductsToGrid(products);
});


//$(function () {

//	var job = $.connection.myHub;


//	job.client.displayStatus = function () {
//		getData();
//	};


//	$.connection.hub.start();
//	getData();
//});

//function getData() {
//	var $tbl = $('#tbl');
//	$.ajax({
//		url: 'index.aspx/GetData',
//		contentType: "application/json; charset=utf-8",
//		dataType: "json",
//		type: "POST",
//		success: function (data) {
//			debugger;
//			if (data.d.length > 0) {
//				var newdata = data.d;
//				$tbl.empty();

//				$tbl.append(' <tr><th rowspan="2">mã CK</th><th colspan="6">bên mua</th><th colspan="3">khớp lệnh</th><th colspan="6">bên bán</th></tr><tr><th>giá 3</th><th>KL 3</th><th>giá 2</th><th>KL 1</th><th>giá 1</th><th>KL 1</th><th>giá</th><th>KL</th><th>+/-</th><th>giá 1</th><th>KL 1</th><th>giá 2</th><th>KL 2</th><th>giá 3</th><th>KL 3</th></tr>')
//				var rows = [];
//				for (var i = 0; i < newdata.length; i++) {
//					rows.push('<tr><td>' + newdata[i].id + '</td><td>' + newdata[i].name + '</td><td>' + newdata[i].address + '</td><td>' + newdata[i].age + '</td> <tr>');
//				}
//				$tbl.append(rows.join(''));
//			}
//		}
//	});
//}

function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty();
	var tr="";

	$.each(products, function (index, product) {
		tr = $('<tr/>');
		tr.append(`<td>${(index + 1)}</td>`);
		tr.append(`<td>${product.macp}</td>`);
		tr.append(`<td>${product.muagiaba}</td>`);
		tr.append(`<td>${product.muaklba}</td>`);
		tr.append(`<td>${product.muagiahai}</td>`);
		tr.append(`<td>${product.muaklhai}</td>`);
		tr.append(`<td>${product.muagiamot}</td>`);
		tr.append(`<td>${product.muaklmot}</td>`);
		tr.append(`<td>${product.giakhop}</td>`);
		tr.append(`<td>${product.klkhop}</td>`);
		tr.append(`<td>${product.bangiamot}</td>`);
		tr.append(`<td>${product.banklmot}</td>`);
		tr.append(`<td>${product.bangiahai}</td>`);
		tr.append(`<td>${product.banklhai}</td>`);
		tr.append(`<td>${product.bangiaba}</td>`);
		tr.append(`<td>${product.banklba}</td>`);
		tr.append(`<td>${product.tongkl}</td>`);
		$('#tblProduct tbody').append(tr);
	});
}

connection.on("ReceivedProductsForGraph", function (productsForGraph) {
	BindProductsToGraph(productsForGraph);
});

function BindProductsToGraph(productsForGraph) {
	var labels = [];
	var data = [];

	$.each(productsForGraph, function (index, item) {
		labels.push(item.macp);
		data.push(item.tongkl);
	});

	DestroyCanvasIfExists('canvasProudcts');

	const context = $('#canvasProudcts');
	const myChart = new Chart(context, {
		type: 'line',
		data: {
			labels: labels,
			datasets: [{
				label: '# of Products',
				data: data,
				backgroundColor: backgroundColors,
				borderColor: borderColors,
				borderWidth: 1
			}]
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	});
}


// supporting functions for Graphs
function DestroyCanvasIfExists(canvasId) {
	let chartStatus = Chart.getChart(canvasId);
	if (chartStatus != undefined) {
		chartStatus.destroy();
	}
}

var backgroundColors = [
	'rgba(255, 99, 132, 0.2)',
	'rgba(54, 162, 235, 0.2)',
	'rgba(255, 206, 86, 0.2)',
	'rgba(75, 192, 192, 0.2)',
	'rgba(153, 102, 255, 0.2)',
	'rgba(255, 159, 64, 0.2)'
];
var borderColors = [
	'rgba(255, 99, 132, 1)',
	'rgba(54, 162, 235, 1)',
	'rgba(255, 206, 86, 1)',
	'rgba(75, 192, 192, 1)',
	'rgba(153, 102, 255, 1)',
	'rgba(255, 159, 64, 1)'
];