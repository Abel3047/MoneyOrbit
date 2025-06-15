// src/react-icons.d.ts

import { IconTree, IconType } from "react-icons";

declare module "react-icons/lib/esm/iconBase" {
  interface IconBaseProps extends React.SVGAttributes<SVGElement> {
    children?: React.ReactNode;
    size?: string | number;
    color?: string;
    title?: string;
  }
  
  type IconType = (props: IconBaseProps) => JSX.Element;
}