<script>
	import PlayBar from './Components/PlayBar.svelte';
	import MainPage from './Components/MainPage.svelte'
	import InputComponent from './Components/InputComponent.svelte';
	import axios from "axios";
	import PlaylistTab from "./Components/PlaylistTab.svelte";
	import SongInfo from "./Components/SongInfo.svelte";
	
	let playlists = [{name:"balls"}];
	
	let inSettings = false;
	
	function toggleSettings() {
		inSettings = !inSettings;
	}
	
	async function getPlaylists() {
		playlists = (await axios.get("http://localhost:5000/GET_PLAYLST_LIST")).data;
	}
</script>

<main>
	<script src="app://dynamic.js"></script>
	
	<div class="playlist">
		<PlaylistTab />
	</div>
	
	<MainPage />

	<div class="songinfo">
		<SongInfo />
	</div>

	<div class="playbar">
		<PlayBar />
	</div>
</main>

<style>
	.playbar {
		position: fixed;
		bottom: 0;
		left: 0;
		z-index: 4;
	}

	.playlist {
		position: fixed;
		z-index: 2;
	}
	
	.songinfo {
		z-index: 3;
	}
	
	* {
		user-select: none;
	}
</style>