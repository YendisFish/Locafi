<script>
	import PlayBar from './Components/PlayBar.svelte';
	import MainPage from './Components/MainPage.svelte'
	import axios from "axios";

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

	{#each playlists as playlist}
		<p>{playlist.name} AAAAAAAAA</p>
	{/each}
	
	<button on:click={getPlaylists}>Get Playlists</button>
</main>

<style>
	.playbar {
		position: fixed;
		bottom: 0;
		left: 0;
		/*z-index: 10;*/
	}
</style>