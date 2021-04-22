package com.scuavailable.available.util;

public class JNIUtils {
    static {
        System.loadLibrary("native-lib");
    }
    public static native int[] getCountPaperLocation(int[] buf,int w, int h);

}
