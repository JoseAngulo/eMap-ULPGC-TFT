mergeInto(LibraryManager.library, {

  AddNumbers: function (x, y) {
    return x + y;
  },

  OpenPageInNewTab: function (url) {
    url = Pointer_stringify(url);
    console.log('Opening link: ' + url);
    window.open(url,'_blank');
  },

  HideLoadingScreenFadeOut: function () {
  	let loadingScreen = document.getElementById("loading_screen").querySelector("img");
	loadingScreen.classList.add("fade-out-bck");
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

});