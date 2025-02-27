// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var toggleOpen = document.getElementById('menu-toggle-open');
var toggleClose = document.getElementById('menu-toggle-close');
var form = document.getElementsByTagName('form');
var topNav = document.getElementsByClassName('nv-d');
toggleOpen.addEventListener('click', () => {
	var nav = document.getElementById('side-nav');
	var navmobile = document.getElementById('side-nav-mobile');
	navmobile.style.width = '240px';
	navmobile.style.left = '0';
	nav.style.width = '240px';
	toggleOpen.style.display = 'none';
	toggleClose.style.display = 'block';
});
toggleClose.addEventListener('click', () => {
	var nav = document.getElementById('side-nav');
	var navmobile = document.getElementById('side-nav-mobile');
	navmobile.style.width = '60px';
	navmobile.style.left = '-60px';
	nav.style.width = '60px';
	toggleClose.style.display = 'none';
	toggleOpen.style.display = 'block';
});
