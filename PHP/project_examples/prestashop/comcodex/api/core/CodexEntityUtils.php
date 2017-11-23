<?php

/**
 * 
 * @author koiosoft
 *
 */
class CodexEntityUtils
{
	
	/**
	 * 
	 * @param CodexHttpEntity $entity
	 */
	public static function toString($entity)
	{
		return $entity->getContent();
	}
	
}