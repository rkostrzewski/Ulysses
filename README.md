ircviewer
=========

IRC Viewer

Infrared Camera Viewer

Software to obtain images from InfraRed Camera (IRC) via UDP, perform Non-Uniformity Correction (NUC) and Post Processing on images.

C# - WPF - MVVM pattern

Views:

-IRCViewer.Views
  - WPF windows summary 
  
ViewModels:

-IRCViewer.ViewModels
  - View Models for Views

Models:

-IRCViewer.UDPHandler
  - UDP Server to obtain images form IRC

-IRCViewer.NUC
  - implemented NUC methods
    - Reference Based : 
        - Two-Point NUC (TP NUC) 
    - Scene Based :
        - Constant Range (CR NUC)
        - Interframe Registrastion Least Mean Square Error (IRLMS NUC)

-IRCViewer.PostProcessing
  - Display And Detail Enchancement for High Dynamic Range Images
      - Bilateral Filter
      - Removal of gradient reversal artifacts
      - Detail Enchancement
      - Rearranging 12 bit image to 8 bit domain
      - Histogram Rearrangement
  - Constant Brightness, Contrast and Gamma Filters
