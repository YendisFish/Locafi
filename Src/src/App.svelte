<script>
	import PlayBar from './Components/PlayBar.svelte';
</script>

<main>
	<script src="app://dynamic.js"></script>
	
	<div class="playbar">
		<PlayBar></PlayBar>
	</div>
</main>

<style>
	.playbar {
		position: fixed;
		bottom: 0;
		left: 0;
		z-index: 10;
	}
</style>