<script>
	import PlayBar from './Components/PlayBar.svelte';
	import MainPage from './Components/MainPage.svelte'

	let inSettings = false;
	
	function toggleSettings() {
		inSettings = !inSettings;
	}
</script>

<main>
	<script src="app://dynamic.js"></script>
	
	<MainPage></MainPage>
	
	<div class="playbar">
		<PlayBar></PlayBar>
	</div>
</main>

<style>
	.playbar {
		position: fixed;
		bottom: 0;
		left: 0;
		/*z-index: 10;*/
	}
</style>